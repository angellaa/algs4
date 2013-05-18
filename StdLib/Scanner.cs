using System.IO;
using System.Text;

namespace StdLib
{
    /// <summary>
    /// Simplified version of the Java Scanner class in C#.
    /// </summary>
    public class Scanner : StreamReader
    {
        private string currentWord;
        private string currentWordWithWhiteSpaces;

        public Scanner(Stream stream) : base(stream)
        {
        }

        public Scanner(string path) : base(path)
        {
        }

        private void ReadNextWord()
        {
            var sb = new StringBuilder();
            var prewhitespaces = new StringBuilder();
            var postwhitespaces = new StringBuilder();

            while (Peek() >= 0 && char.IsWhiteSpace((char)Peek()))
            {
                prewhitespaces.Append((char)Read());
            }

            do
            {
                if (Peek() >= 0 && char.IsWhiteSpace((char) Peek()))
                {
                    postwhitespaces.Append((char)Read());
                    break;                    
                }

                int next = Read();

                if (next < 0)
                    break;
                
                char nextChar = (char)next;

                sb.Append(nextChar);
            } 
            while (true);
                        
            currentWord = sb.Length > 0 ? sb.ToString() : null;

            if (prewhitespaces.Length > 0 || postwhitespaces.Length > 0)
            {
                currentWordWithWhiteSpaces = prewhitespaces.ToString() + sb + postwhitespaces;                
            }
            else
            {
                currentWordWithWhiteSpaces = null;
            }
        }

        public bool HasNextInt()
        {
            if (!HasNext())
            {
                return false;
            }

            int dummy;
            return int.TryParse(currentWord, out dummy);
        }

        public int NextInt()
        {
            try
            {
                HasNext();
                return int.Parse(currentWord);
            }
            finally
            {
                Reset();
            }
        }

        public bool HasNextDouble()
        {
            if (!HasNext())
            {
                return false;
            }

            double dummy;
            return double.TryParse(currentWord, out dummy);
        }

        public double NextDouble()
        {
            try
            {
                HasNext();
                return double.Parse(currentWord);
            }
            finally
            {
                Reset();
            }
        }

        public bool HasNextFloat()
        {
            if (!HasNext())
            {
                return false;
            }

            float dummy;
            return float.TryParse(currentWord, out dummy);
        }

        public float NextFloat()
        {
            try
            {
                HasNext();
                return float.Parse(currentWord);
            }
            finally
            {
                Reset();
            }
        }

        public bool HasNextLong()
        {
            if (!HasNext())
            {
                return false;
            }

            long dummy;
            return long.TryParse(currentWord, out dummy);
        }

        public long NextLong()
        {
            try
            {
                HasNext();
                return long.Parse(currentWord);
            }
            finally
            {
                Reset();
            }
        }

        public bool HasNextShort()
        {
            if (!HasNext())
            {
                return false;
            }

            short dummy;
            return short.TryParse(currentWord, out dummy);
        }

        public short NextShort()
        {
            try
            {
                HasNext();
                return short.Parse(currentWord);
            }
            finally
            {
                Reset();
            }
        }

        public bool HasNextByte()
        {
            if (!HasNext())
            {
                return false;
            }

            byte dummy;
            return byte.TryParse(currentWord, out dummy);
        }

        public byte NextByte()
        {
            try
            {
                HasNext();
                return byte.Parse(currentWord);
            }
            finally
            {
                Reset();
            }
        }

        public bool HasNextChar()
        {
            if (!HasNext())
            {
                return false;
            }

            return !string.IsNullOrEmpty(currentWordWithWhiteSpaces);
        }

        public char NextChar()
        {
            try
            {
                HasNext();

                char c = currentWordWithWhiteSpaces[0];
                currentWordWithWhiteSpaces = currentWordWithWhiteSpaces.Substring(1);
                return c;
            }
            finally
            {
                if (string.IsNullOrEmpty(currentWordWithWhiteSpaces))
                {
                    ReadNextWord();                    
                }
            }
        }

        public bool HasNext()
        {
            if (currentWord == null)
            {
                ReadNextWord();

                if (currentWord == null)
                {
                    return false;
                }
            }

            return true;
        }

        public string NextString()
        {
            try
            {
                HasNext();
                return currentWord;
            }
            finally
            {
                Reset();
            }
        }

        public override string ReadLine()
        {
            try
            {
                HasNext();
                return currentWordWithWhiteSpaces + base.ReadLine();
            }
            finally
            {
                Reset();
            }
        }

        public override string ReadToEnd()
        {
            try
            {
                HasNext();
                return currentWordWithWhiteSpaces + base.ReadToEnd();
            }
            finally
            {
                Reset();
            }
        }

        private void Reset()
        {
            currentWord = null;
            currentWordWithWhiteSpaces = null;
        }
    }
}