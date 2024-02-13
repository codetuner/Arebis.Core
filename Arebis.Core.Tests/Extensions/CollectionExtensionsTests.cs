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
    }
}
