﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
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
        /// Whether the enumerable is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        /// Returns the enumerable if it is not empty, null if it is empty.
        /// </summary>
        public static IEnumerable<T>? NullIfEmpty<T>(this  IEnumerable<T>? enumerable)
        {
            return
                enumerable == null ? null 
                :enumerable.Any() ? enumerable
                : null;
        }

        /// <summary>
        /// Returns true if the enumerable is empty.
        /// </summary>
        public static bool None<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        /// <summary>
        /// Returns true if the enumerable is empty.
        /// </summary>
        public static bool None<T>(this IQueryable<T> query)
        {
            return !query.Any();
        }

        /// <summary>
        /// Returns true if none of the elements satisfy the predicate. Inverse of Any().
        /// </summary>
        public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return !enumerable.Any(predicate);
        }

        /// <summary>
        /// Returns true if none of the elements satisfy the predicate. Inverse of Any().
        /// </summary>
        public static bool None<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate)
        {
            return !query.Any(predicate);
        }

        /// <summary>
        /// If the enumerable is an array, return it, otherwise convert it into an array.
        /// </summary>
        [return: NotNullIfNotNull("enumerable")]
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

        /// <summary>
        /// Synchronises a source with a target collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="target">The target to synchronise to.</param>
        /// <param name="comparer">Comparer to use. Object.Equals if null or missing.</param>
        /// <param name="apply">Whether to update the target collection to have it holding the same elements as the source.</param>
        /// <param name="onAdded">Action to take on elements available in the source but not in the target collection.</param>
        /// <param name="onRemoved">Action to take on elements available in the target but not in the source collection.</param>
        /// <param name="onRemaining">Action to take on elements available in both source and target, takes the source item and corresponding target item.</param>
        /// <returns>The source collection for fluent syntax.</returns>
        public static IEnumerable<T> SynchroniseWith<T>(this IEnumerable<T> source, ICollection<T> target, Func<T, T, bool>? comparer = null, bool apply = true, Action<T>? onAdded = null, Action<T>? onRemoved = null, Action<T, T>? onRemaining = null)
        {
            // Set comparer:
            comparer ??= (s, t) => Object.Equals(s, t);

            // Detect differences:
            var added = new List<T>();
            var removed = target.ToList();
            foreach (var item in source)
            {
                var index = removed.IndexWhere(e => comparer(e, item));
                if (index == -1)
                {
                    onAdded?.Invoke(item);
                    added.Add(item);
                }
                else
                {
                    onRemaining?.Invoke(item, removed[index]);
                    removed.RemoveAt(index);
                }
            }
            foreach (var item in removed)
            {
                onRemoved?.Invoke(item);
            }

            // Apply differences:
            if (apply)
            {
                foreach (var item in added) target.Add(item);
                foreach (var item in removed) target.Remove(item);
            }

            // Return source for fluent syntax:
            return source;
        }

        /// <summary>
        /// Takes the given number of items. If there are more items, adds the substitute to replace the removed items.
        /// The following example would return the first three names followed by a "…" string if the
        /// list contains more than 3 names: names.TakeWithSubst(3, "…");
        /// </summary>
        /// <param name="enumerable">The original enumerable.</param>
        /// <param name="limit">Max items to return</param>
        /// <param name="substitute">The substitute for the removed values. If null, it will not be appended.
        /// The substitute string can contain format items {0} for the total number of items in the list and {1} for the removed number of items in the list, i.e: "+{1}".</param>
        /// <returns>Null if enumerable is null, otherwise an enumerable limited to the number of items.</returns>
        [return: NotNullIfNotNull("enumerable")]
        public static IEnumerable<string>? TakeWithSubst(this IEnumerable<string>? enumerable, int limit, string? substitute)
        {
            if (enumerable == null) return null;
            if (substitute is null)
            {
                return enumerable.Take(limit);
            }
            else
            {
                var totalcount = enumerable.Count();
                if (totalcount > limit)
                {
                    var list = enumerable.Take(limit).ToList();
                    list.Add(String.Format(substitute, totalcount, totalcount - limit));
                    return list;
                }
                else
                {
                    return enumerable;
                }
            }
        }

        /// <summary>
        /// Joins the elements of the given enumerable into a single string.
        /// </summary>
        /// <param name="strings">The enumerable of strings to join.</param>
        /// <param name="separator">String to separate strings by.</param>
        /// <param name="prefix">Prefix of the string if the strings enumerable is not null.</param>
        /// <param name="postFix">Postfix of the string if the strings enumerable is not null.</param>
        /// <returns>Null if strings is null, otherwise the joined string with post- and prefix.</returns>
        [return: NotNullIfNotNull("strings")]
        public static string? JoinToString(this IEnumerable<string>? strings, string separator = "; ", string prefix = "", string postFix = "")
        {
            if (strings == null) return null;
            var sb = new StringBuilder();
            sb.Append(prefix);
            var sep = "";
            foreach(var item in strings) {
                sb.Append(sep); sep = separator;
                sb.Append(item);
            }
            sb.Append(postFix);
            return sb.ToString();
        }
    }
}
