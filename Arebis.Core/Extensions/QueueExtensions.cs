using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    public static class QueueExtensions
    {
        /// <summary>
        /// Dequeues the next item or returns default value if queue is empty.
        /// </summary>
        [return: NotNullIfNotNull("ifEmpty")]
        public static TItem? DequeueOrDefault<TItem>(this Queue<TItem> queue, TItem? ifEmpty = default)
        {
            if (queue.Count > 0)
                return queue.Dequeue();
            else
                return ifEmpty;
        }
    }
}
