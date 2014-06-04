using System;
using System.Collections;
using System.Collections.Generic;
using StdLib;
using System.Diagnostics;

namespace Algs4
{
    ///  *************************************************************************
    ///  Dependencies: StdLib StdIn
    ///  Data files:   http://algs4.cs.princeton.edu/21elementary/tiny.txt
    ///                http://algs4.cs.princeton.edu/21elementary/words3.txt
    ///   
    ///  Sorts a sequence of strings from standard input using Quick sort.
    ///   
    ///  % more tiny.txt
    ///  S O R T E X A M P L E
    ///  
    ///  % java Selection < tiny.txt
    ///  A E E L M O P R S T X                 [ one string per line ]
    ///    
    ///  % more words3.txt
    ///  bed bug dad yes zoo ... all bad yet
    ///  
    ///  % java Quick < words3.txt
    ///  all bad bed bug dad ... yes yet zoo    [ one string per line ]
    ///  **************************************************************************



    /// <summary>
    ///  The <tt>Quick</tt> class provides static methods for sorting an
    ///  array using Quick sort.
    ///  <p> </p>
    ///  For additional documentation, see <a href="http://algs4.cs.princeton.edu/21elementary">Section 2.1</a> of
    ///  <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.
    ///
    ///  <author>Robert Sedgewick</author>
    ///  <author>Kevin Wayne</author>
    /// </summary>

    public class Quick
    {
        // This class should not be instantiated.
        private Quick() { }


        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a the array to be sorted</param>
        public static void Sort(IComparable[] a, IComparer c = null)
        {
            //make sure we have the comparer passed by argument or create default
            IComparer comparer = c ?? Comparer<object>.Default;
            Knuth.Shuffle(a);

            sort(a, comparer, 0, a.Length - 1);
        }

        /// <summary>
        /// Quicksort the sub-array from a[lo] to a[hi]
        /// </summary>
        /// <param name="a">the array to be sorted</param>
        /// <param name="c">c the comparator specifying the order</param>
        /// <param name="lo">lower (inclusive) bound of the range of elements</param>
        /// <param name="hi">upper (inclusive) bound of the range of elements</param>
        private static void sort(IComparable[] a, IComparer c, int lo, int hi)
        {
            if (hi <= lo)
            {
                return;
            }

            var pivot = partitioning(a, c, lo, hi);

            sort(a, c, lo, pivot - 1);
            sort(a, c, pivot + 1, hi);
            Debug.Assert(isSorted(a, c, lo, hi));
        }

        /// <summary>
        /// partition the subarray a[lo..hi] so that a[lo..j-1] <= a[j] <= a[j+1..hi]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="aux"></param>
        /// <param name="lo"></param>
        /// <param name="mid"></param>
        /// <param name="hi"></param>
        private static int partitioning(IComparable[] a, IComparer c, int lo, int hi)
        {

            // {k) i               j
            //
            int i = lo;
            int j = hi + 1;
            IComparable v = a[lo];

            while(true)
            {
                // find item on lo to swap
                while (less(c, a[++i], v))
                {
                    if (i == hi)
                    {
                        break;
                    }
                }

                // find item on hi to swap
                while (less(c, v, a[--j]))
                {
                    // redundant since a[lo] acts as sentinel
                    if (j == lo)
                    {
                        break;
                    }
                }

                // check if pointers cross
                if (i >= j)
                {
                    break;
                }

                //swap i and j since they haven't crossed over yet 
                //and they both violate the formula a[lo..j-1] <= a[j] <= a[j+1..hi]
                exch(a, i, j);
            }

            // put partitioning item v at a[j]
            exch(a, lo, j);

            // now, a[lo .. j-1] <= a[j] <= a[j+1 .. hi]
            return j;
        }


        /***********************************************************************
         ///  Helper sorting functions
         ***********************************************************************/

        /// <summary>
        /// is v < w ?
        /// </summary>
        /// <param name="c">comparer</param>
        /// <param name="v">first argument to compare with second</param>
        /// <param name="w">second argument to compare with first</param>
        /// <returns>true - if v is less than w; false - otherwise</returns>
        private static bool less(IComparer c, Object v, Object w)
        {
            return (c.Compare(v, w) < 0);
        }

        /// <summary>
        /// Exchange a[i] and a[j]
        /// </summary>
        /// <param name="a">array of objects</param>
        /// <param name="i">exchange item at position i</param>
        /// <param name="j">exchange item at position j</param>
        private static void exch(Object[] a, int i, int j)
        {
            Object swap = a[i];
            a[i] = a[j];
            a[j] = swap;
        }

        /***********************************************************************
         ///  Check if array is sorted - useful for debugging
         ***********************************************************************/

        /// <summary>
        /// is the array sorted from a[lo] to a[hi]
        /// </summary>
        /// <param name="a">input array</param>
        /// <param name="c">comparer</param>
        /// <param name="lo">low value index</param>
        /// <param name="hi">high value index</param>
        /// <returns>true if given array is sorted; false otherwise</returns>
        private static bool isSorted(Object[] a, IComparer c, int lo, int hi)
        {
            for (int i = lo + 1; i <= hi; i++)
                if (less(c, a[i], a[i - 1])) return false;
            return true;
        }

        /// <summary>
        /// print array to standard output
        /// </summary>
        /// <param name="a">input array</param>
        private static void show(IComparable[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
                Debug.WriteLine(a[i]);
            }
        }

        /// <summary>
        /// Reads in a sequence of strings from standard input; selection sorts them; 
        /// and prints them to standard output in ascending order. 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(String[] args)
        {
            String[] a = StdIn.ReadAllStrings();
            Quick.Sort(a);
            show(a);
        }
    }
}
