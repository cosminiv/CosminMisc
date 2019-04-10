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
                "https://news.google.com",
                "https://news.yahoo.com",
                "https://www.theguardian.com",
                "https://www.hotnews.ro",
                "https://www.youtube.com",
                "https://www.facebook.com",
                "https://www.linkedin.com",
                "https://edition.cnn.com"
            };

            await DownloadParallelAsync(sw, urls);
            //await DownloadSequentiallyAsync(sw, urls);

            TextBox1.Text += $"{sw.Elapsed.Seconds}s\n\n";
        }

        private async Task DownloadParallelAsync(Stopwatch sw, string[] urls) {
            var tasks = urls.Select(u => Task.Run(() => DownloadUrl(u))).ToList();
            StringBuilder sb = new StringBuilder();

            while (tasks.Count > 0) {
                string status = "";

                try {
                    Task<DownloadResult> taskDone = await Task.WhenAny(tasks);
                    tasks.Remove(taskDone);
                    DownloadResult dlRes = await taskDone;
                    status = $"{dlRes.Url}: {Math.Round(dlRes.Html.Length / 1000.0)}KB";
                }
                catch(AggregateException) {
                    status = "ae error";
                }
                catch (Exception e) {
                    status = "error";
                }

                TextBox1.Text += $"{status}  {sw.ElapsedMilliseconds}ms \n";
            }
        }

        private async Task DownloadSequentiallyAsync(Stopwatch sw, string[] urls) {
            string crtUrl = "";

            foreach (string url in urls) {
                crtUrl = url;
                string status = "";
                TextBox1.Text += $"{url}";

                try {
                    DownloadResult dlRes = await Task.Run(() => DownloadUrl(url));
                    status = $"{Math.Round(dlRes.Html.Length / 1000.0)}KB";
                }
                catch (Exception) {
                    status = "error";
                }

                TextBox1.Text += $": {status}\n";
            }
        }

        DownloadResult DownloadUrl(string url) {
            DownloadResult result = new DownloadResult { Url = url };
            using (var client = new WebClient()) {
                string html = client.DownloadString(url);
                result.Html = html;
            }
            return result;
        }

        private void Button2_Click(object sender, RoutedEventArgs e) {
            TextBox2.Text += "Hello!  ";
        }

        private void BtnClear1_Click(object sender, RoutedEventArgs e) {
            TextBox1.Text = "";
        }

        private class DownloadResult
        {
            public string Url { get; set; }
            public string Html { get; set; }
        }
    }
}
