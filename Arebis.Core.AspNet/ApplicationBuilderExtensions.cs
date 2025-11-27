using Microsoft.AspNetCore.Builder;
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
        /// Installs support for HttpResponseException exceptions using middleware.
        /// </summary>
        [Obsolete("UseHttpResponseException middleware is obsolete. Use HttpResponseExceptionFilter instead.")]
        public static IApplicationBuilder UseHttpResponseException(this IApplicationBuilder application)
        {
            throw new NotImplementedException("UseHttpResponseException middleware is obsolete. Use HttpResponseExceptionFilter instead.");
        }
    }
}
