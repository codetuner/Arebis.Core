using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.Services
{
    /// <summary>
    /// Service providing information about the current user based on the current HttpContext.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Constructs the current user service based on the current HttpContext.
        /// </summary>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public ClaimsPrincipal? User => this.httpContextAccessor.HttpContext?.User;

        /// <inheritdoc/>
        public string? UserId => this.httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        /// <inheritdoc/>
        public string? UserName => this.httpContextAccessor.HttpContext?.User?.Identity?.Name;

        /// <inheritdoc/>
        public string? FirstName => this.httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.GivenName)?.Value;

        /// <inheritdoc/>
        public string? LastName => this.httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.Surname)?.Value;

        /// <inheritdoc/>
        public string? Gender => this.httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.Gender)?.Value;

        /// <inheritdoc/>
        public string? Email => this.httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

        /// <inheritdoc/>
        public string? MobilePhone => this.httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.MobilePhone)?.Value;

        /// <inheritdoc/>
        public IEnumerable<string> Roles => this.httpContextAccessor.HttpContext?.User?.FindAll(System.Security.Claims.ClaimTypes.Role).Select(c => c.Value) ?? [];
    }
}
