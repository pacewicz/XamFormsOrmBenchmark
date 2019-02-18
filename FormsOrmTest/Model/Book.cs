using System.Collections.Generic;
using SQLite;

namespace FormsOrmTest.Model
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

//        public Book(string author, string title, int pagesCount, Library library) : this(author, title, pagesCount)
//        {
//            Library = library;
//        }
    }
}