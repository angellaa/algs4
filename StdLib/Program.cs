using System;

namespace StdLib
{
    class Program
    {
        /// <summary>
        /// Main method that allows to run the Main method of a specific standard class.
        /// </summary>
        /// <param name="args">
        /// The first element must be the name of the class. 
        /// The following arguments will be passed directly to the Main method of the requested class.
        /// </param>
        public static void Main(string[] args)
        {
            // Show the usage syntax when no arguments are provided
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: StdLib className [args...] < [input.txt]");
                return;
            }

            // Search for the requested class
            var className = args[0];
            var classType = Type.GetType("StdLib." + className);

            if (classType == null)
            {
                Console.WriteLine("No class with the name {0} found.", className);
                return;
            }

            // Create the arguments for the class Main method
            string[] classArgs = new string[args.Length - 1];
            Array.Copy(args, 1, classArgs, 0, args.Length - 1);

            // Invoke the Main method of the requested class
            classType.GetMethod("Main").Invoke(null, new object[] { classArgs });
        }
    }
}
