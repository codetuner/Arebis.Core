using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Arebis.Core.Source;
using Microsoft.AspNetCore.Http;

namespace Arebis.Core.AspNet.Mvc.TagHelpers
{
    /// <summary>
    /// Authorization taghelper.
    /// Add the "asp-authorize" attribute alone or in combination with "asp-roles" or "asp-policy" attributes.
    /// Set authentication schemes with "asp-authentication-schemes".
    /// </summary>
    [CodeSource("https://www.davepaquette.com/archive/2017/11/05/authorize-tag-helper.aspx", "Dave Paquette")]
    [HtmlTargetElement(Attributes = "asp-authorize")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-policy")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-roles")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-authentication-schemes")]
    public class AuthorizationPolicyTagHelper : TagHelper, IAuthorizeData
    {
        private readonly IAuthorizationPolicyProvider _policyProvider;
        private readonly IPolicyEvaluator _policyEvaluator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructs an AuthorizationPolicyTagHelper.
        /// </summary>
        public AuthorizationPolicyTagHelper(IHttpContextAccessor httpContextAccessor, IAuthorizationPolicyProvider policyProvider, IPolicyEvaluator policyEvaluator)
        {
            _httpContextAccessor = httpContextAccessor;
            _policyProvider = policyProvider;
            _policyEvaluator = policyEvaluator;
        }

        /// <summary>
        /// Whether authorization is required. If set, user must be logged in.
        /// </summary>
        [HtmlAttributeName("asp-authorize")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the policy name that determines access to the HTML block.
        /// </summary>
        [HtmlAttributeName("asp-policy")]
        public string? Policy { get; set; }

        /// <summary>
        /// Gets or sets a comma delimited list of roles that are allowed to access the HTML  block.
        /// </summary>
        [HtmlAttributeName("asp-roles")]
        public string? Roles { get; set; }

        /// <summary>
        /// Gets or sets a comma delimited list of schemes from which user information is constructed.
        /// </summary>
        [HtmlAttributeName("asp-authentication-schemes")]
        public string? AuthenticationSchemes { get; set; }

        /// <summary>
        /// Processes the taghelper.
        /// </summary>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!Enabled) return;

            var policy = await AuthorizationPolicy.CombineAsync(_policyProvider, new[] { this });

            var authenticateResult = await _policyEvaluator.AuthenticateAsync(policy!, _httpContextAccessor.HttpContext!);

            var authorizeResult = await _policyEvaluator.AuthorizeAsync(policy!, authenticateResult, _httpContextAccessor.HttpContext!, null);

            if (!authorizeResult.Succeeded)
            {
                output.SuppressOutput();
            }
        }
    }
}