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
    }
}
