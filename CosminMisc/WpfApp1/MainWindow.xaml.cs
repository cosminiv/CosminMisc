using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    delegate int MyOperation(int x, int y);

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            Stopwatch sw = Stopwatch.StartNew();

            string[] urls = {
                "https://news.google.com/?hl=en-US&gl=US&ceid=US:en",
                "https://news.yahoo.com",
                "https://www.theguardian.com/international",
                "https://www.hotnews.ro",
                "https://www.youtube.com/",
                "https://www.facebook.com",
                "https://www.linkedin.com",
                "https://edition.cnn.com/"
            };

            //Task.WhenAll()
            //new WebClient().DownloadStringTaskAsync();
            //await Task.Run(() => { });
            //TaskCompletionSource
            //Progress<ProgressReport>

            await DownloadAsyncSequentially(sw, urls);
        }

        private async Task DownloadAsyncSequentially(Stopwatch sw, string[] urls) {
            string crtUrl = "";

            foreach (string url in urls) {
                crtUrl = url;
                string status = "";
                TextBox1.Text += $"{url}";

                try {
                    string html = await Task.Run(() => DownloadUrl(url));
                    status = $"{Math.Round(html.Length / 1000.0)}KB";
                }
                catch (Exception) {
                    status = "error";
                }

                TextBox1.Text += $": {status}\n";
            }

            TextBox1.Text += $"{sw.Elapsed.Seconds}s\n\n";
        }

        string DownloadUrl(string url) {
            using (var client = new WebClient())
                return client.DownloadString(url);
        }

        private void Button2_Click(object sender, RoutedEventArgs e) {
            TextBox2.Text += "Hello!  ";
        }

        private void BtnClear1_Click(object sender, RoutedEventArgs e) {
            TextBox1.Text = "";
        }
    }
}
