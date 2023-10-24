using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Arebis.Core.AspNet.Mvc.TagHelpers
{
    /// <summary>
    /// A Page-size Select tag-helper.
    /// </summary>
    [HtmlTargetElement("pagesize-select", Attributes = "asp-for")]
    public class PageSizeSelectTagHelper : SelectTagHelper
    {
        /// <summary>
        /// Constructs a PageSizeSelectTagHelper instance.
        /// </summary>
        public PageSizeSelectTagHelper(IHtmlGenerator generator) : base(generator)
        { }

        /// <summary>
        /// Space-separated list of available sizes.
        /// </summary>
        [HtmlAttributeName("sizes")]
        public string Sizes { get; set; } = "5 10 25 50 100 250";

        /// <summary>
        /// Initializes the TagHelper.
        /// </summary>
        public override void Init(TagHelperContext context)
        {
            var sizes = this.Sizes.Split(' ').Select(s => Convert.ToInt32(s)).ToList();
            var value = Convert.ToInt32(For.Model);
            if (!sizes.Contains(value)) sizes.Add(value);
            var values = sizes.Select(s => new SelectListItem() { Value = s.ToString(), Text = s.ToString(), Selected = (s == value) }).ToList();

            this.Items = values;

            base.Init(context);
        }

        /// <summary>
        /// Processes the TagHelper.
        /// </summary>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";
            base.Process(context, output);
        }
    }
}
