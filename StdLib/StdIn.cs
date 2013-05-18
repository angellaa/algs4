/*************************************************************************
 *  Compilation:  StdLib StdIn
 *  Execution:    StdIn   (interactive test of basic functionality)
 *
 *  Reads in data of various types from standard input.
 *
 *************************************************************************/

using System;
using System.IO;

namespace StdLib
{
    /// <summary>
    /// This class provides methods for reading strings and numbers from standard input.  
    /// </summary>
    public class StdIn
    {
        private static readonly Scanner scanner = new Scanner(Console.OpenStandardInput());

        /// <summary>
        /// Is the input empty (except possibly for whitespace)? Use this
        /// to know whether the next call to <c>ReadString()</c>, 
        /// <c>ReadDouble()</c>, etc will succeed.
        /// </summary>
        public static bool IsEmpty()
        {
            return !scanner.HasNext();
        }

        /// <summary>
        /// Does the input have a next line? Use this to know whether the
        /// next call to <c>readLine()</c> will succeed. Functionally
        /// equivalent to <c>HasNextChar()</c>.
        /// </summary>
        public static bool HasNextLine()
        {
            return scanner.HasNext();
        }

        /// <summary>
        /// Is the input empty (including whitespace)? Use this to know 
        /// whether the next call to <c>readChar()</c> will succeed. Functionally
        /// equivalent to <c>HasNextLine()</c>.
        /// </summary>
        public static bool HasNextChar()
        {
            return scanner.HasNextChar();
        }

        /// <summary>
        /// Read and return the next line.
        /// </summary>
        public static string ReadLine()
        {
            return scanner.ReadLine();
        }

        /// <summary>
        /// Read and return the next character.
        /// </summary>
        public static char ReadChar()
        {
            return scanner.NextChar();
        }

        /// <summary>
        /// Read and return the remainder of the input as a string.
        /// </summary>
        public static string ReadAll()
        {
            return scanner.ReadToEnd();
        }

        /// <summary>
        /// Read and return the next string.
        /// </summary>
        public static String ReadString()
        {
            return scanner.NextString();
        }

        /// <summary>
        /// Read and return the next int.
        /// </summary>
        public static int ReadInt()
        {
            return scanner.NextInt();
        }

        /// <summary>
        /// Read and return the next double.
        /// </summary>
        public static double ReadDouble()
        {
            return scanner.NextDouble();
        }

        /// <summary>
        /// Read and return the next float.
        /// </summary>
        public static float ReadFloat()
        {
            return scanner.NextFloat();
        }

        /// <summary>
        /// Read and return the next long.
        /// </summary>
        public static long ReadLong()
        {
            return scanner.NextLong();
        }

        /// <summary>
        /// Read and return the next short.
        /// </summary>
        public static short ReadShort()
        {
            return scanner.NextShort();
        }

        /// <summary>
        /// Read and return the next byte.
        /// </summary>
        public static byte ReadByte()
        {
            return scanner.NextByte();
        }

        /// <summary>
        /// Read and return the next boolean, allowing case-insensitive
        /// "true" or "1" for true, and "false" or "0" for false.
        /// </summary>
        public static bool ReadBoolean()
        {
            string s = ReadString();
            if (s.Equals("true", StringComparison.CurrentCultureIgnoreCase)) return true;
            if (s.Equals("false", StringComparison.CurrentCultureIgnoreCase)) return false;
            if (s.Equals("1")) return true;
            if (s.Equals("0")) return false;

            throw new IOException();
        }

        /// <summary>
        /// Read all strings until the end of input is reached, and return them.
        /// </summary>
        public static string[] ReadAllStrings()
        {
            return scanner.ReadToEnd().Trim().Split();
        }

        /// <summary>
        /// Read all ints until the end of input is reached, and return them.
        /// </summary>
        public static int[] ReadAllInts()
        {
            string[] fields = ReadAllStrings();

            int[] vals = new int[fields.Length];

            for (int i = 0; i < fields.Length; i++)
            {
                vals[i] = int.Parse(fields[i]);
            }

            return vals;
        }

        /// <summary>
        /// Read all doubles until the end of input is reached, and return them.
        /// </summary>
        public static double[] readAllDoubles()
        {
            string[] fields = ReadAllStrings();

            double[] vals = new double[fields.Length];

            for (int i = 0; i < fields.Length; i++)
            {
                vals[i] = Double.Parse(fields[i]);
            }

            return vals;
        }

        /// <summary>
        /// Interactive test of basic functionality.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("Type a string: ");
            String s = StdIn.ReadString();
            Console.WriteLine("Your string was: " + s);
            Console.WriteLine();

            Console.WriteLine("Type an int: ");
            int a = StdIn.ReadInt();
            Console.WriteLine("Your int was: " + a);
            Console.WriteLine();

            Console.WriteLine("Type a boolean: ");
            bool b = StdIn.ReadBoolean();
            Console.WriteLine("Your boolean was: " + b);
            Console.WriteLine();

            Console.WriteLine("Type a double: ");
            double c = StdIn.ReadDouble();
            Console.WriteLine("Your double was: " + c);
            Console.WriteLine();

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}