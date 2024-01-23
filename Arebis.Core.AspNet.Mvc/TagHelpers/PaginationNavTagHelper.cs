using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.Mvc.TagHelpers
{
    /// <summary>
    /// A Pagination tag-helper.
    /// </summary>
    [HtmlTargetElement("pagination-nav", Attributes = "asp-for, max")]
    [HtmlTargetElement("pagination-nav", Attributes = "asp-for, hasnext")]
    public class PaginationNavTagHelper : TagHelper
    {
        /// <summary>
        /// The model expression holding the current page number.
        /// </summary>
        [HtmlAttributeName("asp-for")]
        public ModelExpression? For { get; set; }

        /// <summary>
        /// The minimum page number (defaults to 1).
        /// </summary>
        [HtmlAttributeName("min")]
        public int Min { get; set; } = 1;

        /// <summary>
        /// If set, the maximum page number (last page's number).
        /// </summary>
        [HtmlAttributeName("max")]
        public int? Max { get; set; }

        /// <summary>
        /// Whether there is a next page.
        /// </summary>
        [HtmlAttributeName("hasnext")]
        public bool? HasNextPage { get; set; }

        /// <summary>
        /// Process the TagHelper.
        /// </summary>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var name = For!.Name;
            var value = (int)For!.Model;

            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;

            var builder = new StringBuilder();
            builder.Append("<ul class=\"pagination\">");
            WritePage(builder, name, value, (value == Min ? Min - 1 : value - 1), "&laquo;", "ArrowLeft");
            if (Max.HasValue)
            {
                if ((Max.Value - Min) < 7)
                {
                    for (int p = Min; p <= Max.Value; p++)
                    {
                        WritePage(builder, name, value, p);
                    }
                }
                else
                {
                    var pages = new int[] { value - 3, value - 2, value - 1, value, value + 1, value + 2, value + 3 };
                    if (pages[0] < Min)
                    {
                        var delta = Min - pages[0];
                        for (int i = 0; i < pages.Length; i++) pages[i] += delta;
                    }
                    else if (pages[^1] > Max.Value)
                    {
                        var delta = pages[^1] - Max.Value;
                        for (int i = 0; i < pages.Length; i++) pages[i] -= delta;
                    }
                    if (pages[0] > Min)
                    {
                        pages[0] = Min;
                        pages[1] = Min - 1;
                    }
                    if (pages[^1] < Max.Value)
                    {
                        pages[^2] = Min - 1;
                        pages[^1] = Max.Value;
                    }
                    for (int i = 0; i < pages.Length; i++)
                    {
                        WritePage(builder, name, value, pages[i]);
                    }
                }
                WritePage(builder, name, value, (value < Max ? value + 1 : Min - 1), "&raquo;", "ArrowRight");
            }
            else if (HasNextPage.HasValue)
            {
                WritePage(builder, name, value, value);
                WritePage(builder, name, value, (HasNextPage.Value ? value + 1 : Min - 1), "&raquo;", "ArrowRight");
            }
            else
            {
                WritePage(builder, name, value, (value + 1), "&raquo;", "ArrowRight");
            }
            builder.Append("</ul>");

            output.Content.SetHtmlContent(builder.ToString());
        }

        private void WritePage(StringBuilder builder, string name, int value, int page, string? text = null, string? shortCut = null)
        {
            var active = (page == value);
            builder.Append($"<li class=\"page-item{(active ? " active" : "")}{((page < Min) ? " disabled" : "")}\">");
            builder.Append($"<label class=\"page-link\">");
            if (page >= Min) builder.Append($"<input type=\"radio\" name=\"{name}\" value=\"{page}\"{((shortCut == null) ? "" : $" onkeydown-click=\"{shortCut}\"")} />");
            if (text != null) builder.Append(text);
            else if (page < Min) builder.Append("...");
            else builder.Append(page);
            builder.Append($"</label>");
            builder.Append($"</li>");
        }
    }
}
