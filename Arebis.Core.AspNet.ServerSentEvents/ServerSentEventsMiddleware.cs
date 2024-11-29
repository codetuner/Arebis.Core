using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.ServerSentEvents
{
    /// <summary>
    /// <see cref="ServerSentEventsMiddleware"/> provides support for Server Sent Events.
    /// </summary>
    public class ServerSentEventsMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IOptions<ServerSentEventsOptions> options;
        private readonly ServerSentEventsService serverSentEventsService;
        private readonly ILogger<ServerSentEventsMiddleware> logger;
        private readonly ILogger<ServerSentEventsClientProxy> clientLogger;

        /// <summary>
        /// Constructs a <see cref="ServerSentEventsMiddleware"/> instance.
        /// </summary>
        public ServerSentEventsMiddleware(RequestDelegate next, IOptions<ServerSentEventsOptions> options, ServerSentEventsService serverSentEventsService, ILogger<ServerSentEventsMiddleware> logger, ILogger<ServerSentEventsClientProxy> clientLogger)
        {
            this.next = next;
            this.options = options;
            this.serverSentEventsService = serverSentEventsService;
            this.logger = logger;
            this.clientLogger = clientLogger;
        }

        /// <summary>
        /// Invokes the <see cref="ServerSentEventsMiddleware"/> middleware.
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            // Identify SSE requests:
            if ((context.Request.Method == "GET")
             && (context.Request.Path.ToString().StartsWith(options.Value.PathPrefix ?? String.Empty, StringComparison.OrdinalIgnoreCase))
             && (context.Request.Headers["Accept"] == "text/event-stream" || options.Value.AcceptHeaderAny)
             && (options.Value.IsSseConnection == null || options.Value.IsSseConnection(context) == true))
            {
                logger.LogInformation("Incomming SSE connection.");
                ServerSentEventsClientProxy? clientProxy = null;
                try
                {
                    // Respond:
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "text/event-stream";
                    context.Response.Headers["Cache-Control"] = "no-cache";
                    context.Response.Headers["Connection"] = "keep-alive"; // Needs web.config configuration, see: https://stackoverflow.com/a/71310959/323122
                    options.Value.OnIncomingConnection?.Invoke(context);

                    // Get client proxy:
                    clientProxy = await serverSentEventsService.ConnectClient(context, options.Value.RecoverOnReconnection);

                    // Set reconnection time:
                    if (options.Value.ReconnectionTime.HasValue)
                    {
                        await clientProxy.SetReconnectionTime(options.Value.ReconnectionTime.Value);
                    }

                    // Flush content buffer:
                    await context.Response.Body.FlushAsync(context.RequestAborted);

                    // Wait "forever", until the client disconnects:
                    while (true)
                    {
                        var which = WaitHandle.WaitAny(new WaitHandle[] { context.RequestAborted.WaitHandle, clientProxy.AutoResetEvent }, options.Value.IdleTimeout);
                        if (which == 0)
                        {
                            break;
                        }
                        else if (which == 1)
                        {
                            if (options.Value.DisconnectAfterEachEvent) break;
                        }
                        else // (which == WaitHandle.WaitTimeout)
                        {
                            if (options.Value.SendKeepAlives) await clientProxy.SendKeepAlive();
                            else break;
                        }
                    }
                }
                catch (OperationCanceledException)
                { }
                catch (HttpRequestException hrex)
                {
                    if (hrex.StatusCode.HasValue) context.Response.StatusCode = (int)hrex.StatusCode;
                }
                finally
                {
                    // Remove the client:
                    if (clientProxy != null) await serverSentEventsService.DisconnectClient(context, clientProxy);

                    // It ends here. Do not proceed with next:
                    // This connection is old and broken.
                }
            }
            else
            {
                // Not an SSE request. Proceed with next:
                await next(context);
            }
        }
    }
}
