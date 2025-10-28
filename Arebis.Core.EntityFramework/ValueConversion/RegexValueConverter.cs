using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// A value converter that stores a Regex as string.
    /// </summary>
    public class RegexValueConverter : ValueConverter<Regex, string>
    {
        /// <inheritdoc/>
        public RegexValueConverter()
            : base(
                value => value.ToString(),
                value => new Regex(value),
                null)
        { }

        /// <inheritdoc/>
        public RegexValueConverter(RegexOptions options)
            : base(
                value => value.ToString(),
                value => new Regex(value, options),
                null)
        { }
    }
}
