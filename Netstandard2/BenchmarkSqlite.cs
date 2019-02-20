using System;
using System.IO;
using SQLite;

namespace BenchmarkXamarin.Benchmarks
{
    public class BenchmarkSqlite
    {
        private RandomObjectsGenerator _randomObjectsGenerator;

        public BenchmarkSqlite()
        {
            _randomObjectsGenerator = new RandomObjectsGenerator();
        }

        //[Benchmark]
        public void InsertSync10()
        {
            InsertSync(10);
        }
        
        //[Benchmark]
        public void InsertSync100()
        {
            InsertSync(100);
        }

        //[Benchmark]
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
