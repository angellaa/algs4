using System;

namespace Algs4
{
    class Program
    {
        /// <summary>
        /// Main method that allows to run the Main method of a specific algorithm.
        /// </summary>
        /// <param name="args">
        /// The first element must be the name of the class that implement a particular algorithm. 
        /// The following arguments will be passed directly to the algorithm Main method.
        /// </param>
        public static void Main(string[] args)
        {
            // Show the usage syntax when no arguments are provided
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: algs4 className [args...] < [input.txt]");
                return;
            }

            // Search for the requested algorithm
            var algorithmName = args[0];
            var classType = Type.GetType("Algs4." + algorithmName);

            if (classType == null)
            {
                Console.WriteLine("No class with the name {0} found.", algorithmName);
                return;
            }

            // Create the arguments for the algorithm Main method
            string[] algorithmArgs = new string[args.Length - 1];
            Array.Copy(args, 1, algorithmArgs, 0, args.Length - 1);

            // Invoke the Main method of the requested algorithm
            classType.GetMethod("Main").Invoke(null, new object[] { algorithmArgs });
        }
    }
}
