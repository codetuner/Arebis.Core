using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// A ViewContext aware ViewLocalizer implementation.
    /// </summary>
    public class ContextAwareViewLocalizer : IViewLocalizer, IViewContextAware
    {
        private readonly Localizer innerLocalizer;
        private ViewContext? viewContext;

        /// <summary>
        /// Constructs a ContextAwareViewLocalizer.
        /// </summary>
        public ContextAwareViewLocalizer(Localizer localizer)
        {
            this.innerLocalizer = localizer;
        }

        /// <summary>
        /// Sets the context of this Localizer.
        /// </summary>
        public void Contextualize(ViewContext viewContext)
        {
            this.viewContext = viewContext;
        }

        LocalizedHtmlString IHtmlLocalizer.this[string name]
        {
            get
            {
                var value = this.innerLocalizer.GetRawString(name, viewContext);
                return (value != null) ? new LocalizedHtmlString(name, value) : new LocalizedHtmlString(name, name, true);
            }
        }

        LocalizedHtmlString IHtmlLocalizer.this[string name, params object[] arguments]
        {
            get
            {
                var value = this.innerLocalizer.GetRawString(name, viewContext);
                return (value != null) ? new LocalizedHtmlString(name, value, false, arguments) : new LocalizedHtmlString(name, String.Format(name, arguments), true);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return ((IViewLocalizer)this.innerLocalizer).GetAllStrings();
        }

        /// <inheritdoc/>
        public LocalizedString GetString(string name)
        {
            var value = this.innerLocalizer.GetRawString(name, viewContext);
            return (value != null) ? new LocalizedString(name, value) : new LocalizedString(name, name, true);
        }

        /// <inheritdoc/>
        public LocalizedString GetString(string name, params object[] arguments)
        {
            var value = this.innerLocalizer.GetRawString(name, viewContext);
            return (value != null) ? new LocalizedString(name, String.Format(value, arguments)) : new LocalizedString(name, String.Format(name, arguments), true);
        }
    }
}
