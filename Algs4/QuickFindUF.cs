/****************************************************************************
 * Execution:  algs QuickFindUF < input.txt
 *
 * Data files:   http://algs4.cs.princeton.edu/15uf/tinyUF.txt
 *               http://algs4.cs.princeton.edu/15uf/mediumUF.txt
 *               http://algs4.cs.princeton.edu/15uf/largeUF.txt
 *
 * Quick-find algorithm.
 *
 *  > algs4 QuickFindUF < tinyUF.txt
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
 * ****************************************************************************/

using System;
using StdLib;

namespace Algs4
{
    public class QuickFindUF
    {
        private readonly int[] id;

        /// <summary>
        /// Instantiate N isolated components 0 through N-1
        /// </summary>
        public QuickFindUF(int N)
        {
            Count = N;
            id = new int[N];

            for (int i = 0; i < N; i++)
                id[i] = i;
        }

        /// <summary>
        /// Return the number of connected components
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Return component identifier for component containing p
        /// </summary>
        public int Find(int p)
        {
            return id[p];
        }

        /// <summary>
        /// Are elements p and q in the same component?
        /// </summary>
        public bool Connected(int p, int q)
        {
            return id[p] == id[q];
        }

        /// <summary>
        /// Merge components containing p and q
        /// </summary>
        public void Union(int p, int q)
        {
            if (Connected(p, q)) return;

            int pid = id[p];

            for (int i = 0; i < id.Length; i++)
                if (id[i] == pid)
                    id[i] = id[q];

            Count--;
        }
    }

    public class QuickFindUFExample
    {
        public static void Main(string[] args)
        {
            int N = StdIn.ReadInt();

            QuickFindUF uf = new QuickFindUF(N);

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

            Console.Write(uf.Count + " components");
        }
    }
}