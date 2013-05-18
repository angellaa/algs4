/*************************************************************************
 *  Execution:    algs4 BinarySearch whitelist.txt < input.txt
 *
 *  Data files:   http://algs4.cs.princeton.edu/11model/tinyW.txt
 *                http://algs4.cs.princeton.edu/11model/tinyT.txt
 *                http://algs4.cs.princeton.edu/11model/largeW.txt
 *                http://algs4.cs.princeton.edu/11model/largeT.txt
 *
 *  > algs4 BinarySearch tinyW.txt < tinyT.txt
 *  50
 *  99
 *  13
 *
 *  > algs4 BinarySearch largeW.txt < largeT.txt
 *  499569
 *  984875
 *  295754
 *  207807
 *  140925
 *  161828
 *  [3,675,966 total values]
 *  
 *************************************************************************/

using System;
using StdLib;

namespace Algs4
{
    public class BinarySearch
    {
        /// <summary>
        /// Return the index of the element of the array that contains the key.
        /// If the element does not exist returns -1.
        /// Precondition: array a[] is sorted.
        /// </summary>
        public static int Rank(int key, int[] a)
        {
            int lo = 0;
            int hi = a.Length - 1;

            while (lo <= hi) 
            {
                // Key is in a[lo..hi] or not present.
                int mid = lo + (hi - lo) / 2;

                if      (key < a[mid]) hi = mid - 1;
                else if (key > a[mid]) lo = mid + 1;
                else return mid;
            }

            return -1;            
        }

        public static void Main(string[] args)
        {
            int[] whitelist = new In(args[0]).ReadAllInts();

            Array.Sort(whitelist);

            // read key; print if not in whitelist
            while (!StdIn.IsEmpty())
            {
                int key = StdIn.ReadInt();
                
                if (Rank(key, whitelist) == -1)
                {
                    Console.WriteLine(key);
                }
            }
        }
    }
}
