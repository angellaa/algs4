/*************************************************************************
 * 
 *  A library of static methods to generate pseudo-random numbers from
 *  different distributions (bernoulli, uniform, gaussian, discrete,
 *  and exponential). Also includes a method for shuffling an array.
 *
 *  > StdLib StdRandom 5
 *  seed = 1755779618
 *  25 22.31074 True  9.34465 0
 *  10 51.05381 False 9.03719 1
 *  29 10.94104 True  8.98654 0
 *  93 67.30122 False 8.76335 3
 *  56 10.06169 False 9.20318 0
 * 
 *  > StdLib StdRandom 5 123456789
 *  seed = 123456789
 *  50 25.82536 False 8.96422 1
 *  08 90.55304 False 9.27877 3
 *  95 52.06136 False 9.02516 0
 *  65 79.53091 False 9.05470 1
 *  38 75.64123 False 8.87307 3
 *
 *  Remark
 *  ------
 *    - Relies on randomness of Next() method in Random
 *      to generate pseudorandom numbers in [0, 1).
 *
 *    - This library allows you to set and get the pseudorandom number seed.
 *
 *************************************************************************/

using System;

namespace StdLib
{

    /// <summary>
    /// Standard random. This class provides methods for generating random number from various distributions.
    /// For additional documentation, see <see cref="http://introcs.cs.princeton.edu/22library">Section 2.2</see> of
    /// Introduction to Programming in Java: An Interdisciplinary Approach by Robert Sedgewick and Kevin Wayne.
    /// </summary>
    public static class StdRandom
    {
        private static Random random; // pseudo-random number generator

        public static int seed;

        /// <summary>
        /// Gets and sets a pseudo-random number generator seed
        /// </summary>
        public static int Seed
        {
            get { return seed; }
            set
            {
                seed = value;
                random = new Random(seed);
            }
        }

        static StdRandom()
        {
            seed = (int) DateTime.Now.Ticks;
            random = new Random();
        }

        /// <summary>
        /// Return real number uniformly in [0, 1).
        /// </summary>
        public static double Uniform()
        {
            return random.NextDouble();
        }

        /// <summary>
        /// Return an integer uniformly between 0 (inclusive) and N (exclusive).
        /// </summary>
        public static int Uniform(int N)
        {
            return random.Next(N);
        }

        /// <summary>
        /// Return real number uniformly in [0, 1).
        /// </summary>
        public static double Random()
        {
            return Uniform();
        }

        /// <summary>
        /// Return int uniformly in [a, b).
        /// </summary>
        public static int Uniform(int a, int b)
        {
            return a + Uniform(b - a);
        }

        /// <summary>
        /// Return real number uniformly in [a, b).
        /// </summary>
        public static double Uniform(double a, double b)
        {
            return a + Uniform() * (b - a);
        }

        /// <summary>
        /// Return a boolean, which is true with probability p, and false otherwise.
        /// </summary>
        public static bool Bernoulli(double p)
        {
            return Uniform() < p;
        }

        /// <summary>
        /// Return a boolean, which is true with probability .5, and false otherwise.
        /// </summary>
        public static bool Bernoulli()
        {
            return Bernoulli(0.5);
        }

        /// <summary>
        /// Return a real number with a standard Gaussian distribution.
        /// </summary>
        public static double Gaussian()
        {
            // use the polar form of the Box-Muller transform
            double r, x, y;

            do
            {
                x = Uniform(-1.0, 1.0);
                y = Uniform(-1.0, 1.0);
                r = x * x + y * y;
            }
            while (r >= 1 || r == 0);

            return x * Math.Sqrt(-2 * Math.Log(r) / r);

            // Remark:  y * Math.Sqrt(-2 * Math.Log(r) / r)
            // is an independent random gaussian
        }

        /// <summary>
        /// Return a real number from a gaussian distribution with given mean and stddev
        /// </summary>
        public static double Gaussian(double mean, double stddev)
        {
            return mean + stddev * Gaussian();
        }

        /// <summary>
        /// Return an integer with a geometric distribution with mean 1/p.
        /// </summary>
        public static int Geometric(double p)
        {
            // using algorithm given by Knuth
            return (int) Math.Ceiling(Math.Log(Uniform()) / Math.Log(1.0 - p));
        }

        /// <summary>
        /// Return an integer with a Poisson distribution with mean lambda.
        /// </summary>
        public static int Poisson(double lambda)
        {
            // using algorithm given by Knuth
            // see http://en.wikipedia.org/wiki/Poisson_distribution
            int k = 0;
            double p = 1.0;
            double L = Math.Exp(-lambda);
            do
            {
                k++;
                p *= Uniform();
            }
            while (p >= L);
            return k - 1;
        }

        /// <summary>
        /// Return a real number with a Pareto distribution with parameter alpha.
        /// </summary>
        public static double Pareto(double alpha)
        {
            return Math.Pow(1 - Uniform(), -1.0 / alpha) - 1.0;
        }

        /// <summary>
        /// Return a real number with a Cauchy distribution.
        /// </summary>
        public static double Cauchy()
        {
            return Math.Tan(Math.PI * (Uniform() - 0.5));
        }

        /// <summary>
        /// Return a number from a discrete distribution: i with probability a[i].
        /// Precondition: array entries are nonnegative and their sum (very nearly) equals 1.0.
        /// </summary>
        public static int Discrete(double[] a)
        {
            const double EPSILON = 1E-14;
            double sum = 0.0;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] < 0.0) throw new ArgumentException("array entry " + i + " is negative: " + a[i]);
                sum = sum + a[i];
            }

            if (sum > 1.0 + EPSILON || sum < 1.0 - EPSILON)
            {
                throw new ArgumentException("sum of array entries not equal to one: " + sum);
            }

            // the for loop may not return a value when both r is (nearly) 1.0 and when the
            // cumulative sum is less than 1.0 (as a result of floating-point roundoff error)
            while (true)
            {
                double r = Uniform();
                sum = 0.0;
                for (int i = 0; i < a.Length; i++)
                {
                    sum = sum + a[i];
                    if (sum > r) return i;
                }
            }
        }

        /// <summary>
        /// Return a real number from an exponential distribution with rate lambda.
        /// </summary>
        public static double Exp(double lambda)
        {
            return -Math.Log(1 - Uniform()) / lambda;
        }

        /// <summary>
        /// Rearrange the elements of an array in random order.
        /// </summary>
        public static void Shuffle<T>(T[] a)
        {
            int N = a.Length;

            for (int i = 0; i < N; i++)
            {
                int r = i + Uniform(N - i); // between i and N-1

                T temp = a[i];
                a[i] = a[r];
                a[r] = temp;
            }
        }

        /// <summary>
        /// Rearrange the elements of the subarray a[lo..hi] in random order.
        /// </summary>
        public static void Shuffle<T>(T[] a, int lo, int hi)
        {
            if (lo < 0 || lo > hi || hi >= a.Length)
                throw new ArgumentException("Illegal subarray range");

            for (int i = lo; i <= hi; i++)
            {
                int r = i + Uniform(hi - i + 1); // between i and hi

                T temp = a[i];
                a[i] = a[r];
                a[r] = temp;
            }
        }

        public static void Main(String[] args)
        {
            int N = int.Parse(args[0]);

            if (args.Length == 2)
            {
                StdRandom.Seed = int.Parse(args[1]);
            }

            double[] t = {.5, .3, .1, .1};

            Console.WriteLine("seed = " + StdRandom.Seed);

            for (int i = 0; i < N; i++)
            {
                Console.Write("{0:00} ", Uniform(100));
                Console.Write("{0:########.00000} ", Uniform(10.0, 99.0));
                Console.Write("{0} ", Bernoulli(.5).ToString().PadRight(5));
                Console.Write("{0:#######.00000} ", Gaussian(9.0, .2));
                Console.Write("{0}", Discrete(t));
                Console.WriteLine();
            }
        }
    }
}

