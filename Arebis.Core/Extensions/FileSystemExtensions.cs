using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// Extensions for file system operations.
    /// </summary>
    public static class FileSystemExtensions
    {
        /// <summary>
        /// Returns a unique name for the specified file by appending a number to the file name if it already exists.
        /// </summary>
        [return: NotNullIfNotNull(nameof(fileInfo))]
        public static FileInfo? ToUniqueName(this FileInfo? fileInfo)
        { 
            if (fileInfo == null) return null;
            if (!fileInfo.Exists) return fileInfo;

            var baseName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            var extension = fileInfo.Extension;
            var counter = 1;
            while (true)
            { 
                var uniqueName = Path.Combine(fileInfo.DirectoryName ?? string.Empty, $"{baseName} ({counter}){extension}");
                if (!File.Exists(uniqueName))
                {
                    return new FileInfo(uniqueName);
                }
                counter++;
            }
        }
    }
}
