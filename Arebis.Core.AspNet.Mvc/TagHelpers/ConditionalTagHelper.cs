using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Arebis.Core.AspNet.Mvc.TagHelpers
{
    /// <summary>
    /// Condition taghelper. Determines whether to render depending on the condition outcome.
    /// </summary>
    /// <example>
    /// An if can be expressed as follows:
    /// <code lang="html">
    /// &lt;div id="c1" asp-if="Model.Number &lt; 10"&gt;...less than 10...&lt;/div&gt;
    /// </code>
    /// The id attribute is optional but required when using elsefor, as in:
    /// <code lang="html">
    /// &lt;div asp-elsefor="c1" asp-if="Model.Number &lt; 100"&gt;...from 10 to 1000...&lt;/div&gt;
    /// </code>
    /// Note how an else-if branch uses asp-elsefor to refer to the original if condition, in combination with an asp-if. The final else branch would be:
    /// <code lang="html">
    /// &lt;div asp-elsefor="c1"&gt;...100 or more...&lt;/div&gt;
    /// </code>
    /// The tags hving asp-if and asp-elsefor attributes do not need to be subsequent, they can be anywhere in the page.
    /// </example>
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

            if (this.Id != null) output.Attributes.Add("id", this.Id);

            var conditionId = ElseFor ?? Id;

            if (ElseFor is not null)
            {
                processed = ((bool?)ViewContext.ViewData[$"{nameof(ConditionalTagHelper)}:{conditionId}:Processed"] ?? false);
            }

            if (processed || (Condition.HasValue && Condition.Value == false))
            {
                output.SuppressOutput();
            }
            else if (conditionId is not null)
            {
                ViewContext.ViewData[$"{nameof(ConditionalTagHelper)}:{conditionId}:Processed"] = true;
            }
        }
    }
}