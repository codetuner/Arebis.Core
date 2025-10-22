using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.Mvc.Sircl
{
    /// <summary>
    /// ASP.NET MVC Context extension methods to support Sircl.
    /// </summary>
    public static class ContextExtensions
    {
        /// <summary>
        /// Whether the request is a Sircl partial request.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        public static bool IsSirclPartial(this HttpRequest request)
        {
            return (request.Headers["X-Sircl-Request-Type"] == "Partial");
        }

        /// <summary>
        /// Builds and sets the Sircl AppId by concatenating the given parts.
        /// I.e: Context.Response.SetSirclAppId("MyApp", languageCode, userToken)
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="appIdParts">The AppId parts that will be concatenated with '-' as separator.</param>
        public static void SetSirclAppId(this HttpResponse response, params string[] appIdParts)
        {
            response.Headers["X-Sircl-AppId"] = String.Join('-', appIdParts);
        }
    }
}
