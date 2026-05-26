using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet
{
    /// <summary>
    /// Context extensions.
    /// </summary>
    public static class ContextExtensions
    {
        /// <summary>
        /// Get client hints.
        /// </summary>
        /// <example>
        /// <code>string? mobileHint = context.GetClientHint("Sec-CH-UA-Mobile");</code>
        /// </example>
        public static string? GetClientHint(this Microsoft.AspNetCore.Http.HttpContext context, string hintName)
        {
            if (context.Request.Headers.TryGetValue(hintName, out var hintValue))
                return hintValue.ToString();
            else
                return null;
        }

        /// <summary>
        /// Whether the viewport is narrow (e.g. mobile) based on client hints.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="narrowWidth">The maximum width considered narrow.</param>
        /// <returns>True if the viewport is narrow, otherwise false.</returns>
        public static bool IsViewportNarrow(this Microsoft.AspNetCore.Http.HttpContext context, int narrowWidth = 767)
        {
            var widthHint = context.GetClientHint("Sec-CH-Viewport-Width");
            if (widthHint != null && int.TryParse(widthHint, out var widthValue))
                return widthValue <= narrowWidth;

            var mobileHint = context.GetClientHint("Sec-CH-UA-Mobile");
            if (mobileHint != null)
                return mobileHint == "?1";

            return false;
        }
    }
}
