using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Types.Attributes
{
    /// <summary>
    /// Marks an enum as identifier, allowing it to have any value even undefined ones.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public class IdentifierEnumAttribute : Attribute
    { }
}
