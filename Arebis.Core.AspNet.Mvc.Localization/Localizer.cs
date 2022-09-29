using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Arebis.Core.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// Implementation of generic Localizer using Localization Source infrastructure.
    /// To be registered as Singleton service for types <see cref="IStringLocalizer{T}"/> and <see cref="IHtmlLocalizer{T}"/>.
    /// </summary>
    public class Localizer<T> : Localizer, IStringLocalizer<T>, IHtmlLocalizer<T>
    {
        /// <summary>
        /// Constructs a Localizer.
        /// </summary>
        public Localizer(IOptions<LocalizationOptions> localizationOptions, ILocalizationResourceProvider resourceProvider, IHttpContextAccessor? httpContextAccessor, ILogger<Localizer> logger)
            : base(localizationOptions, resourceProvider, httpContextAccessor, logger)
        {
            this.ContextTypeName = this.GetType().GenericTypeArguments[0].FullName;
        }
    }

    /// <summary>
    /// Implementation of non-generic Localizer using Localization Source infrastructure.
    /// To be registered as Singleton service for types <see cref="IStringLocalizer"/>, <see cref="IHtmlLocalizer"/> and <see cref="IViewLocalizer"/>.
    /// </summary>
    public class Localizer : IStringLocalizer, IHtmlLocalizer, IViewLocalizer
    {
        /// <summary>
        /// Constructs a Localizer.
        /// </summary>
        public Localizer(IOptions<LocalizationOptions> localizationOptions, ILocalizationResourceProvider localizationResourceProvider, IHttpContextAccessor? httpContextAccessor, ILogger<Localizer> logger)
        {
            this.localizationOptions = localizationOptions;
            this.localizationResourceProvider = localizationResourceProvider;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;

            this.RouteDataStringKey = this.localizationOptions.Value.RouteDataStringKey ?? "culture";
            this.UIRouteDataStringKey = this.localizationOptions.Value.UIRouteDataStringKey ?? "ui-culture";
        }

        private readonly ILocalizationResourceProvider localizationResourceProvider;
        private readonly IOptions<LocalizationOptions> localizationOptions;
        private readonly IHttpContextAccessor? httpContextAccessor;
        private readonly ILogger<Localizer> logger;

        /// <summary>
        /// The route key name referring to the culture route segment.
        /// </summary>
        public string RouteDataStringKey { get; set; }

        /// <summary>
        /// The route key name referring to the UI culture route segment.
        /// </summary>
        public string UIRouteDataStringKey { get; set; }

        /// <summary>
        /// The context type of the localizer.
        /// This is the generic type parameter of the generic <see cref="Localizer{T}"/>.
        /// </summary>
        public string? ContextTypeName { get; protected set; }

        LocalizedString IStringLocalizer.this[string name]
        {
            get
            {
                var value = GetRawString(name);
                return (value != null) ? new LocalizedString(name, value) : new LocalizedString(name, name, true);
            }
        }

        LocalizedHtmlString IHtmlLocalizer.this[string name]
        {
            get
            {
                var value = GetRawString(name);
                return (value != null) ? new LocalizedHtmlString(name, value) : new LocalizedHtmlString(name, name, true);
            }
        }

        LocalizedString IStringLocalizer.this[string name, params object[] arguments]
        {
            get
            {
                var value = GetRawString(name);
                return (value != null) ? new LocalizedString(name, String.Format(value, arguments)) : new LocalizedString(name, String.Format(name, arguments), true);
            }
        }

        LocalizedHtmlString IHtmlLocalizer.this[string name, params object[] arguments]
        {
            get
            {
                var value = GetRawString(name);
                return (value != null) ? new LocalizedHtmlString(name, value, false, arguments) : new LocalizedHtmlString(name, String.Format(name, arguments), true);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public LocalizedString GetString(string name)
        {
            var value = GetRawString(name);
            return (value != null) ? new LocalizedString(name, value) : new LocalizedString(name, name, true);
        }

        /// <inheritdoc/>
        public LocalizedString GetString(string name, params object[] arguments)
        {
            var value = GetRawString(name);
            return (value != null) ? new LocalizedString(name, String.Format(value, arguments)) : new LocalizedString(name, String.Format(name, arguments), true);
        }

        /// <summary>
        /// Gets the raw string (without arguments substituted- for the given localiation key name.
        /// </summary>
        /// <param name="name">Name of the localization key.</param>
        /// <param name="viewContext">ViewContext, if any.</param>
        public string? GetRawString(string name, ViewContext? viewContext = null)
        {
            if (this.localizationOptions.Value.AllowLocalizeFormat)
            {
                var userFormat = this.httpContextAccessor?.HttpContext?.Request.Query["__LocalizeFormat"].FirstOrDefault();
                if (userFormat != null)
                {
                    return String.Format(userFormat, name).Replace("{", "{{").Replace("}", "}}");
                }
            }

            // Retrieves the request path:
            string? path = null;
            if (this.httpContextAccessor?.HttpContext != null)
            {
                // Get cached path on subsequent invocations on same request:
                if (this.httpContextAccessor.HttpContext.Items.TryGetValue("_Localization_Path", out var pathobj))
                {
                    path = (string?)pathobj;
                }
                else
                {
                    // Else, get path from request:
                    path = this.httpContextAccessor.HttpContext.Request.Path.Value;
                    if (path != null)
                    {
                        // Make sure path ends on "/":
                        path += "/";
                        // If path contains culture, remove it:
                        if (this.httpContextAccessor.HttpContext.Request.RouteValues.TryGetValue(this.UIRouteDataStringKey, out var routeuiculture))
                        {
                            path = path.Replace("/" + routeuiculture + "/", "/");
                        }
                        else if (this.httpContextAccessor.HttpContext.Request.RouteValues.TryGetValue(this.RouteDataStringKey, out var routeculture))
                        {
                            path = path.Replace("/" + routeculture + "/", "/");
                        }
                    }

                    // Cache path on context for subsequent invocations on same request:
                    this.httpContextAccessor.HttpContext.Items["_Localization_Path"] = path;
                }
            }

            // Retrieve request UICulture:
            var uiculture = this.httpContextAccessor?.HttpContext?.Features.Get<IRequestCultureFeature>()?.RequestCulture.UICulture ?? System.Threading.Thread.CurrentThread.CurrentUICulture;

            // Informational logging every requested key:
            logger.LogDebug("Requesting localization key \"{name}\" for path \"{path}\".", name, path);

            // Search for matching resource; if not found, return null:
            var res = this.localizationResourceProvider.GetResource(name, path);
            if (res == null)
            {
                logger.LogWarning("No localization key found for \"{name}\", path \"{path}\".", name, path);
                return null;
            }

            // Search for resource's culture string; if not found, return null:
            string? str = null;
            var candidateCultures = GetCandidateCulturesFor(uiculture);
            foreach (var c in candidateCultures)
            {
                if (c == null)
                {
                    logger.LogWarning("No localization \"{uiculture}\" value found for \"{name}\", path \"{path}\".", uiculture, name, path);
                }
                if (res.Values.TryGetValue(c ?? this.localizationResourceProvider.GetDefaultCulture(), out str)) break;
            }
            if (str == null) return null;

            // Substitute extra fields if any:
            if (res.SubstitutionFields != null)
            {
                foreach (var substKey in res.SubstitutionFields)
                {
                    if ("{{culture:name}}".Equals(substKey))
                    {
                        str = str.Replace(substKey, (this.httpContextAccessor?.HttpContext?.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture ?? System.Threading.Thread.CurrentThread.CurrentCulture).Name);
                    }
                    else if ("{{uiculture:name}}".Equals(substKey))
                    {
                        str = str.Replace(substKey, uiculture.Name);
                    }
                    else if (substKey.StartsWith("{{route:"))
                    {
                        var routesegment = substKey[8..^2];
                        if (this.httpContextAccessor?.HttpContext?.Request.RouteValues.TryGetValue(routesegment, out var value) ?? false)
                        {
                            str = str.Replace(substKey, value?.ToString() ?? String.Empty);
                        }
                        else
                        {
                            str = str.Replace(substKey, String.Empty);
                            this.logger.LogWarning("No Route section \"{routesegment}\" found while localizing \"{name}\".", routesegment, name);
                        }
                    }
                    else if ("{{user:name}}".Equals(substKey))
                    {
                        var username = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                        str = str.Replace(substKey, username);
                    }
                    else if (substKey.StartsWith("{{localizer:"))
                    {
                        var reskey = substKey[12..^2];
                        var resval = this.GetRawString(reskey);
                        if (resval != null)
                        {
                            str = str.Replace(substKey, resval);
                        }
                    }
                    else if (substKey.StartsWith("{{model:") && viewContext != null)
                    {
                        // Parse format "{{model:<path>[:<format>]}}":
                        var parts = substKey[8..^2].Split(':');
                        // Get value from expression path:
                        var value = GetValueFromExpressionPath(viewContext.ViewData.Model, parts[0]);
                        // Render value:
                        if (parts.Length >= 2)
                        {
                            str = str.Replace(substKey, String.Format("{0:" + parts[1] + "}", value));
                        }
                        else
                        {
                            str = str.Replace(substKey, value?.ToString());
                        }
                    }
                    else if (substKey.StartsWith("{{view:") && viewContext != null)
                    {
                        // Parse format "{{view:<path>[:<format>]}}":
                        var parts = substKey[7..^2].Split(':');
                        // Get first element in expression path:
                        var pathparts = parts[0].Split('.', 2);
                        if (viewContext.ViewData.TryGetValue(pathparts[0], out var value))
                        {
                            // Get remainer of expression path:
                            if (pathparts.Length > 1) value = GetValueFromExpressionPath(value, pathparts[1]);
                            // Render value:
                            if (parts.Length >= 2)
                            {
                                str = str.Replace(substKey, String.Format("{0:" + parts[1] + "}", value));
                            }
                            else
                            {
                                str = str.Replace(substKey, value?.ToString());
                            }
                        }
                        else
                        {
                            str = str.Replace(substKey, String.Empty);
                            this.logger.LogWarning("No ViewData item with key \"{viewdatakey}\" found while localizing \"{name}\".", pathparts[0], name);
                        }
                    }
                }
            }

            return str;
        }

        private static object? GetValueFromExpressionPath(object? obj, string? propertyPath)
        {
            if (propertyPath == null) return obj;
            var props = propertyPath.Split('.');
            foreach (var prop in props)
            {
                obj = obj?.GetType().GetProperty(prop!, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)?.GetValue(obj);
                if (obj == null) return null;
            }
            return obj;
        }

        /// <summary>
        /// Return list of cultures to probe for, given the request culture.
        /// </summary>
        private string?[] GetCandidateCulturesFor(CultureInfo culture)
        {
            if (candidateCultures.TryGetValue(culture.Name, out var cultures))
            {
                // If list already cached, return from cache:
                return cultures;
            }
            else
            {
                // Build list cultures based on the request culture, going up parents:
                var supportedCultureNames = this.localizationResourceProvider.GetCultures();
                var defaultCultureName = this.localizationResourceProvider.GetDefaultCulture();
                var cultureList = new List<string?>();
                var c = culture;
                while (true)
                {
                    if (supportedCultureNames.Contains(c.Name))
                    {
                        cultureList.Add(c.Name);
                    }
                    c = c.Parent;
                    if (c.Name.Length == 0) break;
                }
                // Also add default culture:
                if (!cultureList.Contains(defaultCultureName)) cultureList.Add(null);
                // Store in cahce and return:
                return candidateCultures[culture.Name] = cultureList.ToArray();
            }
        }

        /// <summary>
        /// Cache for list of cultures to probe given the request culture.
        /// </summary>
        private readonly static Dictionary<string, string?[]> candidateCultures = new();
    }
}
