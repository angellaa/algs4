/****************************************************************************
 *  Execution:  algs4 WeightedQuickUnionUF < input.txt
 *
 *  Data files:   http://algs4.cs.princeton.edu/15uf/tinyUF.txt
 *                http://algs4.cs.princeton.edu/15uf/mediumUF.txt
 *                http://algs4.cs.princeton.edu/15uf/largeUF.txt
 *
 * Weighted quick-union (without path compression).
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
    public class WeightedQuickUnionUF
    {
        private readonly int[] id; // id[i] = parent of i
        private readonly int[] sz; // sz[i] = number of objects in subtree rooted at i

        /// <summary>
        /// Create an empty union find data structure with N isolated sets.
        /// </summary>
        public WeightedQuickUnionUF(int N)
        {
            Count = N;
            id = new int[N];
            sz = new int[N];
            
            for (int i = 0; i < N; i++)
            {
                id[i] = i;
                sz[i] = 1;
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
            while (p != id[p])
                p = id[p];

            return p;
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

            // make smaller root point to larger one
            if (sz[i] < sz[j])
            {
                id[i] = j;
                sz[j] += sz[i];
            }
            else
            {
                id[j] = i;
                sz[i] += sz[j];
            }

            Count--;
        }
        
        public static void Main(string[] args)
        {
            int N = StdIn.ReadInt();

            WeightedQuickUnionUF uf = new WeightedQuickUnionUF(N);

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