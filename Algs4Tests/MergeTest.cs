using Algs4;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algs4Tests
{
    [TestFixture]
    public class MergeTest
    {
        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_Elemntary_IsSorted()
        {
            IComparable[] unsortedArray = "4321".Select(c => c.ToString()).ToArray();
            IComparable[] expectedArray = "1234".Select(c => c.ToString()).ToArray();

            Merge.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_Tiny_IsSorted()
        {
            IComparable[] unsortedArray = "SORTEXAMPLE".Select(c => c.ToString()).ToArray();
            IComparable[] expectedArray = "AEELMOPRSTX".Select(c => c.ToString()).ToArray();

            Merge.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_Words3_IsSorted()
        {
            IComparable[] unsortedArray = "bed bug dad yes zoo now for tip ilk dim tag jot sob nob sky hut men egg few jay owl joy rap gig wee was wad fee tap tar dug jam all bad yet".Split(' ');
            IComparable[] expectedArray = "all bad bed bug dad dim dug egg fee few for gig hut ilk jam jay jot joy men nob now owl rap sky sob tag tap tar tip wad was wee yes yet zoo".Split(' ');

            Merge.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_Sorted_IsSorted()
        {
            IComparable[] unsortedArray = "AEELMOPRSTX".Select(c => c.ToString()).ToArray();
            IComparable[] expectedArray = "AEELMOPRSTX".Select(c => c.ToString()).ToArray();

            Merge.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_TimeTable_IsStable()
        {
            
            IEnumerable<TimeTable> sortedSample = TimeTable.GetTestSample();
            TimeTable[] stableByMergeSortSample = TimeTable.GetTestSample().ToArray();

            //rearrange an array of objects in uniformly random order
            Knuth.Shuffle<TimeTable>(stableByMergeSortSample);
            
            //sort by TIME
            Merge.Sort(stableByMergeSortSample, new TimeTableComparerByTime());
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
            Merge.Sort(stableByMergeSortSample, new TimeTableComparerByCity());
           
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

            CollectionAssert.AreEqual(sortedSample, stableByMergeSortSample);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_ReverseSorted_IsSorted()
        {
            IComparable[] unsortedArray = "XTSRPOMLEEA".Select(c => c.ToString()).ToArray();
            IComparable[] expectedArray = "AEELMOPRSTX".Select(c => c.ToString()).ToArray();

            Merge.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_OneItem_IsSorted()
        {
            IComparable[] unsortedArray = new string[1] { "A" };

            IComparable[] expectedArray = new string[1] { "A" };

            Merge.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_EmptyItems_IsSorted()
        {
            IComparable[] unsortedArray = Enumerable.Range(1, 10).Select(x => string.Empty).ToArray();

            IComparable[] expectedArray = Enumerable.Range(1, 10).Select(x => string.Empty).ToArray();

            Merge.Sort(unsortedArray);

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

            Merge.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }

        [Test]
        //[MethodName_StateUnderTest_ExpectedBehavior]
        public void Sort_NullItem_IsSorted()
        {
            IComparable[] unsortedArray = new string[0];

            IComparable[] expectedArray = new string[0];

            Merge.Sort(unsortedArray);

            CollectionAssert.AreEqual(expectedArray, unsortedArray);
        }
    }
}
