using Microsoft.AspNetCore.Mvc.RazorPages;
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
        // See: http://blog.techdominator.com/article/using-html-helper-inside-tag-helpers.html
        //
        //private IHtmlHelper htmlHelper;
        //
        //public PaginationNavTagHelper(IHtmlHelper htmlHelper)
        //{
        //    this.htmlHelper = htmlHelper;
        //}
        //
        //[ViewContext]
        //[HtmlAttributeNotBound]
        //public ViewContext ViewContext { get; set; }        
        //
        //public override void Process(TagHelperContext context, TagHelperOutput output)
        //{
        //    (htmlHelper as IViewContextAware).Contextualize(ViewContext);
        //    var id = htmlHelper.GenerateIdFromName(AspFor.Name);
        //    ...
        //}


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
        /// Styling to apply ("default", "small",...)
        /// </summary>
        [HtmlAttributeName("style-template")]
        public string? StyleTemplate { get; set; }

        /// <summary>
        /// Whether to support keyboard shortcuts.
        /// </summary>
        [HtmlAttributeName("keyboard")]
        public bool Keyboard { get; set; } = false;

        /// <summary>
        /// Text on the previous page link.
        /// </summary>
        [HtmlAttributeName("previous-text")]
        public string? PreviousText { get; set; }

        /// <summary>
        /// Text on the next page link.
        /// </summary>
        [HtmlAttributeName("next-text")]
        public string? NextText { get; set; }

        /// <summary>
        /// Process the TagHelper.
        /// </summary>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var name = For!.Name;
            var value = (int)For!.Model;

            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;

            string? paginationStyle = null;
            string? pageItemStyle = null;
            string? pageItemArrowStyle = null;
            string? pageItemLinkStyle = null;
            string? pageItemRadioStyle = null;
            if ("default".Equals(StyleTemplate))
            {
                pageItemStyle = "min-width: 48px; text-align: right;";
                pageItemArrowStyle = "min-width: 32px; text-align: center;";
                pageItemLinkStyle = "padding-left: 8px; padding-right: 12px;";
                pageItemRadioStyle = "display: none;";
            }
            else if ("small".Equals(StyleTemplate))
            {
                pageItemStyle = "min-width: 24px; text-align: right;";
                pageItemArrowStyle = "min-width: 24px; text-align: center;";
                pageItemLinkStyle = "padding-left: 4px; padding-right: 4px;";
                pageItemRadioStyle = "display: none;";
            }

            var builder = new StringBuilder();
            builder.Append($"<ul class=\"pagination\"{(paginationStyle == null ? "" : " style=\"" + paginationStyle + "\"")}>");
            WritePage(builder, name, value, (value == Min ? Min - 1 : value - 1), pageItemStyle, pageItemArrowStyle, pageItemRadioStyle, PreviousText ?? "&laquo;", !Keyboard ? null : "ArrowLeft");
            if (Max.HasValue)
            {
                if ((Max.Value - Min) < 7)
                {
                    for (int p = Min; p <= Max.Value; p++)
                    {
                        WritePage(builder, name, value, p, pageItemStyle, pageItemLinkStyle, pageItemRadioStyle, null, !Keyboard ? null : p == Min ? null /*Home*/ : p == Max.Value ? "End" : null);
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
                        WritePage(builder, name, value, pages[i], pageItemStyle, pageItemLinkStyle, pageItemRadioStyle, null, !Keyboard ? null : i == 0 ? null /*Home*/ : i == pages.Length - 1 ? "End" : null);
                    }
                }
                WritePage(builder, name, value, (value < Max ? value + 1 : Min - 1), pageItemStyle, pageItemArrowStyle, pageItemRadioStyle, NextText ?? "&raquo;", !Keyboard ? null : "ArrowRight");
            }
            else if (HasNextPage.HasValue)
            {
                WritePage(builder, name, value, value, pageItemStyle, pageItemLinkStyle, pageItemRadioStyle);
                WritePage(builder, name, value, (HasNextPage.Value ? value + 1 : Min - 1), pageItemStyle, pageItemArrowStyle, pageItemRadioStyle, NextText ?? "&raquo;", !Keyboard ? null : "ArrowRight");
            }
            else
            {
                WritePage(builder, name, value, (value + 1), pageItemStyle, pageItemArrowStyle, pageItemRadioStyle, NextText ?? "&raquo;", !Keyboard ? null : "ArrowRight");
            }
            builder.Append("</ul>");
            if (Keyboard == true) builder.Append($"<input type=\"radio\" name=\"{name}\" value=\"{Min}\" onkeydown-click=\"Home\" style=\"display: none;\" />");

            output.Content.SetHtmlContent(builder.ToString());
        }

        private void WritePage(StringBuilder builder, string name, int value, int page, string? pageItemStyle, string? pageItemLinkStyle, string? pageItemRadioStyle, string? text = null, string? shortCut = null)
        {
            var active = (page == value);
            builder.Append($"<li class=\"page-item{(active ? " active" : "")}{((page < Min) ? " disabled" : "")}\"{(pageItemStyle == null ? "" : " style=\"" + pageItemStyle + "\"")}>");
            builder.Append($"<label class=\"page-link\"{(pageItemLinkStyle == null ? "" : " style=\"" + pageItemLinkStyle + "\"")}>");
            if (page >= Min) builder.Append($"<input type=\"radio\" name=\"{name}\" value=\"{page}\"{((shortCut == null) ? "" : $" onkeydown-click=\"{shortCut}\"")}{(pageItemRadioStyle == null ? "" : " style=\"" + pageItemRadioStyle + "\"")} />");
            if (text != null) builder.Append(text);
            else if (page < Min) builder.Append("...");
            else builder.Append(page);
            builder.Append($"</label>");
            builder.Append($"</li>");
        }
    }
}
