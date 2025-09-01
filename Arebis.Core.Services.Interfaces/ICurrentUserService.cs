using System.Security.Claims;

namespace Arebis.Core.AspNet.Services
{
    /// <summary>
    /// Defines a service providing information about the current user.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// The current user, if any.
        /// </summary>
        ClaimsPrincipal? User { get; }

        /// <summary>
        /// Id of the current user, if any.
        /// </summary>
        string? UserId { get; }

        /// <summary>
        /// User name of the current user, if any.
        /// </summary>
        string? UserName { get; }

        /// <summary>
        /// First name or given name of the current user, if any and if set.
        /// </summary>
        string? FirstName { get; }

        /// <summary>
        /// Last name or surname of the current user, if any and if set.
        /// </summary>
        string? LastName { get; }

        /// <summary>
        /// Gender of the current user, if any and if set.
        /// Possible values include "male", "female", "non-binary", "other" and "unknown".
        /// </summary>
        string? Gender { get; }

        /// <summary>
        /// Email of the current user, if any and if set.
        /// </summary>
        string? Email { get; }

        /// <summary>
        /// Mobile phone of the current user, if any and if set.
        /// </summary>
        string? MobilePhone { get; }

        /// <summary>
        /// Roles of the current user, if any.
        /// </summary>
        IEnumerable<string> Roles { get; }
    }
}