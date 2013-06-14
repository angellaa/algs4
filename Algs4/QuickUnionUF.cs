/****************************************************************************
 *  Execution:  algs4 QuickUnionUF < input.txt
 *
 *  Data files:   http://algs4.cs.princeton.edu/15uf/tinyUF.txt
 *                http://algs4.cs.princeton.edu/15uf/mediumUF.txt
 *                http://algs4.cs.princeton.edu/15uf/largeUF.txt
 *
 *  Quick-union algorithm.
 *
 *  > algs4 QuickUnionUF < tinyUF.txt
 *  4 3
 *  3 8
 *  6 5
 *  9 4
 *  2 1
 *  5 0
 *  7 2
 *  6 1
 *  2 components
 * 
 ****************************************************************************/

using System;
using StdLib;

namespace Algs4
{
    public class QuickUnionUF
    {
        private readonly int[] id; // id[i] = parent of i

        /// <summary>
        /// Instantiate N isolated components 0 through N-1
        /// </summary>
        public QuickUnionUF(int N)
        {
            id = new int[N];
            Count = N;

            for (int i = 0; i < N; i++)
            {
                id[i] = i;
            }
        }

        /// <summary>
        /// Return the number of connected components
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Return root of component corresponding to element p
        /// </summary>
        public int Find(int p)
        {
            while (p != id[p])
                p = id[p];

            return p;
        }

        /// <summary>
        /// Are elements p and q in the same component?
        /// </summary>
        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        /// <summary>
        /// Merge components containing p and q
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        public void Union(int p, int q)
        {
            int i = Find(p);
            int j = Find(q);

            if (i == j) return;
            id[i] = j;

            Count--;
        }
    }

    public class QuickUnionUFExample
    {
        public static void Main(string[] args)
        {
            int N = StdIn.ReadInt();

            QuickUnionUF uf = new QuickUnionUF(N);

            // read in a sequence of pairs of integers (each in the range 0 to N-1),
            // calling find() for each pair: If the members of the pair are not already
            // call union() and print the pair.
            while (!StdIn.IsEmpty())
            {
                int p = StdIn.ReadInt();
                int q = StdIn.ReadInt();

                if (uf.Connected(p, q)) continue;
                uf.Union(p, q);

                Console.WriteLine(p + " " + q);
            }

            Console.WriteLine(uf.Count + " components");
        }
    }
}