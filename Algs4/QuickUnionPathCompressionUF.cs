/****************************************************************************
 *  Execution:  algs4 QuickUnionPathCompressionUF < input.txt
 *
 *  Data files:   http://algs4.cs.princeton.edu/15uf/tinyUF.txt
 *                http://algs4.cs.princeton.edu/15uf/mediumUF.txt
 *                http://algs4.cs.princeton.edu/15uf/largeUF.txt
 *
 *  Quick-union with path compression.
 *
 *  > algs4 WeightedQuickUnionUF < tinyUF.txt
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
    public class QuickUnionPathCompressionUF
    {
        private readonly int[] id; // id[i] = parent of i

        /// <summary>
        /// Create an empty union find data structure with N isolated sets.
        /// </summary>
        public QuickUnionPathCompressionUF(int N)
        {
            Count = N;
            id = new int[N];
            
            for (int i = 0; i < N; i++)
            {
                id[i] = i;
            }
        }

        /// <summary>
        /// Return the number of disjoint sets.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Return component identifier for component containing p
        /// </summary>
        public int Find(int p)
        {
            int root = p;

            while (root != id[root])
                root = id[root];
            
            while (p != root)
            {
                int newp = id[p];
                id[p] = root;
                p = newp;
            }

            return root;
        }

        /// <summary>
        /// Are objects p and q in the same set?
        /// </summary>
        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        /// <summary>
        /// Replace sets containing p and q with their union.
        /// </summary>
        public void Union(int p, int q)
        {
            int i = Find(p);
            int j = Find(q);
            
            if (i == j) return;
            id[i] = j;
            
            Count--;
        }
    }

    public class QuickUnionPathCompressionUFExample
    {
        public static void Main(string[] args)
        {
            int N = StdIn.ReadInt();

            QuickUnionPathCompressionUF uf = new QuickUnionPathCompressionUF(N);

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