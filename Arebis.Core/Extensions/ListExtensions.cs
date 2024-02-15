using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// List extension methods.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Removes the first element. Returns false if list is empty.
        /// </summary>
        public static bool RemoveFirst<T>(this IList<T> list)
        {
            var lc = list.Count;
            if (lc == 0) return false;
            list.RemoveAt(0);
            return true;
        }

        /// <summary>
        /// Removes the last element. Returns false if list is empty.
        /// </summary>
        public static bool RemoveLast<T>(this IList<T> list)
        {
            var lc = list.Count;
            if (lc == 0) return false;
            list.RemoveAt(lc - 1);
            return true;
        }

        /// <summary>
        /// Given a list and a number of "rows", returns an array per row containg an array per column.
        /// First elements appear in first row (horizontal orientation).
        /// </summary>
        /// <param name="list">The source list.</param>
        /// <param name="columns">The number of columns to produce.</param>
        /// <param name="fillWithDefault">Whether to fill the last row's missing elements with default(T) values. By default, returns smaller array.</param>
        /// <returns>An array per row containing an array per column/cell of KeyValuePairs holding index of the element in the original list and the element itself.</returns>
        public static KeyValuePair<int, T>[][] ToColumnsHorizontal<T>(this IList<T> list, int columns, bool fillWithDefault = false)
        {
            var index = 0;
            var fullrows = list.Count / columns;
            var lastrowcols = list.Count % columns;
            var result = new KeyValuePair<int, T>[fullrows + (lastrowcols > 0 ? 1 : 0)][];
            // All but last row:
            for (int r = 0; r < fullrows; r++)
            {
                result[r] = new KeyValuePair<int, T>[columns];
                for (int c = 0; c < columns; c++)
                {
                    result[r][c] = new(index, list[index++]);
                }
            }
            // Last row:
            if (lastrowcols > 0)
            {
                result[fullrows] = (fillWithDefault) ? new KeyValuePair<int, T>[columns] : new KeyValuePair<int, T>[lastrowcols];
                for(int c=0;c<lastrowcols; c++)
                {
                    result[fullrows][c] = new(index, list[index++]);
                }
                if (fillWithDefault)
                { 
                    for(int c=lastrowcols; c<columns; c++)
                    {
                        result[fullrows][c] = new(index++, default(T)!);
                    }
                }
            }
            // Return result:
            return result;
        }
        /// <summary>
        /// Given a collection and a number of "rows", returns an array per row containg an array per column.
        /// First elements appear in first column (vertical orientation).
        /// </summary>
        /// <param name="list">The source list.</param>
        /// <param name="columns">The number of columns to produce.</param>
        /// <param name="fillWithDefault">Whether to fill the last row's missing elements with default(T) values. By default, returns smaller array.</param>
        /// <returns>An array per row containing an array per column/cell of KeyValuePairs holding index of the element in the original list and the element itself.</returns>
        public static KeyValuePair<int, T>[][] ToColumnsVertical<T>(this IList<T> list, int columns, bool fillWithDefault = false)
        {
            var index = 0;
            var rows = (list.Count + columns - 1) / columns;
            var completerows = rows - (list.Count % columns);
            var result = new KeyValuePair<int, T>[rows][];
            for (int r = 0; r < rows; r++)
                result[r] = new KeyValuePair<int, T>[r <= completerows ? columns : fillWithDefault ? columns : columns - 1];
            for(int c= 0; c < columns; c++)
            {
                for(int r = 0; r<rows;r++)
                {
                    if (result[r].Length >= (c + 1))
                    {
                        result[r][c] = (index < list.Count) ? new(index, list[index++]) : new(index++, default(T)!);
                    }
                }
            }
            return result;
        }
    }
}
