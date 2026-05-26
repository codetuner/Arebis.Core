using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet
{
    /// <summary>
    /// ApplicationBuilder extensions for Arebis.Core.AspNet features.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Requests for client hints.
        /// </summary>
        /// <param name="app">The app to configure.</param>
        /// <param name="aspects">The client hint aspects to request (incl. citical ones, i.e. "Sec-CH-UA-Mobile, Sec-CH-Viewport-Width, Sec-CH-Viewport-Height, Sec-CH-Prefers-Reduced-Motion"). Defaults to "Sec-CH-UA-Mobile, Sec-CH-Viewport-Width".</param>
        /// <param name="criticalAspects">The critical client hint aspects to request, if any (i.e. "Sec-CH-Prefers-Reduced-Motion").</param>
        public static IApplicationBuilder UseClientHints(this IApplicationBuilder app, string aspects = "Sec-CH-UA-Mobile, Sec-CH-Viewport-Width", string? criticalAspects = null)
        {
            return app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    if (!String.IsNullOrWhiteSpace(aspects)) context.Response.Headers.Append("Accept-CH", aspects);
                    if (!String.IsNullOrWhiteSpace(criticalAspects)) context.Response.Headers.Append("Critical-CH", criticalAspects);
                    return Task.CompletedTask;
                });

                await next();
            });
        }

        /// <summary>
        /// Installs support for HttpResponseException exceptions using middleware.
        /// </summary>
        [Obsolete("UseHttpResponseException middleware is obsolete. Use HttpResponseExceptionFilter instead.")]
        public static IApplicationBuilder UseHttpResponseException(this IApplicationBuilder application)
        {
            throw new NotImplementedException("UseHttpResponseException middleware is obsolete. Use HttpResponseExceptionFilter instead.");
        }
    }
}
