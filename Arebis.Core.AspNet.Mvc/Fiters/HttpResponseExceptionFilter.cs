using Arebis.Core.Source;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.Mvc.Filters
{
    /// <summary>
    /// Filter to handle exceptions of type <see cref="HttpResponseException"/> and <see cref="ResultException{TResult}"/>.
    /// </summary>
    /// <example>
    /// Install as a global filter in Program.cs:
    /// <code lang="csharp">
    /// builder.Services.AddControllersWithViews(options =>
    /// {
    ///     options.Filters.Add&lt;ApplicationActionFilter&gt;();
    /// })
    /// </code>
    /// </example>
    [CodeSource("https://learn.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-7.0")]
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        /// <inheritdoc/>
        public int Order => int.MaxValue - 10;

        /// <inheritdoc/>
        public void OnActionExecuting(ActionExecutingContext context) { }

        /// <inheritdoc/>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ResultException<IActionResult> arex)
            {
                context.Result = arex.Result;
                context.ExceptionHandled = true;
            }
            else if (context.Exception is HttpResponseException httpResponseException)
            {
                context.Result = new ObjectResult(httpResponseException.Message)
                {
                    StatusCode = (int)httpResponseException.StatusCode
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
