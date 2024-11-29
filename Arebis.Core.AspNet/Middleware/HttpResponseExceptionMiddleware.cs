using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Arebis.Core.Source;

namespace Arebis.Core.AspNet.Middleware
{
    /// <summary>
    /// ASP.NET middleware to support HttpResponseExceptions.
    /// </summary>
    [CodeSource("https://stackoverflow.com/a/35829637")]
    public class HttpResponseExceptionMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Constructs and installs as middleware.
        /// </summary>
        public HttpResponseExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Called to invoke the middleware when processing requests.
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (HttpResponseException httpResonseException)
            {
                context.Response.StatusCode = (int)httpResonseException.StatusCode;
                var responseFeature = context.Features.Get<IHttpResponseFeature>();
                if (responseFeature != null)
                {
                    responseFeature.ReasonPhrase = httpResonseException.Message;
                }
            }
        }
    }
}
