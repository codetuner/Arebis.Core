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
    ///     options.Filters.Add&lt;ExceptionHandlingFilter&gt;();
    /// })
    /// </code>
    /// or on an API controller or action method:
    /// <code lang="csharp">
    /// [ExceptionHandlingFilter]
    /// public class MyController : Controller // or ControllerBase
    /// { ... }
    /// </code>
    /// </example>
    [CodeSource("https://learn.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-7.0")]
    public class ExceptionHandlingFilter : ActionFilterAttribute, IOrderedFilter
    {
        /// <summary>
        /// Constructs an instance of ExceptionHandlingFilter.
        /// </summary>
        public ExceptionHandlingFilter()
        {
            this.Order = int.MaxValue - 10;
        }

        ///// <inheritdoc/>
        //public int Order => int.MaxValue - 10;

        /// <inheritdoc/>
        public override void OnActionExecuting(ActionExecutingContext context) { }

        /// <inheritdoc/>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ResultException<IActionResult> arex)
            {
                // Set result:
                context.Result = arex.Result;

                // Set exception as handled:
                context.ExceptionHandled = true;
            }
            else if (context.Exception is HttpResponseException httpResponseException)
            {
                // If accepts JSON or XML:
                if (context.HttpContext.Request.Headers.Accept.Any(h => h == "application/json") || context.HttpContext.Request.Headers.Accept.Any(h => h == "application/xml" || h == "text/xml"))
                {
                    context.Result = new ObjectResult(new { isError = true, errorMessage = httpResponseException.Message })
                    {
                        StatusCode = (int)httpResponseException.StatusCode
                    };
                }
                else
                {
                    // Otherwise, set status code and reason phrase:
                    context.HttpContext.Response.StatusCode = (int)httpResponseException.StatusCode;
                    var responseFeature = context.HttpContext.Features.Get<IHttpResponseFeature>();
                    if (responseFeature != null)
                    {
                        responseFeature.ReasonPhrase = httpResponseException.Message;
                    }
                }

                // Set exception as handled:
                context.ExceptionHandled = true;
            }
        }
    }
}
