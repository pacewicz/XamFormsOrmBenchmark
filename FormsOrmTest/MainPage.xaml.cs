using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var stopwatch = Stopwatch.StartNew();
            var databasePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal), "db.sql");
            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }
            var db = new SQLiteAsyncConnection(databasePath);
//            await db.CreateTableAsync<Library>();
            await db.CreateTableAsync<Book>();
//            await db.CreateTableAsync<Person>();
            for (int i = 0; i < 1000; i++)
            {
                var book = new Book("Author", "Title", i+300);
                await db.InsertAsync(book);
            }

            await db.CloseAsync();
            stopwatch.Stop();
            Device.BeginInvokeOnMainThread(() => ResultLabel.Text = $"End: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
