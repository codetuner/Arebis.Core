using Arebis.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core
{
    /// <summary>
    /// Represents a selection of a collection, to which adding or removing items is reflected on the underlying collection.
    /// </summary>
    public class Selection<T> : ICollection<T>
    {
        /// <summary>
        /// Constructs a selection.
        /// </summary>
        /// <param name="innerCollection">Inner collection.</param>
        /// <param name="criteria">Selection criteria.</param>
        public Selection(ICollection<T> innerCollection, Func<T, bool> criteria)
        {
            InnerCollection = innerCollection;
            Criteria = criteria;
        }

        /// <summary>
        /// The underlying collection for this selection.
        /// </summary>
        public ICollection<T> InnerCollection { get; }

        /// <summary>
        /// The criteria of this selection.
        /// </summary>
        public Func<T, bool> Criteria { get; }

        /// <inheritdoc/>
        public int Count => this.InnerCollection.Count(Criteria);

        /// <inheritdoc/>
        public bool IsReadOnly => this.InnerCollection.IsReadOnly;

        /// <inheritdoc/>
        public void Add(T item)
        {
            if (Criteria(item)) this.InnerCollection.Add(item);
            else throw new InvalidOperationException("Item does not match criteria of selection.");
        }

        /// <inheritdoc/>
        public void Clear()
        {
            foreach (var item in this.ToList()) this.InnerCollection.Remove(item);
        }

        /// <inheritdoc/>
        public bool Contains(T item)
        {
            return (Criteria(item) && this.InnerCollection.Contains(item));
        }

        /// <inheritdoc/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach(var item in this)
            {
                array[arrayIndex++] = item;
            }
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            return this.InnerCollection.Where(Criteria).GetEnumerator();
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            if (Criteria(item)) return this.InnerCollection.Remove(item);
            else return false;
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.InnerCollection.Where(Criteria).GetEnumerator();
        }
    }
}
