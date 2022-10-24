using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// Collection extension methods.
    /// </summary>
    public static class CollectionExtension
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
        public static ICollection<T> With<T>(this ICollection<T> collection, IEnumerable<T> items)
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
        public static ICollection<T> Without<T>(this ICollection<T> collection, IEnumerable<T> items, bool removeFirstOccurencesOnly = true)
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
    }
}
