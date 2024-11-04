using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Tests
{
    [TestClass]
    public class LiveAssertTests
    {
        [TestMethod]
        public void ArgumentNotNullTests()
        {
            var s = "foobar";
            LiveAssert.ArgumentNotNullOrEmpty(s);

            s = null;
            try
            {
                LiveAssert.ArgumentNotNullOrEmpty(s);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Value cannot be null. (Parameter 's')", ex.Message);
            }
        }
    }
}
