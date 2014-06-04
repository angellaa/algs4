using Algs4;
using NUnit.Framework;

namespace Algs4Tests
{
    [TestFixture]
    public class KnuthTest
    {
        [Test]
        public void Random()
        {
            var notExpected = new int[5] { 1, 2, 3, 4, 5};

            for (int i = 0; i < 1000; i++)
            {
                var a = new int[5] { 1, 2, 3, 4, 5};
                Knuth.Shuffle(a);
                CollectionAssert.AreNotEqual(a, notExpected);
            }
        }
    }
}
