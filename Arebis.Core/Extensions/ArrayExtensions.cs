﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// Array extension methods.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Compares this array to the given one. Returns true if they are
        /// content-wise identical, false otherwise.
        /// </summary>
        public static bool ContentEquals<TItem>(this TItem[]? a, TItem[]? b)
        {
            if (Object.ReferenceEquals(a, b))
                return true;
            else if (a is null)
                return false;
            else if (b is null)
                return false;
            else if (a.Length != b.Length)
                return false;
            else
            {
                for (int i = 0; i < a.Length; i++)
                    if (!Object.Equals(a[i], b[i]))
                        return false;
                return true;
            }
        }

        /// <summary>
        /// Compares this array to the given one. Returns true if they are
        /// content-wise identical, false otherwise.
        /// </summary>
        public static bool ContentEquals<TItem>(this TItem[]? a, TItem[]? b, Comparer<TItem> comparer)
        {
            if (Object.ReferenceEquals(a, b))
                return true;
            else if (a is null)
                return false;
            else if (b is null)
                return false;
            else if (a.Length != b.Length)
                return false;
            else
            {
                for (int i = 0; i < a.Length; i++)
                    if (comparer.Compare(a[i], a[i]) != 0)
                        return false;
                return true;
            }
        }

        /// <summary>
        /// Returns the (first) index of the given value, or -1 if not found.
        /// </summary>
        public static int IndexOf<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (Object.Equals(array[i], value)) return i;
            }

            return -1;
        }
    }
}
