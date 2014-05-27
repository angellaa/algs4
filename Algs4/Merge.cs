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
    ///  Sorts a sequence of strings from standard input using Merge sort.
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
    ///  % java Merge < words3.txt
    ///  all bad bed bug dad ... yes yet zoo    [ one string per line ]
    ///  **************************************************************************



    /// <summary>
    ///  The <tt>Merge</tt> class provides static methods for sorting an
    ///  array using Merge sort.
    ///  <p> </p>
    ///  For additional documentation, see <a href="http://algs4.cs.princeton.edu/21elementary">Section 2.1</a> of
    ///  <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.
    ///
    ///  <author>Robert Sedgewick</author>
    ///  <author>Kevin Wayne</author>
    /// </summary>

    public class Merge
    {
        // This class should not be instantiated.
        private Merge() { }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">array to be sorted</param>
        /// <param name="c">comparator specifying the order</param>
        public static void Sort(IComparable[] a, IComparer c = null)
        {
            //make sure we have the comparer passed by argument or create default
            IComparer comparer = c ?? Comparer<object>.Default;

            IComparable[] aux = new IComparable[a.Length];
            sort(a, aux, comparer, 0, a.Length - 1);
        }

        /// <summary>
        /// mergesort the sub-array from a[lo] to a[hi] 
        /// </summary>
        /// <param name="a">the array to be sorted</param>
        /// <param name="aux">auxiliary array</param>
        /// <param name="c">comparator specifying the order</param>
        /// <param name="lo">lower (inclusive) bound of the range of elements</param>
        /// <param name="hi">upper (inclusive) bound of the range of elements</param>
        private static void sort(IComparable[] a, IComparable[] aux, IComparer c, int lo, int hi)
        {
            if (hi <= lo)
            {
                return;
            }

            int mid = lo + (hi - lo) / 2;

            sort(a, aux, c, lo, mid);
            sort(a, aux, c, mid + 1, hi);
            merge(a, aux, c, lo, mid, hi);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">the array to be sorted</param>
        /// <param name="aux">auxiliary array</param>
        /// <param name="c">comparator specifying the order</param>
        /// <param name="lo">lower (inclusive) bound of the range of elements</param>        
        /// <param name="mid">middle point</param>
        /// <param name="hi">upper (inclusive) bound of the range of elements</param>
        private static void merge(IComparable[] a, IComparable[] aux, IComparer c, int lo, int mid, int hi)
        {
            Debug.Assert(isSorted(a, c, lo, mid));
            Debug.Assert(isSorted(a, c, mid + 1, hi));

            for (int k = lo; k <= hi; k++)
            {
                aux[k] = a[k];
            }

            int i = lo;
            int j = mid + 1;

            for (int k = lo; k <= hi; k++)
            {
                if (i > mid)                        a[k] = aux[j++];
                else if (j > hi)                    a[k] = aux[i++];
                else if (less(c, aux[j], aux[i]))   a[k] = aux[j++];
                else                                a[k] = aux[i++];
            }

            Debug.Assert(isSorted(a, c, lo, hi));
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
            Merge.Sort(a);
            show(a);
        }
    }
}
