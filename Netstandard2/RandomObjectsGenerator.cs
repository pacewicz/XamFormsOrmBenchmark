using System;
using System.Collections.Generic;
using System.Text;

namespace BenchmarkXamarin.Benchmarks
{
    public class RandomObjectsGenerator
    {
        protected static Random random = new Random();
        protected static RandomString randomString = new RandomString(40);

        public Book nextBook()
        {
            return new Book(randomString.nextString(), randomString.nextString(),
                Math.Abs(random.Next()) + 1);
        }

        public List<Book> generateBooks(int quantity)
        {
            List<Book> books = new List<Book>(quantity);
            for (int i = 0; i < quantity; i++)
            {
                books.Add(nextBook());
            }
            return books;
        }

        public String nextString()
        {
            return randomString.nextString();
        }

        public class RandomString
        {

            private static char[] symbols;

            static RandomString()
            {
                StringBuilder tmp = new StringBuilder();

                for (char ch = '0'; ch <= '9'; ++ch)
                    tmp.Append(ch);
                for (char ch = 'a'; ch <= 'z'; ++ch)
                    tmp.Append(ch);
                for (char ch = 'A'; ch <= 'Z'; ++ch)
                    tmp.Append(ch);
                tmp.Append(" ");
                symbols = tmp.ToString().ToCharArray();
            }

            private Random random = new Random();

            private int maxStringLength;

            public RandomString(int maxStringLength)
            {
                this.maxStringLength = maxStringLength;
            }

            public String nextString(int length)
            {
                if (length < 1)
                {
                    throw new ArgumentException("String length must be greater than 0");
                }
                char[] buf = new char[length];
                for (int idx = 0; idx < buf.Length; ++idx)
                    buf[idx] = symbols[random.Next(symbols.Length)];
                return new String(buf);
            }

            public String nextString()
            {
                return nextString(Math.Abs(random.Next()) % maxStringLength + 1);
            }
        }
    }
}