using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Source
{
    /// <summary>
    /// This class is to be instantiated with a using block to surround code that contains hardcoded portions and may need refactoring.
    /// This code itself does nothing, it is just a marker class.
    /// </summary>
    public class HardcodedSection : IDisposable
    {
        /// <summary>
        /// Marks a hard coded section.
        /// </summary>
        public HardcodedSection()
        { }

        /// <summary>
        /// Marks a hard coded section.
        /// </summary>
        public HardcodedSection(string filterExpression)
        { }

        /// <summary>
        /// Marks a hard coded section.
        /// </summary>
        public HardcodedSection(string filterExpression, bool toRefactor = false)
        { }

        /// <summary>
        /// Marks a hard coded section.
        /// </summary>
        public HardcodedSection(string filterExpression, string documentation, bool toRefactor = false)
        { }

        /// <summary>
        /// Ends a hard coded section.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
