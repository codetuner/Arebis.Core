using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Data
{
    /// <summary>
    /// DataReader extension methods.
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Returns the value as int or null if DbNull.
        /// </summary>
        public static int? GetInt32OrNull(this IDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
                return null;
            else
                return reader.GetInt32(ordinal);
        }

        /// <summary>
        /// Returns the value as string or null if DbNull.
        /// </summary>
        public static string? GetStringOrNull(this IDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
                return null;
            else
                return reader.GetString(ordinal);
        }

        /// <summary>
        /// Returns the value as DateTime or null if DbNull.
        /// </summary>
        public static DateTime? GetDateTimeOrNull(this IDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
                return null;
            else
                return reader.GetDateTime(ordinal);
        }

        /// <summary>
        /// Returns the value as byte array or null if DbNull.
        /// </summary>
        public static byte[]? GetBytesOrNull(this IDataReader reader, int ordinal)
        {
            if (reader.IsDBNull(ordinal))
                return null;
            else
            {
                var len = reader.GetBytes(ordinal, 0, null, 0, 0);
                if (len > Int32.MaxValue) throw new Exception("Binary data too long for GetBytesOrNull extension method's implementation.");
                var buffer = new byte[len];
                reader.GetBytes(ordinal, 0, buffer, 0, (int)len);
                return buffer;
            }
        }
    }
}
