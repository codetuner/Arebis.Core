using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Types.Collections.Generic
{
    /// <summary>
    /// A Dictionary where querying a missing key does not result in KeyNotFoundException but in returning default(TValue).
    /// </summary>
    [Serializable]
    public class DefaultDictionary<TKey, TValue> : IDictionary<TKey, TValue?>
        where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue?> internalDictionary;

        /// <summary>
        /// Constructs a new DefaultDictonary.
        /// </summary>
        public DefaultDictionary()
        {
            this.internalDictionary = new Dictionary<TKey, TValue?>();
        }

        /// <summary>
        /// Constructs a new DefaultDictonary.
        /// </summary>
        public DefaultDictionary(IEqualityComparer<TKey> comparer)
        {
            this.internalDictionary = new Dictionary<TKey, TValue?>(comparer);
        }

        /// <summary>
        /// Constructs a new DefaultDictonary.
        /// </summary>
        public DefaultDictionary(IDictionary<TKey, TValue?> dictionary)
        {
            this.internalDictionary = new Dictionary<TKey, TValue?>(dictionary);
        }

        /// <summary>
        /// Constructs a new DefaultDictonary.
        /// </summary>
        public DefaultDictionary(IDictionary<TKey, TValue?> dictionary, IEqualityComparer<TKey> comparer)
        {
            this.internalDictionary = new Dictionary<TKey, TValue?>(dictionary, comparer);
        }

        #region IDictionary<TKey,TValue?> Members

        /// <inheritdoc/>
        public void Add(TKey key, TValue? value)
        {
            this.internalDictionary.Add(key, value);
        }

        /// <inheritdoc/>
        public bool ContainsKey(TKey key)
        {
            return this.internalDictionary.ContainsKey(key);
        }

        /// <inheritdoc/>
        public ICollection<TKey> Keys
        {
            get { return this.internalDictionary.Keys; }
        }

        /// <inheritdoc/>
        public bool Remove(TKey key)
        {
            return this.internalDictionary.Remove(key);
        }

        /// <inheritdoc/>
        public bool TryGetValue(TKey key, out TValue? value)
        {
            return this.internalDictionary.TryGetValue(key, out value);
        }

        /// <inheritdoc/>
        public ICollection<TValue?> Values
        {
            get { return this.internalDictionary.Values; }
        }

        /// <inheritdoc/>
        public TValue? this[TKey key]
        {
            get
            {
                if (this.internalDictionary.TryGetValue(key, out TValue? value))
                    return value;
                else
                    return default;
            }
            set
            {
                this.internalDictionary[key] = value;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        /// <inheritdoc/>
        void ICollection<KeyValuePair<TKey, TValue?>>.Add(KeyValuePair<TKey, TValue?> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue?>>)this.internalDictionary).Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.internalDictionary.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<TKey, TValue?> item)
        {
            return this.internalDictionary.Contains(item);
        }

        /// <inheritdoc/>
        void ICollection<KeyValuePair<TKey, TValue?>>.CopyTo(KeyValuePair<TKey, TValue?>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this.internalDictionary).CopyTo(array!, arrayIndex);
        }

        /// <inheritdoc/>
        public int Count
        {
            get { return this.internalDictionary.Count; }
        }

        /// <inheritdoc/>
        bool ICollection<KeyValuePair<TKey, TValue?>>.IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<TKey, TValue>>)this.internalDictionary).IsReadOnly; }
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<TKey, TValue?> item)
        {
            return this.Remove(item);
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<TKey, TValue?>> GetEnumerator()
        {
            return this.internalDictionary.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <inheritdoc/>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (System.Collections.IEnumerator)this.GetEnumerator();
        }

        #endregion
    }
}
