namespace Arebis.Core.AspNet.SqlServer.DomainSettings
{
    /// <summary>
    /// Source for SqlServer domain settings.
    /// </summary>
    public class SqlServerDomainSettingsSource
    {
        /// <summary>
        /// Constructs a new SqlServerDomainSettingsSource.
        /// </summary>
        public SqlServerDomainSettingsSource(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Connection string to the database containing the domain settings.
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
