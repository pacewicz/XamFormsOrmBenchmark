using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkXamarin;
using FormsOrmTest.Model;
using MobileAppPerformance;
using Netstandard2;
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

        void Handle_Clicked2(object sender, System.EventArgs e)
        {
            var thread = new Thread(RunThread);
            thread.Start();
        }

        private void RunThread()
        {
            BenchmarkManager.Current.Register(typeof(Tests).Assembly);
            var status = "wait";

            BenchmarkManager.Current.Log += text =>
            {
                status += "\n" + text;
                Device.BeginInvokeOnMainThread(() => ResultLabel.Text = status);

            };
            BenchmarkManager.Current.Start();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var tests = new Tests();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            tests.Test1000();
            stopWatch.Stop();
            var elapsed = stopWatch.Elapsed.TotalMilliseconds;
            Device.BeginInvokeOnMainThread(() => ResultLabel.Text = "1k elapsed " +elapsed);
        }

        async void Handle_Clicked10k(object sender, System.EventArgs e)
        {
            var tests = new Tests();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            tests.Test10K();
            stopWatch.Stop();
            var elapsed = stopWatch.Elapsed.TotalMilliseconds;
            Device.BeginInvokeOnMainThread(() => ResultLabel.Text = "10k elapsed " + elapsed     );
        }
    }
}
