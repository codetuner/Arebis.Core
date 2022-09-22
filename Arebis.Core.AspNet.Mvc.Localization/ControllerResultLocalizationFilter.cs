using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// Provides access from localization to the controller result (model and viewdata).
    /// </summary>
    public class ControllerResultLocalizationFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Stores the controller result on the HttpContext Items.
        /// </summary>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Items["_Localization_ControllerResult"] = context.Result;
        }
    }
}
