using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arebis.Core.Extensions;

namespace Arebis.Core.Tests.Extensions
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void AsArrayTests()
        {
            IEnumerable<string>? n = null;
            var a = new string[] { "Foobar" };
            var l = new List<string>(a);
            Assert.IsNull(n.AsArray());
            Assert.AreSame(a, a.AsArray());
            Assert.AreNotSame(a, a.ToArray());
            Assert.AreNotSame<object>(l, l.AsArray());
            Assert.AreEqual(l[0], l.AsArray()![0]);
        }

        [TestMethod]
        public void AsListTests()
        {
            IEnumerable<string>? n = null;
            var a = new string[] { "Foobar" };
            var l = new List<string>(a);
            Assert.IsNull(n.AsList());
            Assert.AreSame(l, l.AsList());
            Assert.AreNotSame(l, l.ToList());
            Assert.AreNotSame<object>(a, a.AsList());
            Assert.AreEqual(a[0], a.AsList()![0]);
        }

        [TestMethod]
        public void NullIfEmptyTests()
        {
            IEnumerable<string>? n = null;
            Assert.IsNull(n.NullIfEmpty());
            n = new string[] { };
            Assert.IsNull(n.NullIfEmpty());
            n = new List<string>();
            Assert.IsNull(n.NullIfEmpty());
            n = new List<string>() { "FooBar" };
            Assert.AreEqual(n, n.NullIfEmpty());
        }

        [TestMethod]
        public void SynchroniseWithTests()
        {
            {
                var source = new int[] { 5, 6, 7 };
                var target = new List<int>();
                var added = new List<int>();
                var removed = new List<int>();
                var remaining = new List<int>();
                var result = source.SynchroniseWith(target, null, true, e => added.Add(e), e => removed.Add(e), (s, t) => remaining.Add(t));
                Assert.AreEqual(3, added.Count);
                Assert.AreEqual(0, removed.Count);
                Assert.AreEqual(0, remaining.Count);
            }
            {
                var source = new int[] { 5, 6, 7 };
                var target = source.ToList();
                var added = new List<int>();
                var removed = new List<int>();
                var remaining = new List<int>();
                var result = source.SynchroniseWith(target, null, true, e => added.Add(e), e => removed.Add(e), (s, t) => remaining.Add(t));
                Assert.AreEqual(0, added.Count);
                Assert.AreEqual(0, removed.Count);
                Assert.AreEqual(3, remaining.Count);
            }
            {
                var source = new int[0];
                var target = new List<int>([5, 6, 7]);
                var added = new List<int>();
                var removed = new List<int>();
                var remaining = new List<int>();
                var result = source.SynchroniseWith(target, null, true, e => added.Add(e), e => removed.Add(e), (s, t) => remaining.Add(t));
                Assert.AreEqual(0, added.Count);
                Assert.AreEqual(3, removed.Count);
                Assert.AreEqual(0, remaining.Count);
            }
            {
                var source = new int[] { 5, 6, 7 };
                var target = new List<int>([6, 7, 8, 9, 10]);
                var added = new List<int>();
                var removed = new List<int>();
                var remaining = new List<int>();
                var result = source.SynchroniseWith(target, (s, t) => s == t, true, e => added.Add(e), e => removed.Add(e), (s, t) => remaining.Add(t));
                Assert.AreEqual(1, added.Count);
                Assert.AreEqual(5, added[0]);
                Assert.AreEqual(3, removed.Count);
                Assert.AreEqual(8, removed[0]);
                Assert.AreEqual(9, removed[1]);
                Assert.AreEqual(10, removed[2]);
                Assert.AreEqual(2, remaining.Count);
                Assert.AreEqual(6, remaining[0]);
                Assert.AreEqual(7, remaining[1]);
                Assert.AreEqual(source.Length, target.Count);
            }
            {
                var source = new int[] { 5, 6, 7 };
                var target = new List<int>([6, 7, 8, 9, 10]);
                var result = source.SynchroniseWith(target, (s, t) => s == t, false);
                Assert.AreEqual(3, source.Length);
                Assert.AreEqual(5, target.Count);
            }
        }

        [TestMethod]
        public void SynchroniseWithOtherTests()
        {
            Func<int, char, bool> comparer = (i, c) => c == (char)('0' + i);

            {
                var source = new int[] { 5, 6, 7 };
                var target = (new char[] { '6', '7', '8' }).ToList();
                source.SynchroniseWithOther(target, comparer, (i) => target.Add((char)('0' + i)));
                CollectionAssert.AreEquivalent(source.Select(i => (char)('0' + i)).ToList(), target);
            }
            {
                // All source items are added, target starts empty:
                var source = new int[] { 5, 6, 7 };
                var target = new List<char>();
                var added = new List<int>();
                var removed = new List<char>();
                source.SynchroniseWithOther(target, comparer, e => { added.Add(e); target.Add((char)('0' + e)); }, e => { removed.Add(e); target.Remove(e); });
                Assert.AreEqual(3, added.Count);
                Assert.AreEqual(0, removed.Count);
                Assert.AreEqual(3, target.Count);
            }
            {
                // Source and target fully match, nothing is added or removed:
                var source = new int[] { 5, 6, 7 };
                var target = new List<char>() { '5', '6', '7' };
                var added = new List<int>();
                var removed = new List<char>();
                source.SynchroniseWithOther(target, comparer, e => { added.Add(e); target.Add((char)('0' + e)); }, e => { removed.Add(e); target.Remove(e); });
                Assert.AreEqual(0, added.Count);
                Assert.AreEqual(0, removed.Count);
                Assert.AreEqual(3, target.Count);
            }
            {
                // Source is empty, all target items are removed:
                var source = new int[0];
                var target = new List<char>() { '5', '6', '7' };
                var added = new List<int>();
                var removed = new List<char>();
                source.SynchroniseWithOther(target, comparer, e => { added.Add(e); target.Add((char)('0' + e)); }, e => { removed.Add(e); target.Remove(e); });
                Assert.AreEqual(0, added.Count);
                Assert.AreEqual(3, removed.Count);
                Assert.AreEqual(0, target.Count);
            }
            {
                // Mix of items to add, remove and leave remaining:
                var source = new int[] { 5, 6, 7 };
                var target = new List<char>() { '6', '7', '8', '9' };
                var added = new List<int>();
                var removed = new List<char>();
                source.SynchroniseWithOther(target, comparer, e => { added.Add(e); target.Add((char)('0' + e)); }, e => { removed.Add(e); target.Remove(e); });
                Assert.AreEqual(1, added.Count);
                Assert.AreEqual(5, added[0]);
                Assert.AreEqual(2, removed.Count);
                Assert.AreEqual('8', removed[0]);
                Assert.AreEqual('9', removed[1]);
                Assert.AreEqual(3, target.Count);
                Assert.AreEqual('6', target[0]);
                Assert.AreEqual('7', target[1]);
                Assert.AreEqual('5', target[2]);
            }
            {
                // Duplicate items in source: only the first duplicate matches, the second is added:
                var source = new int[] { 5, 5, 6 };
                var target = new List<char>() { '5', '6' };
                var added = new List<int>();
                var removed = new List<char>();
                source.SynchroniseWithOther(target, comparer, e => { added.Add(e); target.Add((char)('0' + e)); }, e => { removed.Add(e); target.Remove(e); });
                Assert.AreEqual(1, added.Count);
                Assert.AreEqual(5, added[0]);
                Assert.AreEqual(0, removed.Count);
                Assert.AreEqual(3, target.Count);
                Assert.AreEqual('5', target[0]);
                Assert.AreEqual('6', target[1]);
                Assert.AreEqual('5', target[2]);
            }
        }

        [TestMethod]
        public void TakeWithSubstTests()
        {
            {
                var source = new String[] { "White", "Green", "Yellow", "Blue", "Red", "Black" };
                var result = source.TakeWithSubst(4, null);
                Assert.AreEqual(4, result.Count());
                Assert.AreEqual(source[0], result.ToList()[0]);
                Assert.AreEqual(source[3], result.ToList()[3]);
            }
            {
                var source = new String[] { "White", "Green", "Yellow", "Blue", "Red", "Black" };
                var result = source.TakeWithSubst(4, "...");
                Assert.AreEqual(5, result.Count());
                Assert.AreEqual(source[0], result.ToList()[0]);
                Assert.AreEqual("...", result.ToList()[4]);
            }
            {
                var source = new String[] { "White", "Green", "Yellow", "Blue", "Red", "Black" };
                var result = source.TakeWithSubst(4, " of {0}");
                Assert.AreEqual(5, result.Count());
                Assert.AreEqual(source[0], result.ToList()[0]);
                Assert.AreEqual(" of 6", result.ToList()[4]);
            }
            {
                var source = new String[] { "White", "Green", "Yellow", "Blue", "Red", "Black" };
                var result = source.TakeWithSubst(4, "+{1}");
                Assert.AreEqual(5, result.Count());
                Assert.AreEqual(source[0], result.ToList()[0]);
                Assert.AreEqual("+2", result.ToList()[4]);
            }
        }
    }
}
