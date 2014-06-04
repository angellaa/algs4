using StdLib;
using System;
using System.Diagnostics;

namespace Algs4
{
    /// <summary>
    ///  The Knuth class provides a client for reading in a 
    ///  sequence of strings and shuffling them using the Knuth (or Fisher-Yates)
    ///  shuffling algorithm. This algorithm guarantees to rearrange the
    ///  elements in uniformly random order, under
    ///  the assumption that Random(new Guid().GetHashCode()) generates independent and
    ///  uniformly distributed numbers.
    ///  <p>
    ///  For additional documentation, see <a href="http://algs4.cs.princeton.edu/11model">Section 1.1</a> of
    ///  Algorithms, 4th Edition by Robert Sedgewick and Kevin Wayne.
    ///
    ///  <author>Robert Sedgewick</author>
    ///  <author>Kevin Wayne</author>
    /// </summary>
    public class Knuth
    {
        /// <summary>
        /// this class should not be instantiated
        /// </summary>
        private Knuth() { }

        /// <summary>
        /// Rearranges an array of objects in uniformly random order
        /// (under the assumption that <tt>Random(new Guid().GetHashCode())</tt> generates independent
        /// and uniformly distributed numbers.
        /// 
        /// the random interval could be taken either from 0(inclusively) to i(inclusively)
        /// OR
        /// from i(inclusively) to  N-1 (inclusively)
        /// </summary>
        /// <see cref="StdRandom"/>
        /// <param name="a">a the array to be shuffled</param>
        public static void Shuffle<T>(T[] a)
        {
            //set up randomizer based on GUID
            Random random = new Random(new Guid().GetHashCode());
            
            int N = a.Length;
            for (int i = 0; i < N; i++)
            {
                //take the item between 0 and i (inclusively)
                int r = random.Next(i + 1);
                T swap = a[r];
                a[r] = a[i];
                a[i] = swap;
            }
        }

        /// <summary>
        /// Rearranges an array of objects in uniformly random order
        /// (under the assumption that <tt>Random(new Guid().GetHashCode())</tt> generates independent
        /// and uniformly distributed numbers.
        /// 
        /// the random interval could be taken either from 0(inclusively) to i(inclusively)
        /// OR
        /// from i(inclusively) to  N-1 (inclusively)
        /// </summary>
        /// <see cref="StdRandom"/>
        /// <param name="a">a the array to be shuffled</param>
        /// <param name="random">given random object, used when we like to reuse the same random object again</param>
        public static void Shuffle<T>(T[] a, Random random)
        {
            Debug.Assert(random != null);

            int N = a.Length;
            for (int i = 0; i < N; i++)
            {
                //take the item between 0 and i (inclusively)
                int r = random.Next(i + 1);
                T swap = a[r];
                a[r] = a[i];
                a[i] = swap;
            }
        }

        /// <summary>
        /// Reads in a sequence of strings from standard input, shuffles 
        /// them, and prints out the results.
        /// </summary>
        /// <example>Algs4.exe Knuth</example>
        /// <remarks>End of file is CTRL+Z and hit ENTER
        /// if you want to have shuffle of 1 2 3 4 5
        /// type into command line: 1 2 3 4 5        
        /// CTRL+Z + hit enter
        /// </remarks>
        /// <param name="args">no parameters expected</param>
        public static void Main(String[] args)
        {
            // read in the data
            String[] a = StdIn.ReadAllStrings();

            //shuffle the array
            Knuth.Shuffle(a);

            // print results.
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
                Debug.WriteLine(a[i]);
            }
        }
    }
}
