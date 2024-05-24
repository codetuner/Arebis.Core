using Arebis.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void ShortenTests()
        {
            string? n = null;

            Debug.WriteLine("-------------");
            Debug.WriteLine(n + n);
            Assert.AreEqual("", n + n);
            Debug.WriteLine("-------------");

            Assert.IsNull(n.Shorten(10));
            Assert.AreEqual(10, "Abcdefghijklmnopqrstuvwxyz".Shorten(10).Length);
            Assert.AreEqual("", n.Shorten(10, null, null));
            Assert.AreEqual("[", n.Shorten(10, "[", null));
            Assert.AreEqual("]", n.Shorten(10, null, "]"));
            Assert.AreEqual("[]", n.Shorten(10, "[", "]"));
            Assert.AreEqual("[FooBar]", "FooBar".Shorten(10, "[", "]"));
            Assert.AreEqual("[Abcdfghj]", "Abcdefghijklmnopqrstuvwxyz".Shorten(10, "[", "]"));
        }

        [TestMethod]
        public void ToUniqueNameWithinTests()
        {
            // Expected behaviour:
            var names = new string[] { "Filip", "Filip (2)", "Filip (3)", "Mathilde" };
            Assert.AreEqual("Albert", "Albert".ToUniqueNameWithin(names));
            Assert.AreEqual("Filip (4)", "Filip".ToUniqueNameWithin(names));
            Assert.AreEqual("Mathilde (2)", "Mathilde".ToUniqueNameWithin(names));

            // Could be enhanced in future, but this is current behaviour:
            Assert.AreEqual("Filip (2) (2)", "Filip (2)".ToUniqueNameWithin(names));
        }
    }
}
