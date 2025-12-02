using Arebis.Core.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// A FileSystemWatcher based monitor for localization resource persistor changes.
    /// To install as Singleton service.
    /// </summary>
    public class FileSystemLocalizationResourcePersistorMonitor : IDisposable
    {
        private readonly ILocalizationResourcePersistor localizationResourcePersistor;
        private readonly IOptions<LocalizationOptions> localizationOptions;
        private readonly ILogger<FileSystemLocalizationResourcePersistorMonitor> logger;
        private FileSystemWatcher? fileSystemWatcher = null;

        /// <summary>
        /// Constructs a FileSystemLocalizationResourcePersistorMonitor.
        /// </summary>
        public FileSystemLocalizationResourcePersistorMonitor(ILocalizationResourcePersistor localizationResourcePersistor, IOptions<LocalizationOptions> localizationOptions, ILogger<FileSystemLocalizationResourcePersistorMonitor> logger)
        {
            this.localizationResourcePersistor = localizationResourcePersistor;
            this.localizationOptions = localizationOptions;
            this.logger = logger;
            if (this.localizationOptions.Value.CacheFileName != null)
            {
                var filename = Environment.ExpandEnvironmentVariables(this.localizationOptions.Value.CacheFileName);
                var file = new FileInfo(filename);
                this.fileSystemWatcher = new FileSystemWatcher(file.Directory!.FullName, file.Name);
                this.fileSystemWatcher.IncludeSubdirectories = false;
                this.fileSystemWatcher.EnableRaisingEvents = true;
                this.fileSystemWatcher.Changed += FileSystemWatcher_Changed;
                this.fileSystemWatcher.Created += FileSystemWatcher_Created;
            }
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            logger.LogInformation("Detected creation of localization cache file.");
            this.localizationResourcePersistor.Changed(e);
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            logger.LogInformation("Detected change in localization cache file.");
            this.localizationResourcePersistor.Changed(e);
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            if (this.fileSystemWatcher != null)
            {
                this.fileSystemWatcher.Dispose();
                this.fileSystemWatcher = null;
            }
        }
    }
}
