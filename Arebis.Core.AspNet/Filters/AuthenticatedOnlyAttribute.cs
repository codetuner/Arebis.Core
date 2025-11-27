using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace Arebis.Core.AspNet.Filters
{
    /// <summary>
    /// Mark the action as available only to authenticated users.
    /// For anymous users, the search for a matching action will continue.
    /// No redirection to login page is performed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AuthenticatedOnlyAttribute : ActionMethodSelectorAttribute
    {
        /// <inheritdoc/>
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            var user = routeContext.HttpContext.User;
            return user?.Identity?.IsAuthenticated == true;
        }
    }
}
