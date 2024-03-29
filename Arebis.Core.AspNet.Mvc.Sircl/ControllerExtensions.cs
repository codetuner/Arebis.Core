﻿using Microsoft.AspNetCore.Mvc;

namespace Arebis.Core.AspNet.Mvc.Sircl
{
    /// <summary>
    /// ASP.NET MVC Controller extension methods to support Sircl.
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Redirects to the given resource. For a partial (Sircl) request, redirection is done with status code 204.
        /// Otherwise a regular redirect with status code 302 is done.
        /// </summary>
        public static IActionResult SirclRedirectToAction(this Controller controller, string actionName)
        {
            return SirclRedirect(controller, controller.Url.Action(actionName)!);
        }

        /// <summary>
        /// Redirects to the given resource. For a partial (Sircl) request, redirection is done with status code 204.
        /// Otherwise a regular redirect with status code 302 is done.
        /// </summary>
        public static IActionResult SirclRedirectToAction(this Controller controller, string actionName, object actionValues)
        {
            return SirclRedirect(controller, controller.Url.Action(actionName, actionValues)!);
        }

        /// <summary>
        /// Redirects to the given resource. For a partial (Sircl) request, redirection is done with status code 204.
        /// Otherwise a regular redirect with status code 302 is done.
        /// </summary>
        public static IActionResult SirclRedirectToAction(this Controller controller, string actionName, string controllerName)
        {
            return SirclRedirect(controller, controller.Url.Action(actionName, controllerName)!);
        }

        /// <summary>
        /// Redirects to the given resource. For a partial (Sircl) request, redirection is done with status code 204.
        /// Otherwise a regular redirect with status code 302 is done.
        /// </summary>
        public static IActionResult SirclRedirectToAction(this Controller controller, string actionName, string controllerName, object actionValues)
        {
            return SirclRedirect(controller, controller.Url.Action(actionName, controllerName, actionValues)!);
        }

        /// <summary>
        /// Redirects to the given resource. For a partial (Sircl) request, redirection is done with status code 204.
        /// Otherwise a regular redirect with status code 302 is done.
        /// </summary>
        public static IActionResult SirclRedirect(this Controller controller, string url)
        {
            if (controller.Request.IsSirclPartial())
            {
                controller.Response.Headers["Location"] = url;
                return controller.StatusCode(204);
            }
            else
            {
                return controller.Redirect(url);
            }
        }

        /// <summary>
        /// Sets the X-Sircl-History header to instruct back navigation and returns a 204 (NoContent).
        /// </summary>
        public static IActionResult SirclBack(this Controller controller, bool allowCaching = true, bool allowClosing = true, string? allowClosingPrompt = null)
        {
            controller.Response.Headers["X-Sircl-History"] = allowCaching ? "back" : "back-uncached";
            if (allowClosing)
            {
                if (allowClosingPrompt == null)
                {
                    controller.Response.Headers["X-Sircl-History-AllowClose"] = "true";
                }
                else
                {
                    controller.Response.Headers["X-Sircl-History-AllowClose"] = allowClosingPrompt;
                }
            }
            return controller.StatusCode(204);
        }

        /// <summary>
        /// Sets the X-Sircl-History header to instruct a main target reload and returns a 204 (NoContent).
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="fullPage">Whether to perform a full page reload.</param>
        public static IActionResult SirclRefresh(this Controller controller, bool fullPage = false)
        {
            controller.Response.Headers["X-Sircl-History"] = "reload";
            if (fullPage)
            {
                controller.Response.Headers["X-Sircl-Target"] = "_self";
            }
            return controller.StatusCode(204);
        }

        /// <summary>
        /// Sets the X-Sircl-History-Replace header to display a different URL in the client.
        /// </summary>
        public static void SirclSetUrl(this Controller controller, string url)
        {
            controller.Response.Headers["X-Sircl-History-Replace"] = url;
        }

        /// <summary>
        /// Sets the X-Sircl-Target header to specify or override the target.
        /// </summary>
        public static void SirclSetTarget(this Controller controller, string target)
        {
            controller.Response.Headers["X-Sircl-Target"] = target;
        }

        /// <summary>
        /// Sets the X-Sircl-Target-Method header to instruct how to full the target.
        /// </summary>
        public static void SirclSetTargetMethodAppend(this Controller controller)
        {
            SirclSetTargetMethod(controller, "append");
        }

        /// <summary>
        /// Sets the X-Sircl-Target-Method header to instruct how to full the target.
        /// </summary>
        public static void SirclSetTargetMethodContent(this Controller controller)
        {
            SirclSetTargetMethod(controller, "content");
        }

        /// <summary>
        /// Sets the X-Sircl-Target-Method header to instruct how to full the target.
        /// </summary>
        public static void SirclSetTargetMethodPrepend(this Controller controller)
        {
            SirclSetTargetMethod(controller, "prepend");
        }

        /// <summary>
        /// Sets the X-Sircl-Target-Method header to instruct how to full the target.
        /// </summary>
        public static void SirclSetTargetMethodReplace(this Controller controller)
        {
            SirclSetTargetMethod(controller, "replace");
        }

        /// <summary>
        /// Sets the X-Sircl-Target-Method header to instruct how to full the target.
        /// Supported values: "content", "prepend", "append" or "replace".
        /// </summary>
        public static void SirclSetTargetMethod(this Controller controller, string method)
        {
            controller.Response.Headers["X-Sircl-Target-Method"] = method;
        }

        /// <summary>
        /// Sets the X-Sircl-Load header to instruct (re)loading the given selection.
        /// </summary>
        public static void SirclSetLoad(this Controller controller, string cssSelector)
        {
            controller.Response.Headers["X-Sircl-Load"] = cssSelector;
        }

        /// <summary>
        /// Sets the X-Sircl-Reload-After header to the given value to instruct automatic reloading.
        /// </summary>
        public static void SirclSetReloading(this Controller controller, int seconds)
        {
            controller.Response.Headers["X-Sircl-Reload-After"] = seconds.ToString();
        }

        /// <summary>
        /// Sets the X-Sircl-Reload-After header to "0" to abort repeated reloadings.
        /// </summary>
        public static void SirclStopRefreshing(this Controller controller)
        {
            SirclSetReloading(controller, 0);
        }

        /// <summary>
        /// Sets the X-Sircl-Form-Changed header to "true" to mark the form as changed.
        /// </summary>
        public static void SirclSetFormChanged(this Controller controller)
        {
            controller.Response.Headers["X-Sircl-Form-Changed"] = "true";
        }

        /// <summary>
        /// Sets the X-Sircl-Document-Title header to specify the document title.
        /// </summary>
        public static void SirclSetDocumentTitle(this Controller controller, string title)
        {
            controller.Response.Headers["X-Sircl-Document-Title"] = title;
        }

        /// <summary>
        /// Sets the X-Sircl-Alert-Message header to render a message in an alert box.
        /// </summary>
        public static void SirclAlert(this Controller controller, string message)
        {
            controller.Response.Headers["X-Sircl-Alert-Message"] = message;
        }

        /// <summary>
        /// Sets the X-Sircl-Toastr header to render a Toastr message.
        /// </summary>
        public static void SirclToastrSuccess(this Controller controller, string message, string? title = null)
        {
            SirclToastr(controller, "success", message, title);
        }

        /// <summary>
        /// Sets the X-Sircl-Toastr header to render a Toastr message.
        /// </summary>
        public static void SirclToastrInfo(this Controller controller, string message, string? title = null)
        {
            SirclToastr(controller, "info", message, title);
        }

        /// <summary>
        /// Sets the X-Sircl-Toastr header to render a Toastr message.
        /// </summary>
        public static void SirclToastrWarning(this Controller controller, string message, string? title = null)
        {
            SirclToastr(controller, "warning", message, title);
        }

        /// <summary>
        /// Sets the X-Sircl-Toastr header to render a Toastr message.
        /// </summary>
        public static void SirclToastrError(this Controller controller, string message, string? title = null)
        {
            SirclToastr(controller, "error", message, title);
        }

        /// <summary>
        /// Sets the X-Sircl-Toastr header to render a Toastr message.
        /// Type of message is "success", "info", "warning" or "error".
        /// </summary>
        public static void SirclToastr(this Controller controller, string type, string message, string? title = null)
        {
            controller.Response.Headers["X-Sircl-Toastr"] = type + "|" + message + (!String.IsNullOrEmpty(title) ? "|" + title : "");
        }
    }
}
