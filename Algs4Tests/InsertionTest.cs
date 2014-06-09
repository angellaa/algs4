using Algs4;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algs4Tests
{
    [TestFixture]
    public class InsertionTest
    {
        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_Elementary_IsSorted()
        {
            IComparable[] unsortedArray = "54321".Select(c => c.ToString()).ToArray();
            IComparable[] expectedArray = "12345".Select(c => c.ToString()).ToArray();

            Insertion.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_Tiny_IsSorted()
        {
            IComparable[] unsortedArray = "SORTEXAMPLE".Select(c => c.ToString()).ToArray();
            IComparable[] expectedArray = "AEELMOPRSTX".Select(c => c.ToString()).ToArray();

            Insertion.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_Words3_IsSorted()
        {
            IComparable[] unsortedArray = "bed bug dad yes zoo now for tip ilk dim tag jot sob nob sky hut men egg few jay owl joy rap gig wee was wad fee tap tar dug jam all bad yet".Split(' ');
            IComparable[] expectedArray = "all bad bed bug dad dim dug egg fee few for gig hut ilk jam jay jot joy men nob now owl rap sky sob tag tap tar tip wad was wee yes yet zoo".Split(' ');

            Insertion.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void IndexSort_Elementary_IsSorted()
        {
            IComparable[] unsortedArray = "edcba".Select(c => c.ToString()).ToArray();
            IEnumerable<int> expectedArray = new List<int>() { 4, 3, 2, 1, 0 };

            var sortedIndex = Insertion.IndexSort(unsortedArray);

            CollectionAssert.AreEqual(sortedIndex, expectedArray);

            unsortedArray = "ADBC".Select(c => c.ToString()).ToArray();
            //a[0]->A
            //a[2]->B
            //a[3]->C
            //a[1]->D
            expectedArray = new List<int>() { 0, 2, 3, 1};

            sortedIndex = Insertion.IndexSort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, sortedIndex);

        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_TimeTable_IsStable()
        {

            IEnumerable<TimeTable> sortedSample = TimeTable.GetTestSample();
            TimeTable[] stableByInsertionSortSample = TimeTable.GetTestSample().ToArray();

            //rearrange an array of objects in uniformly random order
            Knuth.Shuffle<TimeTable>(stableByInsertionSortSample);

            //sort by TIME
            Insertion.Sort(stableByInsertionSortSample, new TimeTableComparerByTime());
            //City: Chicago, Time:09:00:00
            //City: Phoenix, Time:09:00:03
            //City: Houston, Time:09:00:13
            //City: Chicago, Time:09:00:59
            //City: Houston, Time:09:01:10
            //City: Chicago, Time:09:03:13
            //City: Seattle, Time:09:10:11
            //City: Seattle, Time:09:10:25
            //City: Phoenix, Time:09:14:25
            //City: Chicago, Time:09:19:32
            //City: Chicago, Time:09:19:46
            //City: Chicago, Time:09:21:05
            //City: Seattle, Time:09:22:43
            //City: Seattle, Time:09:22:54
            //City: Chicago, Time:09:25:52
            //City: Chicago, Time:09:35:21
            //City: Seattle, Time:09:36:14
            //City: Phoenix, Time:09:37:44

            //sort by CITY
            Insertion.Sort(stableByInsertionSortSample, new TimeTableComparerByCity());

            //algorithms is STABLE if sorted by time is preserved for the same city
            //City: Chicago, Time:09:00:00
            //City: Chicago, Time:09:00:59
            //City: Chicago, Time:09:03:13
            //City: Chicago, Time:09:19:32
            //City: Chicago, Time:09:19:46
            //City: Chicago, Time:09:21:05
            //City: Chicago, Time:09:25:52
            //City: Chicago, Time:09:35:21
            //City: Houston, Time:09:00:13
            //City: Houston, Time:09:01:10
            //City: Phoenix, Time:09:00:03
            //City: Phoenix, Time:09:14:25
            //City: Phoenix, Time:09:37:44
            //City: Seattle, Time:09:10:11
            //City: Seattle, Time:09:10:25
            //City: Seattle, Time:09:22:43
            //City: Seattle, Time:09:22:54
            //City: Seattle, Time:09:36:14

            CollectionAssert.AreEqual(sortedSample, stableByInsertionSortSample);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_ReverseSorted_IsSorted()
        {
            IComparable[] unsortedArray = "XTSRPOMLEEA".Select(c => c.ToString()).ToArray();
            IComparable[] expectedArray = "AEELMOPRSTX".Select(c => c.ToString()).ToArray();

            Insertion.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_OneItem_IsSorted()
        {
            IComparable[] unsortedArray = new string[1] { "A" };

            IComparable[] expectedArray = new string[1] { "A" };

            Insertion.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_EmptyItems_IsSorted()
        {
            IComparable[] unsortedArray = Enumerable.Range(1, 10).Select(x => string.Empty).ToArray();

            IComparable[] expectedArray = Enumerable.Range(1, 10).Select(x => string.Empty).ToArray();

            Insertion.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_EmptyAndNonEpmtyItems_IsSorted()
        {
            var empty = Enumerable.Range(1, 10).Select(x => string.Empty).ToArray(); ;
            var newGuid = Guid.NewGuid().ToString().Select(c => c.ToString());

            var newGuidSorted = newGuid.ToArray();
            Array.Sort(newGuidSorted);

            IComparable[] unsortedArray = newGuid.Concat(empty).ToArray();
            IComparable[] expectedArray = empty.Concat(newGuidSorted).ToArray();

            Array.Sort(newGuidSorted);

            Insertion.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_NullItem_IsSorted()
        {
            IComparable[] unsortedArray = new string[0];

            IComparable[] expectedArray = new string[0];

            Insertion.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }
    }
}
