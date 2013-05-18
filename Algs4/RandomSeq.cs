/*************************************************************************
 *
 *  Prints N numbers between lo and hi.
 *
 *  > algs4 RandomSeq 5 100.0 200.0
 *  123.43
 *  153.13
 *  144.38
 *  155.18
 *  104.02
 *
 *************************************************************************/

using System;
using StdLib;

namespace Algs4
{
    public class RandomSeq
    {
        public static void Main(string[] args)
        {
            // command-line arguments
            int N = int.Parse(args[0]);

            if (args.Length == 1)
            {
                // generate and print N numbers between 0.0 and 1.0
                for (int i = 0; i < N; i++)
                {
                    double x = StdRandom.Uniform();
                    Console.WriteLine(x);
                }
            }
            else if (args.Length == 3)
            {
                double lo = Double.Parse(args[1]);
                double hi = Double.Parse(args[2]);

                // generate and print N numbers between lo and hi
                for (int i = 0; i < N; i++)
                {
                    double x = StdRandom.Uniform(lo, hi);
                    Console.WriteLine("{0:N2}", x);
                }
            }
            else
            {
                throw new ArgumentException("Invalid number of arguments");
            }
        }
    }
}