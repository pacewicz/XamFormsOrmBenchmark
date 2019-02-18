using System;
using SQLite;

namespace FormsOrmTest.Model
{
    [Table("Person")]
    public class Person
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [Column("firstName")] public string FirstName { get; set; }

        [Column("secondName")] public string SecondName { get; set; }

        [Column("birthdayDate")] public DateTime BirthdayDate { get; set; }

        [Column("gender")] public string Gender { get; set; }

        [Column("phone")] public long Phone { get; set; }

        [Column("library")] public Library Library { get; set; }

        public Person()
        {
        }

        public Person(String firstName, String secondName, DateTime birthdayDate, String gender, long phone)
        {
            FirstName = firstName;
            SecondName = secondName;
            BirthdayDate = birthdayDate;
            Gender = gender;
            Phone = phone;
        }

        public Person(String firstName, String secondName, DateTime birthdayDate, String gender, long phone,
            Library library)
            : this(firstName, secondName, birthdayDate, gender, phone)
        {
            Library = library;
        }
    }
}