using System;
using System.IO;
using SQLite;

namespace BenchmarkXamarin.Benchmarks
{
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
        public BenchmarkSqlite()
        {
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
