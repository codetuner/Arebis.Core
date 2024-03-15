using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// Collection extension methods.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds the given item to the collection and returns the collection for fluent syntax.
        /// </summary>
        public static ICollection<T> With<T>(this ICollection<T> collection, T item)
        {
            collection.Add(item);
            return collection;
        }

        /// <summary>
        /// Adds the given items to the collection and returns the collection for fluent syntax.
        /// </summary>
        public static ICollection<T> WithAll<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach(var item in items)
                collection.Add(item);
            return collection;
        }

        /// <summary>
        /// Removes the first instance of the given item from the collection and returns the collection for fluent syntax.
        /// </summary>
        public static ICollection<T> Without<T>(this ICollection<T> collection, T item, bool removeFirstOccurenceOnly = true)
        {
            if (removeFirstOccurenceOnly)
            {
                collection.Remove(item);
            }
            else
            {
                while (collection.Remove(item)) ;
            }
            return collection;
        }

        /// <summary>
        /// Removes all instances of the given item from the collection and returns the collection for fluent syntax.
        /// </summary>
        public static ICollection<T> WithoutAll<T>(this ICollection<T> collection, IEnumerable<T> items, bool removeFirstOccurencesOnly = true)
        {
            foreach (var item in items)
            {
                collection.Without(item, removeFirstOccurencesOnly);
            }
            return collection;
        }
        
        /// <summary>
        /// Retrieves data in sets.
        /// Retrieves sets of items opposed to retrieving them all one-by-one or all at once.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="collection">Collection to retrieve data from.</param>
        /// <param name="setSize">Set size.</param>
        /// <returns>Sets of given size.</returns>
        public static IEnumerable<T[]> InSetsOf<T>(this IOrderedQueryable<T> collection, int setSize)
        {
            var skipCount = 0;
            while (true)
            {
                var set = collection.Skip(skipCount).Take(setSize).ToArray();
                if (set.Length == 0)
                    yield break;
                else
                {
                    skipCount += set.Length;
                    yield return set;
                }
            }
        }

        /// <summary>
        /// Returns and removes the first element of the collection.
        /// </summary>
        public static T? PopFirstOrDefault<T>(this ICollection<T> collection)
        {
            var en = collection.GetEnumerator();
            if (en.MoveNext())
            {
                var first = en.Current;
                en.Dispose();
                collection.Remove(first);
                return first;
            }
            else
            {
                // If none found:
                en.Dispose();
                return default(T);
            }
        }

        /// <summary>
        /// Returns and removes the first element of the collection that matches the given predicate.
        /// </summary>
        public static T? PopFirstOrDefault<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            using var en = collection.GetEnumerator();
            while (en.MoveNext())
            { 
                if (predicate(en.Current))
                {
                    var first = en.Current;
                    en.Dispose();
                    collection.Remove(first);
                    return first;
                }
            }

            // If none found:
            en.Dispose();
            return default(T);
        }

        /// <summary>
        /// Returns and removes all elements of the collection that match the given predicate.
        /// </summary>
        public static IEnumerable<T> PopAll<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            // Search for matches:
            foreach (var item in collection.Where(predicate).ToList())
            {
                collection.Remove(item);
                yield return item;
            }
        }

        /// <summary>
        /// Returns a selection on the given collection for the given criteria.
        /// </summary>
        /// <param name="collection">The underlying collection to return a selection for.</param>
        /// <param name="criteria">The criteria of the selection.</param>
        /// <returns>Returns a selection that mutates the underlying collection.</returns>
        public static Selection<T> Select<T>(this ICollection<T> collection, Func<T, bool> criteria)
        {
            return new Selection<T>(collection, criteria);
        }
    }
}
