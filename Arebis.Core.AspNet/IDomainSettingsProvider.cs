namespace Arebis.Core.AspNet
{
    /// <summary>
    /// Defines a provider for domain settings.
    /// </summary>
    public interface IDomainSettingsProvider
    {
        /// <summary>
        /// Retrieves the domain settings for the current domain as a dictionary of key-value pairs.
        /// </summary>
        Task<IDictionary<string, string?>> GetDomainSettingsAsync(CancellationToken ct = default);

        /// <summary>
        /// Define a domain alias.
        /// </summary>
        /// <param name="domainName">The domain name.</param>
        /// <param name="aliasFor">The domain name it is an alias for.</param>
        /// <param name="ct">Cancellation token.</param>
        Task SetDomainAliasAsync(string domainName, string aliasFor, CancellationToken ct = default);

        /// <summary>
        /// Set domain settings. Overwrite existing settings.
        /// </summary>
        /// <param name="domainName">The domain name.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="ct">Cancellation token.</param>
        Task SetDomainSettingsAsync(string domainName, IDictionary<string, string?> settings, CancellationToken ct = default);

        /// <summary>
        /// Deletes domain alias or settings.
        /// </summary>
        /// <param name="domainName">The domain name.</param>
        /// <param name="ct">Cancellation token.</param>
        Task DeleteDomainAsync(string domainName, CancellationToken ct = default);
    }
}
