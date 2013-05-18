/*************************************************************************
 *  Execution: StdLib In input.txt
 *
 *  Reads in data of various types from standard input and file.
 *
 *************************************************************************/

using System;
using System.IO;
using System.Linq;

namespace StdLib
{
    /// <summary>
    /// This class provides methods for reading strings and numbers from standard input and file.
    /// </summary>
    public class In
    {
        private readonly Scanner scanner;

        /// <summary>
        /// Create an input stream from standard input.
        /// </summary>
        public In()
        {
            scanner = new Scanner(Console.OpenStandardInput());
        }

        /// <summary>
        /// Create an input stream from a filename.
        /// </summary>
        public In(string path)
        {
            scanner = new Scanner(path);
        }

        /// <summary>
        /// Is the input empty (except possibly for whitespace)? Use this
        /// to know whether the next call to <c>ReadString</c>,
        /// <c>ReadDouble</c> etc will succeed.
        /// </summary>
        public bool IsEmpty()
        {
            return !scanner.HasNext();
        }

        /// <summary>
        /// Does the input have a next line? Use this to know whether the
        /// next call to <c>ReadLine()</c> will succeed. Functionally
        /// equivalent to <c>HasNextChar()</c>.
        /// </summary>
        public bool HasNextLine()
        {
            return scanner.HasNext();
        }

        /// <summary>
        /// Is the input empty (including whitespace)? Use this to know 
        /// whether the next call to <c>ReadChar()</c> will succeed. Functionally
        /// equivalent to <c>hasNextLine()</c>.
        /// </summary>
        public bool HasNextChar()
        {
            return scanner.HasNextChar();
        }

        /// <summary>
        /// Read and return the next line.
        /// </summary>
        public string ReadLine()
        {
            return scanner.ReadLine();
        }

        /// <summary>
        /// Read and return the next character.
        /// </summary>
        public char ReadChar()
        {
            return scanner.NextChar();
        }

        /// <summary>
        /// Read and return the remainder of the input as a string.
        /// </summary>
        public string ReadAll()
        {
            return scanner.ReadToEnd();
        }

        /// <summary>
        /// Read and return the next string.
        /// </summary>
        public String ReadString()
        {
            return scanner.NextString();
        }

        /// <summary>
        /// Read and return the next int.
        /// </summary>
        public int ReadInt()
        {
            return scanner.NextInt();
        }

        /// <summary>
        /// Read and return the next double.
        /// </summary>
        public double ReadDouble()
        {
            return scanner.NextDouble();
        }

        /// <summary>
        /// Read and return the next float.
        /// </summary>
        public float ReadFloat()
        {
            return scanner.NextFloat();
        }

        /// <summary>
        /// Read and return the next long.
        /// </summary>
        public long ReadLong()
        {
            return scanner.NextLong();
        }

        /// <summary>
        /// Read and return the next short.
        /// </summary>
        public short ReadShort()
        {
            return scanner.NextShort();
        }

        /// <summary>
        /// Read and return the next byte.
        /// </summary>
        public byte ReadByte()
        {
            return scanner.NextByte();
        }

        /// <summary>
        /// Read and return the next boolean, allowing case-insensitive
        /// "true" or "1" for true, and "false" or "0" for false.
        /// </summary>
        public bool ReadBoolean()
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
        public string[] ReadAllStrings()
        {
            return scanner.ReadToEnd().Trim().Split().Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }

        /// <summary>
        /// Read all ints until the end of input is reached, and return them.
        /// </summary>
        public int[] ReadAllInts()
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
        public double[] ReadAllDoubles()
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
        /// Close the input stream.
        /// </summary>
        public void Close()
        {
            scanner.Close();
        }

        /// <summary>
        /// Test client
        /// </summary>
        public static void Main(string[] args)
        {
            In input;

            // read one line at a time from file in current directory
            Console.WriteLine("ReadLine() from current directory");
            Console.WriteLine("---------------------------------------------------------------------------");

            try
            {
                input = new In(args[0]);

                while (!input.IsEmpty())
                {
                    String s = input.ReadLine();
                    Console.WriteLine(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine();

            // read one char at a time
            Console.WriteLine("ReadChar() from file");
            Console.WriteLine("---------------------------------------------------------------------------");

            try
            {
                input = new In(args[0]);

                while (!input.IsEmpty())
                {
                    char c = input.ReadChar();
                    Console.Write(c);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}