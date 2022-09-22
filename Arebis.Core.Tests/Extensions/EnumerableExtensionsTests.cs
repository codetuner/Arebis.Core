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
            Assert.AreNotSame(l, l.AsArray());
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
            Assert.AreNotSame(a, a.AsList());
            Assert.AreEqual(a[0], a.AsList()![0]);
        }
    }
}
