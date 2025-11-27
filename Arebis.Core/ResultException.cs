using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core
{
    /// <summary>
    /// An exception that encapsulates a result to be returned on a higher level when unwinding the stack.
    /// </summary>
    /// <typeparam name="TResult">Type of result value.</typeparam>
    public class ResultException<TResult> : Exception
    {
        /// <summary>
        /// Constructs an exception that encapsulates a result to be returned on a higher level when unwinding the stack.
        /// </summary>
        public ResultException()
        { }

        /// <summary>
        /// Constructs an exception that encapsulates a result to be returned on a higher level when unwinding the stack.
        /// </summary>
        /// <param name="result"></param>
        [SetsRequiredMembers]
        public ResultException(TResult result)
        {
            this.Result = result;
        }

        /// <summary>
        /// The the result to be returned on a higher level.
        /// </summary>
        public required TResult Result { get; set; }
    }
}
