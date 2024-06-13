using Arebis.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Tests.Extensions
{
    [TestClass]
    public class ListExtensionsTests
    {
        [TestMethod]
        public void SwapTests()
        {
            var list = new List<int>() { 3, 2, 4, 0, 1 };
            list
                .Swap(0, 3)
                .Swap(1, 4)
                .Swap(2, 4)
                .Swap(3, 3);

            Assert.AreEqual(0, list[0]);
            Assert.AreEqual(1, list[1]);
            Assert.AreEqual(2, list[2]);
            Assert.AreEqual(3, list[3]);
            Assert.AreEqual(4, list[4]);
        }

        [TestMethod]
        public void ToColumnsHorizontalTests()
        {
            {
                var list = new List<int>([10, 11, 12, 13, 14, 15, 16, 17]);
                var result = list.ToColumnsHorizontal(4, true);
                Assert.AreEqual(2, result.Length); // Number of rows
                Assert.AreEqual(4, result[0].Length); // Number of cols
                Assert.AreEqual(4, result[1].Length); // Number of cols
                Assert.AreEqual(0, result[0][0].Key);
                Assert.AreEqual(7, result[1][3].Key);
                Assert.AreEqual(17, result[1][3].Value);
            }
            {
                var list = new List<int>([10, 11, 12, 13, 14, 15, 16, 17]);
                var result = list.ToColumnsHorizontal(5, true);
                Assert.AreEqual(2, result.Length); // Number of rows
                Assert.AreEqual(5, result[0].Length); // Number of cols
                Assert.AreEqual(5, result[1].Length); // Number of cols
                Assert.AreEqual(0, result[0][0].Key);
                Assert.AreEqual(7, result[1][2].Key);
                Assert.AreEqual(17, result[1][2].Value);
                Assert.AreEqual(8, result[1][3].Key);
                Assert.AreEqual(0, result[1][3].Value);
            }
            {
                var list = new List<int>([10, 11, 12, 13, 14, 15, 16, 17]);
                var result = list.ToColumnsHorizontal(5, false);
                Assert.AreEqual(2, result.Length); // Number of rows
                Assert.AreEqual(5, result[0].Length); // Number of cols
                Assert.AreEqual(3, result[1].Length); // Number of cols
                Assert.AreEqual(0, result[0][0].Key);
                Assert.AreEqual(7, result[1][2].Key);
                Assert.AreEqual(17, result[1][2].Value);
            }
            {
                var list = new List<int>([10, 11, 12]);
                var result = list.ToColumnsHorizontal(4, true);
                Assert.AreEqual(1, result.Length); // Number of rows
                Assert.AreEqual(4, result[0].Length); // Number of cols
                Assert.AreEqual(0, result[0][0].Key);
                Assert.AreEqual(10, result[0][0].Value);
                Assert.AreEqual(2, result[0][2].Key);
                Assert.AreEqual(12, result[0][2].Value);
                Assert.AreEqual(3, result[0][3].Key);
                Assert.AreEqual(0, result[0][3].Value);
            }
            {
                var list = new List<int>([10, 11, 12]);
                var result = list.ToColumnsHorizontal(4, true);
                Assert.AreEqual(1, result.Length); // Number of rows
                Assert.AreEqual(4, result[0].Length); // Number of cols
                Assert.AreEqual(0, result[0][0].Key);
                Assert.AreEqual(10, result[0][0].Value);
                Assert.AreEqual(2, result[0][2].Key);
                Assert.AreEqual(12, result[0][2].Value);
                Assert.AreEqual(3, result[0][3].Key);
                Assert.AreEqual(0, result[0][3].Value);
            }
            {
                var list = new List<int>([10, 11, 12]);
                var result = list.ToColumnsHorizontal(4, false);
                Assert.AreEqual(1, result.Length); // Number of rows
                Assert.AreEqual(3, result[0].Length); // Number of cols
                Assert.AreEqual(0, result[0][0].Key);
                Assert.AreEqual(10, result[0][0].Value);
                Assert.AreEqual(2, result[0][2].Key);
                Assert.AreEqual(12, result[0][2].Value);
            }
            {
                var list = new List<int>();
                var result = list.ToColumnsHorizontal(4, true);
                Assert.AreEqual(0, result.Length); // Number of rows
            }
            {
                var list = new List<int>();
                var result = list.ToColumnsHorizontal(4, false);
                Assert.AreEqual(0, result.Length); // Number of rows
            }
            {
                var list = new string[] { "John", "Jack" }.ToList();
                var result = list.ToColumnsHorizontal(4, true);
                Assert.AreEqual(1, result.Length); // Number of rows
                Assert.AreEqual("John", result[0][0].Value);
                Assert.AreEqual("Jack", result[0][1].Value);
                Assert.AreEqual(null, result[0][2].Value);
                Assert.AreEqual(null, result[0][3].Value);
            }
            {
                var list = new string?[] { "John", "Jack" }.ToList();
                var result = list.ToColumnsHorizontal(4, true);
                Assert.AreEqual(1, result.Length); // Number of rows
                Assert.AreEqual("John", result[0][0].Value);
                Assert.AreEqual("Jack", result[0][1].Value);
                Assert.AreEqual(null, result[0][2].Value);
                Assert.AreEqual(null, result[0][3].Value);
            }
        }

        [TestMethod]
        public void ToColumnsVerticalTests() 
        {
            {
                var list = new List<int>([10, 11, 12, 13, 14, 15, 16, 17]);
                var result = list.ToColumnsVertical(4, true);
                Assert.AreEqual(2, result.Length); // Number of rows
                Assert.AreEqual(4, result[0].Length); // Number of cols
                Assert.AreEqual(4, result[1].Length); // Number of cols
                Assert.AreEqual(0, result[0][0].Key);
                Assert.AreEqual(10, result[0][0].Value);
                Assert.AreEqual(6, result[0][3].Key);
                Assert.AreEqual(16, result[0][3].Value);
                Assert.AreEqual(1, result[1][0].Key);
                Assert.AreEqual(11, result[1][0].Value);
                Assert.AreEqual(7, result[1][3].Key);
                Assert.AreEqual(17, result[1][3].Value);
            }
            {
                var list = new List<int>([10, 11, 12, 13, 14, 15, 16, 17]);
                var result = list.ToColumnsVertical(5, true);
                Assert.AreEqual(2, result.Length); // Number of rows
                Assert.AreEqual(5, result[0].Length); // Number of cols
                Assert.AreEqual(5, result[1].Length); // Number of cols
                Assert.AreEqual(0, result[0][0].Key);
                Assert.AreEqual(10, result[0][0].Value);
                Assert.AreEqual(6, result[0][3].Key);
                Assert.AreEqual(16, result[0][3].Value);
                Assert.AreEqual(8, result[0][4].Key);
                Assert.AreEqual(0, result[0][4].Value);
                Assert.AreEqual(1, result[1][0].Key);
                Assert.AreEqual(11, result[1][0].Value);
                Assert.AreEqual(7, result[1][3].Key);
                Assert.AreEqual(17, result[1][3].Value);
                Assert.AreEqual(9, result[1][4].Key);
                Assert.AreEqual(0, result[1][4].Value);
            }
            {
                var list = new List<int>([10, 11, 12, 13, 14, 15, 16, 17]);
                var result = list.ToColumnsVertical(5, false);
                Assert.AreEqual(2, result.Length); // Number of rows
                Assert.AreEqual(4, result[0].Length); // Number of cols
                Assert.AreEqual(4, result[1].Length); // Number of cols
                Assert.AreEqual(0, result[0][0].Key);
                Assert.AreEqual(10, result[0][0].Value);
                Assert.AreEqual(6, result[0][3].Key);
                Assert.AreEqual(16, result[0][3].Value);
                Assert.AreEqual(1, result[1][0].Key);
                Assert.AreEqual(11, result[1][0].Value);
                Assert.AreEqual(7, result[1][3].Key);
                Assert.AreEqual(17, result[1][3].Value);
            }
            {
                var list = new List<int>([10, 11, 12]);
                var result = list.ToColumnsVertical(4, true);
                Assert.AreEqual(1, result.Length); // Number of rows
                Assert.AreEqual(4, result[0].Length); // Number of cols
                Assert.AreEqual(0, result[0][0].Key);
                Assert.AreEqual(10, result[0][0].Value);
                Assert.AreEqual(2, result[0][2].Key);
                Assert.AreEqual(12, result[0][2].Value);
                Assert.AreEqual(3, result[0][3].Key);
                Assert.AreEqual(0, result[0][3].Value);
            }
            {
                var list = new List<int>([10, 11, 12]);
                var result = list.ToColumnsVertical(4, false);
                Assert.AreEqual(1, result.Length); // Number of rows
                Assert.AreEqual(3, result[0].Length); // Number of cols
                Assert.AreEqual(0, result[0][0].Key);
                Assert.AreEqual(10, result[0][0].Value);
                Assert.AreEqual(2, result[0][2].Key);
                Assert.AreEqual(12, result[0][2].Value);
            }
            {
                var list = new List<int>();
                var result = list.ToColumnsVertical(4, true);
                Assert.AreEqual(0, result.Length); // Number of rows
            }
            {
                var list = new List<int>();
                var result = list.ToColumnsVertical(4, false);
                Assert.AreEqual(0, result.Length); // Number of rows
            }
            {
                var list = new string[] { "John", "Jack" }.ToList();
                var result = list.ToColumnsVertical(4, true);
                Assert.AreEqual(1, result.Length); // Number of rows
                Assert.AreEqual("John", result[0][0].Value);
                Assert.AreEqual("Jack", result[0][1].Value);
                Assert.AreEqual(null, result[0][2].Value);
                Assert.AreEqual(null, result[0][3].Value);
            }
            {
                var list = new string?[] { "John", "Jack" }.ToList();
                var result = list.ToColumnsVertical(4, true);
                Assert.AreEqual(1, result.Length); // Number of rows
                Assert.AreEqual("John", result[0][0].Value);
                Assert.AreEqual("Jack", result[0][1].Value);
                Assert.AreEqual(null, result[0][2].Value);
                Assert.AreEqual(null, result[0][3].Value);
            }
        }
    }
}
