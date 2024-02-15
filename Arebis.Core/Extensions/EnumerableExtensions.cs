using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// Enumerable extension methods.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns true if none of the elements satisfy the predicate. Inverse of Any().
        /// </summary>
        public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return !enumerable.Any(predicate);
        }

        /// <summary>
        /// If the enumerable is an array, return it, otherwise convert it into an array.
        /// </summary>
        [return:NotNullIfNotNull("enumerable")]
        public static T[]? AsArray<T>(this IEnumerable<T>? enumerable)
        {
            if (enumerable == null)
            {
                return null;
            }
            else if (enumerable is T[])
            {
                return (T[])enumerable;
            }
            else
            {
                return enumerable.ToArray();
            }
        }

        /// <summary>
        /// If the enumerable is a list, return it, otherwise convert it into a list.
        /// </summary>
        [return: NotNullIfNotNull("enumerable")]
        public static List<T>? AsList<T>(this IEnumerable<T>? enumerable)
        {
            if (enumerable == null)
            {
                return null;
            }
            else if (enumerable is List<T>)
            {
                return (List<T>)enumerable;
            }
            else
            {
                return enumerable.ToList();
            }
        }

        /// <summary>
        /// Enumerates all but the first (n) elements.
        /// </summary>
        public static IEnumerable<T> ButFirst<T>(this IEnumerable<T> enumerable, int n = 1)
        {
            var list = enumerable.ToArray();
            for (int i = n; i < list.Length; i++)
            {
                yield return list[i];
            }
        }

        /// <summary>
        /// Enumerates all but the last (n) elements.
        /// </summary>
        public static IEnumerable<T> ButLast<T>(this IEnumerable<T> enumerable, int n = 1)
        {
            var list = enumerable.ToArray();
            for (int i = 0; i < (list.Length - n); i++)
            {
                yield return list[i];
            }
        }

        /// <summary>
        /// Returns the items enumerable splitted in pages of pageSize elements.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Pages<T>(this IEnumerable<T> items, int pageSize)
        {
            using (var enumerator = items.GetEnumerator())
            {
                do
                {
                    var page = new List<T>();
                    for (int i = 0; i < pageSize; i++)
                    {
                        if (enumerator.MoveNext())
                        {
                            page.Add(enumerator.Current);
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (page.Count > 0)
                    {
                        yield return page;
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);
            }
        }

        /// <summary>
        /// Return the first index for which the given predicate matches. Returns -1 if no match was found.
        /// </summary>
        public static int IndexWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item)) return index;
                index++;
            }

            return -1;
        }

        /// <summary>
        /// Return the indexes for which the given predicate match.
        /// </summary>
        public static IEnumerable<int> IndexesWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item)) yield return index;
                index++;
            }
        }
    }
}
