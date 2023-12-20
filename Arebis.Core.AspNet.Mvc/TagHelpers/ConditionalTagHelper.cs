using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Arebis.Core.Source;
using Microsoft.AspNetCore.Http;

namespace Arebis.Core.AspNet.Mvc.TagHelpers
{
    /// <summary>
    /// Condition taghelper. Determines whether to render depending on the condition outcome.
    /// </summary>
    [HtmlTargetElement(Attributes = "asp-if")]
    public class ConditionalTagHelper : TagHelper
    {
        /// <summary>
        /// Gets or sets the policy name that determines access to the HTML block.
        /// </summary>
        [HtmlAttributeName("asp-if")]
        public bool Condition { get; set; }

        /// <summary>
        /// Processes the taghelper.
        /// </summary>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!Condition)
            {
                output.SuppressOutput();
            }
        }
    }
}