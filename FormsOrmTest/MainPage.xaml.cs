using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsOrmTest.Model;
using SQLite;
using Xamarin.Forms;

namespace FormsOrmTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            const string databasePath = "db.sql";
            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }
            var db = new SQLiteAsyncConnection(databasePath);
            db.CreateTableAsync<Book>();
            for (int i = 0; i < 1000; i++)
            {
                var book = new Book("Author", "Title", i+300, );
            }
        }
    }
}
