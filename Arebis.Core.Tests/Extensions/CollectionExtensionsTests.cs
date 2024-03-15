using Arebis.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Tests.Extensions
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void PopFirstOrDefaultTests()
        {
            List<int> list = new();

            Assert.AreEqual(0, list.PopFirstOrDefault());

            list = new List<int>([11, 12, 13, 14]);
            Assert.AreEqual(11, list.PopFirstOrDefault());
            CollectionAssert.AreEqual(new List<int>([12, 13, 14]), list);

            list = new List<int>([11, 12, 13, 14]);
            Assert.AreEqual(11, list.PopFirstOrDefault(i => i % 2 == 1));
            CollectionAssert.AreEqual(new List<int>([12, 13, 14]), list);

            list = new List<int>([11, 12, 13, 14]);
            Assert.AreEqual(12, list.PopFirstOrDefault(i => i % 2 == 0));
            CollectionAssert.AreEqual(new List<int>([11, 13, 14]), list);
        }

        [TestMethod]
        public void PopAllTests()
        {
            List<int> list = new();

            list = new List<int>([11, 12, 13, 14]);
            CollectionAssert.AreEqual(new List<int>([11, 13]), list.PopAll(i => i % 2 == 1).ToList());
            CollectionAssert.AreEqual(new List<int>([12, 14]), list);

            list = new List<int>([11, 12, 13, 14]);
            CollectionAssert.AreEqual(new List<int>([12, 14]), list.PopAll(i => i % 2 == 0).ToList());
            CollectionAssert.AreEqual(new List<int>([11, 13]), list);
        }

        [TestMethod]
        public void SelectTests()
        {
            // Test selection:
            {
                var coll = new List<int>([6, 7, 8, 9, 10, 11]);

                var collEvens = coll.Select(i => i % 2 == 0);

                CollectionAssert.AreEqual(new List<int>([6, 8, 10]), collEvens.ToList());
            }

            // Test whether changes on selection are correctly applied on underlying collection:
            {
                var syncSource = new int[] { 5, 7 };
                var coll = new List<int>([6, 7, 8, 9, 10]);

                var collOdds = coll.Select(i => i % 2 == 1);

                CollectionAssert.AreEqual(new List<int>([7, 9]), collOdds.ToList());

                syncSource.SynchroniseWith(collOdds, (a, b) => a == b, true);

                CollectionAssert.AreEqual(new List<int>([5, 6, 7, 8, 10]), coll.OrderBy(i => i).ToList());
            }

            // Adding items that do satisfy criteria:
            {
                var evens = new List<int>().Select(i => i % 2 == 0);
                evens.Add(2);
            }

            // Adding items that do not statisfy criteria:
            try
            {
                var evens = new List<int>().Select(i => i % 2 == 0);
                evens.Add(3);

                Assert.Fail("Must throw InvalidOperationException: element adding is not satisfying criteria.");
            }
            catch (InvalidOperationException) { }

            // Removing items that do satisfy criteria:
            {
                var evens = new List<int>([4, 5, 6, 7, 8]).Select(i => i % 2 == 0);
                Assert.IsTrue(evens.Remove(4));
                Assert.IsFalse(evens.Remove(12));
            }

            // Removing items that do not satisfy criteria:
            {
                var evens = new List<int>([4, 5, 6, 7, 8]).Select(i => i % 2 == 0);
                Assert.IsFalse(evens.Remove(5));
            }
        }
    }
}
