using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// BitArray extension methods.
    /// </summary>
    public static class BitArrayExtensions
    {
        /// <summary>
        /// Converts this BitArray to an array of bytes.
        /// </summary>
        public static Byte[] ToByteArray(this BitArray array)
        {
            var result = new Byte[(int)((array.Length + 7) / 8)];
            array.CopyTo(result, 0);
            return result;
        }

        /// <summary>
        /// Converts this BitArray to an array of ints.
        /// </summary>
        public static Int32[] ToInt32Array(this BitArray array)
        {
            var result = new Int32[(int)((array.Length + 31) / 32)];
            array.CopyTo(result, 0);
            return result;
        }

        /// <summary>
        /// Enumerates the bits set.
        /// </summary>
        public static IEnumerable<int> EnumerateBitsSet(this BitArray array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array.Get(i)) yield return i;
            }
        }

        /// <summary>
        /// Sets the bits at the given indexes.
        /// </summary>
        public static void SetBits(this BitArray array, int[] bitIndexes)
        {
            foreach (var i in bitIndexes)
            {
                array.Set(i, true);
            }
        }

        /// <summary>
        /// Clears the bits at the given indexes.
        /// </summary>
        public static void ClearBits(this BitArray array, int[] bitIndexes)
        {
            foreach (var i in bitIndexes)
            {
                array.Set(i, false);
            }
        }
    }
}
