using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Arebis.Core.AspNet.Mvc.TagHelpers
{
    /// <summary>
    /// Condition taghelper. Determines whether to render depending on the condition outcome.
    /// </summary>
    [HtmlTargetElement(Attributes = "asp-if")]
    [HtmlTargetElement(Attributes = "asp-elsefor")]
    public class ConditionalTagHelper : TagHelper
    {
        /// <summary>
        /// Optional id of this element.
        /// </summary>
        [HtmlAttributeName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Condition to be true for this element to be rendered.
        /// </summary>
        [HtmlAttributeName("asp-if")]
        public bool? Condition { get; set; }

        /// <summary>
        /// Id of the element holding the original if condition this is an else branch of.
        /// </summary>
        [HtmlAttributeName("asp-elsefor")]
        public string? ElseFor { get; set; }

        /// <summary>
        /// ViewContext for this taghelper.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; } = null!;

        /// <summary>
        /// Processes the taghelper.
        /// </summary>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var processed = false;

            var id = ElseFor ?? Id;

            if (ElseFor is not null)
            {
                processed = ((bool?)ViewContext.ViewData[$"{nameof(ConditionalTagHelper)}:{id}:Processed"] ?? false);
            }

            if (processed || (Condition.HasValue && Condition.Value == false))
            {
                output.SuppressOutput();
            }
            else if (id is not null)
            {
                ViewContext.ViewData[$"{nameof(ConditionalTagHelper)}:{id}:Processed"] = true;
            }
        }
    }
}