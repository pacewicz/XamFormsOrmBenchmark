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
            const int passes = 5000;

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
            for (int i = 0; i < passes; i++)
            {
                var book = new Book("Author", "Title", i+300);
                await db.InsertAsync(book);
            }

            await db.CloseAsync();
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            Device.BeginInvokeOnMainThread(() => ResultLabel.Text = $"InsertAsync*{passes}: {elapsedMilliseconds} ms");

          
            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }
            stopwatch = Stopwatch.StartNew();
            var sdb = new SQLiteConnection(databasePath);
//            await db.CreateTableAsync<Library>();
            sdb.CreateTable<Book>();
//            await db.CreateTableAsync<Person>();
            for (int i = 0; i < passes; i++)
            {
                var book = new Book("Author", "Title", i+300);
                sdb.Insert(book);
            }

            sdb.Close();
            stopwatch.Stop();
            var elapsedMilliseconds2 = stopwatch.ElapsedMilliseconds;
            Device.BeginInvokeOnMainThread(() => ResultLabel.Text += $"\nInsertSync*{passes}: {elapsedMilliseconds2} ms");

            stopwatch = Stopwatch.StartNew();
            sdb = new SQLiteConnection(databasePath);
            var books = sdb.Get<Book>(b => true);

            sdb.Close();
            stopwatch.Stop();
            var elapsedMilliseconds3 = stopwatch.ElapsedMilliseconds;
            Device.BeginInvokeOnMainThread(() => ResultLabel.Text += $"\nGetSync*{passes}: {elapsedMilliseconds3} ms");

        }
    }
}
