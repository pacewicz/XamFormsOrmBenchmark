using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;

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

[Table("Book")]
    public class Book
    {
        [Column("author")] public string Author { get; set; }

        [Column("title")] public string Title { get; set; }

        [Column("pagesCount")] public int PagesCount { get; set; }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

//        [Column("library")] public Library Library { get; set; }

        public Book()
        {

        }

        public Book(string author, string title,
            int pagesCount)
        {
            Author = author;
            Title = title;
            PagesCount = pagesCount;
        }
    }

    public class BenchmarkSqlite
    {
        private RandomObjectsGenerator _randomObjectsGenerator;

        public BenchmarkSqlite()
        {
            _randomObjectsGenerator = new RandomObjectsGenerator();
        }

        [Benchmark]
        public void InsertSync10()
        {
            InsertSync(10);
        }
        
        [Benchmark]
        public void InsertSync100()
        {
            InsertSync(100);
        }

        [Benchmark]
        public void InsertGenerated1000()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                "db.db3");
            var sdb = new SQLiteConnection(databasePath);
            sdb.CreateTable<Book>();
            foreach (var book in _randomObjectsGenerator.generateBooks(1000))
            {
                sdb.Insert(book);
            }
          sdb.Close();
          File.Delete(databasePath);
        }

        public void InsertSync(int passes)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                "db.db3");
            var sdb = new SQLiteConnection(databasePath);
            sdb.CreateTable<Book>();
            for (int i = 0; i < passes; i++)
            {
                var book = new Book("Author", "Title", i + 300);
                sdb.Insert(book);
            }

            sdb.Close();
            File.Delete(databasePath);
        }
    }
}
