using Arebis.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Tests.Extensions
{
    [TestClass]
    public class PredicateExtensionsTests
    {
        [TestMethod]
        public void OrElseTests()
        { 
            var values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            Expression<Func<int, bool>> predicate = i => false;
            predicate = predicate.OrElse(i => i == 1);
            predicate = predicate.OrElse(i => i == 5);
            predicate = predicate.OrElse(i => i == 8);

            {
                // On IQueryable<T>:
                var result = values.AsQueryable().Where(predicate).ToList();
                Assert.AreEqual(3, result.Count);
                CollectionAssert.AreEqual(new List<int> { 1, 5, 8 }, result);
            }

            {
                // On IEnumerable<T>:
                var result = values.Where(predicate.Compile()).ToList();
                Assert.AreEqual(3, result.Count);
                CollectionAssert.AreEqual(new List<int> { 1, 5, 8 }, result);
            }
        }
    }
}
