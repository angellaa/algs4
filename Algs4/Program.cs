using System;
using System.Collections.Generic;
using System.Reflection;

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
            var classType = GetClassType(algorithmName);

            if (classType == null)
            {
                Console.WriteLine("No class with the name {0} found.", algorithmName);
                return;
            }

            // Create the arguments for the algorithm Main method
            string[] algorithmArgs = new string[args.Length - 1];
            Array.Copy(args, 1, algorithmArgs, 0, args.Length - 1);

            // Invoke the Main method of the requested algorithm
            MethodInfo mainMethod = classType.GetMethod("Main");

            if (mainMethod == null)
            {
                Console.WriteLine("No Main method has been found in the class.");
                return;
            }

            try
            {
                mainMethod.Invoke(null, new object[] {algorithmArgs});
            }
            catch (Exception ex)
            {
                Console.WriteLine("The main method thrown an exception:\n" + ex);                
            }
        }

        private static Type GetClassType(string algorithmName)
        {
            if (AlgorithmToType.ContainsKey(algorithmName))
            {
                return AlgorithmToType[algorithmName];
            }

            return Type.GetType("Algs4." + algorithmName);
        }

        /// <summary>
        /// A dictionary that associate an algorithm class name to the associate type.
        /// The type will be used to call the static Main method so it should be possible to use open types.
        /// However, a closed generic type is required to avoid the following runtime exception in Reflection:
        /// "Late bound operations cannot be performed on types or methods with generic parameters".
        /// </summary>
        private static readonly Dictionary<string, Type> AlgorithmToType = new Dictionary<string, Type>
        {
            { "IndexMinPQ", typeof (IndexMinPQ<int>) }
        };
    }
}
