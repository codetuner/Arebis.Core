using Arebis.Core.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Tests.Globalization
{
    [TestClass]
    public class CultureScopeTests
    {
        [TestMethod]
        public void NestedCultureScopes()
        {
            using (new CultureScope("nl-BE"))
            {
                Assert.AreEqual("nl-BE", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                Assert.AreEqual("nl-BE", System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
                using (new CultureScope("en-US"))
                {
                    Assert.AreEqual("en-US", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    Assert.AreEqual("en-US", System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
                    using (new CultureScope("ar-EG"))
                    {
                        Assert.AreEqual("ar-EG", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                        Assert.AreEqual("ar-EG", System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
                    }
                    Assert.AreEqual("en-US", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    Assert.AreEqual("en-US", System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
                }
                Assert.AreEqual("nl-BE", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                Assert.AreEqual("nl-BE", System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
            }
        }
    }
}
