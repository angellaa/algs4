/***********************************************************************************
 *
 *  Execution:   algs4 Quick3string < input.txt
 *
 *  Reads string from standard input and 3-way string quicksort them.
 *
 *  > algs4 Quick3string < shells.txt
 *  are
 *  by
 *  sea
 *  seashells
 *  seashells
 *  sells
 *  sells
 *  she
 *  she
 *  shells
 *  shore
 *  surely
 *  the
 *  the
 *
 *
 ***********************************************************************************/

using System;
using System.Diagnostics;
using System.Globalization;
using StdLib;

namespace Algs4
{
    public class Quick3string
    {
        private const int CUTOFF = 15; // cutoff to insertion sort

        // sort the array a[] of strings
        public static void Sort(String[] a)
        {
            StdRandom.Shuffle(a);
            Sort(a, 0, a.Length - 1, 0);
            Debug.Assert(IsSorted(a));
        }

        // return the dth character of s, -1 if d = length of s
        private static int CharAt(String s, int d)
        {
            Debug.Assert(d >= 0 && d <= s.Length);
            if (d == s.Length) return -1;
            return s[d];
        }
        
        // 3-way string quicksort a[lo..hi] starting at dth character
        private static void Sort(String[] a, int lo, int hi, int d)
        {
            // cutoff to insertion sort for small subarrays
            if (hi <= lo + CUTOFF)
            {
                Insertion(a, lo, hi, d);
                return;
            }

            int lt = lo, gt = hi;
            int v = CharAt(a[lo], d);
            int i = lo + 1;
            while (i <= gt)
            {
                int t = CharAt(a[i], d);
                if (t < v) Exch(a, lt++, i++);
                else if (t > v) Exch(a, i, gt--);
                else i++;
            }

            // a[lo..lt-1] < v = a[lt..gt] < a[gt+1..hi]. 
            Sort(a, lo, lt - 1, d);
            if (v >= 0) Sort(a, lt, gt, d + 1);
            Sort(a, gt + 1, hi, d);
        }

        // sort from a[lo] to a[hi], starting at the dth character
        private static void Insertion(String[] a, int lo, int hi, int d)
        {
            for (int i = lo; i <= hi; i++)
                for (int j = i; j > lo && Less(a[j], a[j - 1], d); j--)
                    Exch(a, j, j - 1);
        }

        // exchange a[i] and a[j]
        private static void Exch(String[] a, int i, int j)
        {
            String temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        // is v less than w, starting at character d
        private static bool Less(String v, String w, int d)
        {
            Debug.Assert(v.Substring(0, d).Equals(w.Substring(0, d)));
            for (int i = d; i < Math.Min(v.Length, w.Length); i++)
            {
                if (v[i] < w[i]) return true;
                if (v[i] > w[i]) return false;
            }
            return v.Length < w.Length;
        }

        // is the array sorted
        private static bool IsSorted(String[] a)
        {
            for (int i = 1; i < a.Length; i++)
                if (String.Compare(a[i], a[i - 1], StringComparison.Ordinal) < 0) return false;
            return true;
        }
    }

    public class Quick3stringExample
    {
        public static void Main(String[] args)
        {
            // read in the strings from standard input
            String[] a = StdIn.ReadAllStrings();
            int N = a.Length;

            // sort the strings
            Quick3string.Sort(a);

            // print the results
            for (int i = 0; i < N; i++)
                Console.WriteLine(a[i]);
        }        
    }
}