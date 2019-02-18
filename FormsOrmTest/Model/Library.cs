using System;
using SQLite;

namespace FormsOrmTest.Model
{
    [Table("Library")]
    public class Library
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        
        [Column("address")] public string Address { get; set; }

        [Column("name")] public string Name { get; set; }

        public Library()
        {
        }

        public Library(String address, String name)
        {
            Address = address;
            Name = name;
        }


//    public List<Person> Employees { get; set; }
//    public List<Book> getBooks() {
//        return getMany(Book.class,"library");
//    }
    }
}