/***********************************************************************************
 *
 *  Execution:   algs4 MSD < input.txt
 *
 *    - Sort a String[] array of N extended ASCII strings (R = 256), each of length W.
 *
 *    - Sort an int[] array of N 32-bit integers, treating each integer as
 *      a sequence of W = 4 bytes (R = 256).
 *
 *
 *  % algs4 MSD < shells.txt 
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
 ***********************************************************************************/

using System;
using System.Diagnostics;
using StdLib;

namespace Algs4
{
    public class MSD
    {
        private const int BITS_PER_BYTE = 8;
        private const int BITS_PER_INT = 32; // each Java int is 32 bits 
        private const int R = 256; // extended ASCII alphabet size
        private const int CUTOFF = 15; // cutoff to insertion sort

        // sort array of strings
        public static void Sort(String[] a)
        {
            int N = a.Length;
            String[] aux = new String[N];
            Sort(a, 0, N - 1, 0, aux);
        }

        // return dth character of s, -1 if d = length of string
        private static int CharAt(String s, int d)
        {
            Debug.Assert(d >= 0 && d <= s.Length);
            if (d == s.Length) return -1;
            return s[d];
        }

        // sort from a[lo] to a[hi], starting at the dth character
        private static void Sort(String[] a, int lo, int hi, int d, String[] aux)
        {
            // cutoff to insertion sort for small subarrays
            if (hi <= lo + CUTOFF)
            {
                Insertion(a, lo, hi, d);
                return;
            }

            // compute frequency counts
            int[] count = new int[R + 2];
            for (int i = lo; i <= hi; i++)
            {
                int c = CharAt(a[i], d);
                count[c + 2]++;
            }

            // transform counts to indicies
            for (int r = 0; r < R + 1; r++)
                count[r + 1] += count[r];

            // distribute
            for (int i = lo; i <= hi; i++)
            {
                int c = CharAt(a[i], d);
                aux[count[c + 1]++] = a[i];
            }

            // copy back
            for (int i = lo; i <= hi; i++)
                a[i] = aux[i - lo];


            // recursively sort for each character
            for (int r = 0; r < R; r++)
                Sort(a, lo + count[r], lo + count[r + 1] - 1, d + 1, aux);
        }
        
        // insertion sort a[lo..hi], starting at dth character
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
            // assert v.substring(0, d).equals(w.substring(0, d));
            for (int i = d; i < Math.Min(v.Length, w.Length); i++)
            {
                if (v[i] < w[i]) return true;
                if (v[i] > w[i]) return false;
            }
            return v.Length < w.Length;
        }
        
        // MSD sort array of integers
        public static void Sort(int[] a)
        {
            int N = a.Length;
            int[] aux = new int[N];
            Sort(a, 0, N - 1, 0, aux);
        }

        // MSD sort from a[lo] to a[hi], starting at the dth byte
        private static void Sort(int[] a, int lo, int hi, int d, int[] aux)
        {
            // cutoff to insertion sort for small subarrays
            if (hi <= lo + CUTOFF)
            {
                Insertion(a, lo, hi, d);
                return;
            }

            // compute frequency counts (need R = 256)
            int[] count = new int[R + 1];
            int mask = R - 1; // 0xFF;
            int shift = BITS_PER_INT - BITS_PER_BYTE * d - BITS_PER_BYTE;
            for (int i = lo; i <= hi; i++)
            {
                int c = (a[i] >> shift) & mask;
                count[c + 1]++;
            }

            // transform counts to indicies
            for (int r = 0; r < R; r++)
                count[r + 1] += count[r];

/*************BUGGGY
        // for most significant byte, 0x80-0xFF comes before 0x00-0x7F
        if (d == 0) {
            int shift1 = count[R] - count[R/2];
            int shift2 = count[R/2];
            for (int r = 0; r < R/2; r++)
                count[r] += shift1;
            for (int r = R/2; r < R; r++)
                count[r] -= shift2;
        }
************************************/
            // distribute
            for (int i = lo; i <= hi; i++)
            {
                int c = (a[i] >> shift) & mask;
                aux[count[c]++] = a[i];
            }

            // copy back
            for (int i = lo; i <= hi; i++)
                a[i] = aux[i - lo];

            // no more bits
            if (d == 4) return;

            // recursively sort for each character
            if (count[0] > 0)
                Sort(a, lo, lo + count[0] - 1, d + 1, aux);
            for (int r = 0; r < R; r++)
                if (count[r + 1] > count[r])
                    Sort(a, lo + count[r], lo + count[r + 1] - 1, d + 1, aux);
        }

        // insertion sort a[lo..hi], starting at dth character
        private static void Insertion(int[] a, int lo, int hi, int d)
        {
            for (int i = lo; i <= hi; i++)
                for (int j = i; j > lo && a[j] < a[j - 1]; j--)
                    Exch(a, j, j - 1);
        }

        // exchange a[i] and a[j]
        private static void Exch(int[] a, int i, int j)
        {
            int temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }
    }

    public class MSDExample
    {
        public static void Main(String[] args)
        {
            String[] a = StdIn.ReadAllStrings();
            int N = a.Length;
            MSD.Sort(a);
            for (int i = 0; i < N; i++)
                Console.WriteLine(a[i]);
        }        
    }
}