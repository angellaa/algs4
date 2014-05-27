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
    ///  Sorts a sequence of strings from standard input using insertion sort.
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
    ///  % java Insertion < words3.txt
    ///  all bad bed bug dad ... yes yet zoo    [ one string per line ]
    ///  **************************************************************************



    /// <summary>
    ///  The <tt>Insertion</tt> class provides static methods for sorting an
    ///  array using insertion sort.
    ///  <p> </p>
    ///  For additional documentation, see <a href="http://algs4.cs.princeton.edu/21elementary">Section 2.1</a> of
    ///  <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.
    ///
    ///  <author>Robert Sedgewick</author>
    ///  <author>Kevin Wayne</author>
    /// </summary>
    public class Insertion
    {
        // This class should not be instantiated.
        private Insertion() { }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a the array to be sorted</param>
        /// <param name="c">c the comparator specifying the order</param>
        public static void Sort(Object[] a, IComparer c = null)
        {
            //make sure we have the comparer passed by argument or create default
            IComparer comparer = c ?? Comparer<object>.Default;

            int N = a.Length;
            for (int i = 0; i < N; i++)
            {
                for (int j = i; j > 0 && less(comparer, a[j], a[j - 1]); j--)
                {
                    exch(a, j, j - 1);
                }

                Debug.Assert(isSorted(a, comparer, 0, i));
            }
            Debug.Assert(isSorted(a, comparer));
        }

        /// <summary>
        /// Returns a permutation that gives the elements in the array in ascending order.
        /// </summary>
        /// <param name="a">a the array</param>
        /// <returns>return a permutation that gives the elements in a[] in ascending order
        /// do not change the original array a[]</returns>
        public static int[] IndexSort(IComparable[] a)
        {
            int N = a.Length;
            int[] index = new int[N];
            for (int i = 0; i < N; i++)
                index[i] = i;

            for (int i = 0; i < N; i++)
                for (int j = i; j > 0 && less(a[index[j]], a[index[j - 1]]); j--)
                    exch(index, j, j - 1);

            return index;
        }

        /***********************************************************************
         ///  Helper sorting functions
         ***********************************************************************/

        /// <summary>
        /// is v < w ?
        /// </summary>
        /// <param name="v">first argument to compare with second</param>
        /// <param name="w">second argument to compare with first</param>
        /// <returns>true - if v is less than w; false - otherwise</returns>
        private static bool less(IComparable v, IComparable w)
        {
            return (v.CompareTo(w) < 0);
        }

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

        /// <summary>
        ///  exchange a[i] and a[j]  (for indirect sort)
        /// </summary>
        /// <param name="a">array of int</param>
        /// <param name="i">exchange item at position i</param>
        /// <param name="j">exchange item at position j</param>
        private static void exch(int[] a, int i, int j)
        {
            int swap = a[i];
            a[i] = a[j];
            a[j] = swap;
        }

        /***********************************************************************
         ///  Check if array is sorted - useful for debugging
         ***********************************************************************/


        /// <summary>
        /// is the array a[] sorted?
        /// </summary>
        /// <param name="a">input array</param>
        /// <param name="c">comparer</param>
        /// <returns>true if given array is sorted; false otherwise</returns>
        private static bool isSorted(Object[] a, IComparer c)
        {
            return isSorted(a, c, 0, a.Length - 1);
        }

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
            Insertion.Sort(a);
            show(a);
        }
    }
}