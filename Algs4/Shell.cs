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
    ///  Sorts a sequence of strings from standard input using shell sort.
    ///   
    ///  % more tiny.txt
    ///  S O R T E X A M P L E
    ///  
    ///  algs Shell < tiny.txt
    ///  A E E L M O P R S T X                 [ one string per line ]
    ///    
    ///  % more words3.txt
    ///  bed bug dad yes zoo ... all bad yet
    ///  
    ///  algs Shell < words3.txt
    ///  all bad bed bug dad ... yes yet zoo    [ one string per line ]
    ///   
    ///  example: user wants to type string from command line    
    ///  algs Shell                             [ enter]
    ///  9 8 7 6 1 2 3 4 5                      [ type into command line + enter ]  
    ///                                         [ CTRL+Z + enter ]
    ///  note:  windows => end of file is CTRL+Z       
    ///  **************************************************************************



    /// <summary>
    ///  The <tt>Shell</tt> class provides static methods for sorting an
    /// array using Shellsort with Knuth's increment sequence (1, 4, 13, 40, ...).
    ///  <p> </p>
    ///  For additional documentation, see <a href="http://algs4.cs.princeton.edu/21elementary">Section 2.1</a> of
    ///  <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.
    ///
    ///  <author>Robert Sedgewick</author>
    ///  <author>Kevin Wayne</author>
    /// </summary>

    public class Shell
    {
        // This class should not be instantiated.
        private Shell() { }

        /// <summary>
        /// Rearranges the array in ascending order, using the natural order.
        /// </summary>
        /// <param name="a">a the array to be sorted</param>
        public static void Sort(IComparable[] a, IComparer c = null)
        {
            //make sure we have the comparer passed by argument or create default
            IComparer comparer = c ?? Comparer<object>.Default;

            int N = a.Length;

            int h = 1;

            //3*x +1 -> 1, 4, 13, 40, 121, 364, ..
            while (h < N / 3)
            {
                h = 3 * h + 1;
            }

            while (h >= 1)
            {

                //h- sort array
                for (int i = h; i < N; i++)
                {
                    for (int j = i; j >= h && less(comparer, a[j], a[j - h]); j -= h)
                    {
                        exch(a, j, j - h);
                    }
                }

                Debug.Assert(isHsorted(a, comparer, h));
                h = h / 3;
            }

            Debug.Assert(isSorted(a, comparer));
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
        /// is the array h-sorted?
        /// </summary>
        /// <param name="a">input array</param>
        /// <param name="c">comparer</param>
        /// <param name="h">h step</param>
        /// <returns>true if the array is h-sorted</returns>
        private static bool isHsorted(IComparable[] a, IComparer c, int h)
        {
            for (int i = h; i < a.Length; i++)
                if (less(c, a[i], a[i - h])) return false;
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
            Shell.Sort(a);
            show(a);
        }
    }
}
