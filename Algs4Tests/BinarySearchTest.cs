using Algs4;
using NUnit.Framework;

namespace Algs4Tests
{
    [TestFixture]
    public class BinarySearchTest
    {
        [Test]
        public void SingleElement()
        {
            int[] v = new[] { 5 };

            Assert.AreEqual(0, BinarySearch.Rank(5, v));

            Assert.AreEqual(-1, BinarySearch.Rank(2, v));
        }
        
        [Test]
        public void MultipleElements()
        {
            int[] v = new[] { 1, 4, 5, 8, 9, 10, 15, 17, 20 };

            Assert.AreEqual(0, BinarySearch.Rank(1, v));
            Assert.AreEqual(1, BinarySearch.Rank(4, v));
            Assert.AreEqual(2, BinarySearch.Rank(5, v));
            Assert.AreEqual(3, BinarySearch.Rank(8, v));
            Assert.AreEqual(4, BinarySearch.Rank(9, v));
            Assert.AreEqual(5, BinarySearch.Rank(10, v));
            Assert.AreEqual(6, BinarySearch.Rank(15, v));
            Assert.AreEqual(7, BinarySearch.Rank(17, v));
            Assert.AreEqual(8, BinarySearch.Rank(20, v));

            Assert.AreEqual(-1, BinarySearch.Rank(0, v));
            Assert.AreEqual(-1, BinarySearch.Rank(2, v));
            Assert.AreEqual(-1, BinarySearch.Rank(3, v));
            Assert.AreEqual(-1, BinarySearch.Rank(6, v));
            Assert.AreEqual(-1, BinarySearch.Rank(7, v));
            Assert.AreEqual(-1, BinarySearch.Rank(11, v));
            Assert.AreEqual(-1, BinarySearch.Rank(12, v));
            Assert.AreEqual(-1, BinarySearch.Rank(13, v));
            Assert.AreEqual(-1, BinarySearch.Rank(14, v));
            Assert.AreEqual(-1, BinarySearch.Rank(16, v));
            Assert.AreEqual(-1, BinarySearch.Rank(18, v));
            Assert.AreEqual(-1, BinarySearch.Rank(19, v));
        }
    }
}