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

        [TestMethod]
        public void ReplaceFirstTests()
        {
            Assert.AreEqual(null, ((string?)null).ReplaceFirst("xyz", "123"));
            Assert.AreEqual("AxyzBxyzC", "AxyzBxyzC".ReplaceFirst("xyz", "xyz"));
            Assert.AreEqual("A123BxyzC", "AxyzBxyzC".ReplaceFirst("xyz", "123"));
            Assert.AreEqual("AxyzBxyzC", "AxyzBxyzC".ReplaceFirst("abc", "123"));
            Assert.AreEqual("A123B456C", "AxyzBxyzC".ReplaceFirst("xyz", "123").ReplaceFirst("xyz", "456"));
            Assert.AreEqual("ABxyzC", "AxyzBxyzC".ReplaceFirst("xyz", ""));
        }

        [TestMethod]
        public void ReplaceLastTests()
        {
            Assert.AreEqual(null, ((string?)null).ReplaceLast("xyz", "123"));
            Assert.AreEqual("AxyzBxyzC", "AxyzBxyzC".ReplaceLast("xyz", "xyz"));
            Assert.AreEqual("AxyzB123C", "AxyzBxyzC".ReplaceLast("xyz", "123"));
            Assert.AreEqual("AxyzBxyzC", "AxyzBxyzC".ReplaceLast("abc", "123"));
            Assert.AreEqual("A456B123C", "AxyzBxyzC".ReplaceLast("xyz", "123").ReplaceLast("xyz", "456"));
            Assert.AreEqual("AxyzBC", "AxyzBxyzC".ReplaceLast("xyz", ""));
        }

        [TestMethod]
        public void FormatTests()
        {
            Assert.AreEqual(null, ((string?)null).Format("/{0}"));
            Assert.AreEqual("/Foobar", "Foobar".Format("/{0}"));
            Assert.AreEqual("{Foobar}", "Foobar".Format("{{{0}}}"));
        }
    }
}
