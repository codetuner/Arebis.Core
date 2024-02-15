using Arebis.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Tests.Extensions
{
    [TestClass]
    public class NumberExtensionsTests
    {
        [TestMethod]
        public void IfTests()
        {
            Assert.AreEqual(0, 0.If(i => i > 5, 5));
            Assert.AreEqual(5, 5.If(i => i > 5, 5));
            Assert.AreEqual(5, 8.If(i => i > 5, 5));
            Assert.AreEqual(0, (-2).If(i => i < 0, 0));
        }
    }
}
