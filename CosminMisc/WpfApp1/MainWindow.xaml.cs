using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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
    public partial class MainWindow : Window
    {
        CancellationTokenSource _cancellation = new CancellationTokenSource();

        string[] _urls = {
                "https://news.google.com",
                "https://news.yahoo.com",
                "https://www.theguardian.com",
                "https://www.hotnews.ro",
                //"http://www.sicherheits-charta.ch",
                "https://www.youtube.com",
                "https://www.facebook.com",
                "https://www.linkedin.com",
                "https://edition.cnn.com",
            };

        public MainWindow() {
            InitializeComponent();
        }

        private void DownloadNewThread_Click(object sender, RoutedEventArgs e) {
            Stopwatch sw = Stopwatch.StartNew();
            TextBox1.Text += "Downloading...";

            Thread t = new Thread(() => {
                string html = new WebClient().DownloadString("https://news.yahoo.com");
                TextBox1.Dispatcher.Invoke(() => 
                    TextBox1.Text += $"\n{html.Length} chars in {sw.ElapsedMilliseconds}ms\n\n");
            });
            t.Start();
            //t.Join();

            //TextBox1.Text += $"\nDONE in {sw.ElapsedMilliseconds}ms\n\n";
        }

        private async void Download_Click(object sender, RoutedEventArgs e) {
            Stopwatch sw = Stopwatch.StartNew();
            _cancellation = new CancellationTokenSource();
            await DownloadSequentiallyAsync(sw, _urls);
            TextBox1.Text += $"\n{sw.ElapsedMilliseconds}ms\n\n";
        }

        private async void DownloadParallel_Click(object sender, RoutedEventArgs e) {
            Stopwatch sw = Stopwatch.StartNew();
            _cancellation = new CancellationTokenSource();
            await DownloadParallelAsync(sw, _urls);
            TextBox1.Text += $"\n{sw.ElapsedMilliseconds}ms\n\n";
        }

        private async Task DownloadParallelAsync(Stopwatch sw, string[] urls) {
            var tasks = urls.Select(u => Task.Run(() => DownloadUrlAsync(u), _cancellation.Token)).ToList();

            StringBuilder sb = new StringBuilder();
            Progress1.Value = 0;
            int i = 0;

            while (tasks.Count > 0) {
                //if (_cancellation.IsCancellationRequested) {
                //    TextBox1.Text += "\nCANCELED\n";
                //    break;
                //}

                string status = "";

                try {
                    Task<DownloadResult> taskDone = await Task.WhenAny(tasks);
                    tasks.Remove(taskDone);
                    DownloadResult dlRes = await taskDone;
                    status = $"{dlRes.Url}: {Math.Round(dlRes.Html.Length / 1024.0)}KB";
                }
                catch(OperationCanceledException oce) {
                    status = "CANCELED";
                }
                catch (AggregateException) {
                    status = "ae error";
                }
                catch (Exception e) {
                    status = "error";
                }

                TextBox1.Text += $"{status}  {sw.ElapsedMilliseconds}ms \n";
                i++;
                Progress1.Value = i * 100 / urls.Length;
            }
        }

        private async Task DownloadSequentiallyAsync(Stopwatch sw, string[] urls) {
            int i = 0;
            Progress1.Value = 0;

            foreach (string url in urls) {
                string status = "";
                
                try {
                    _cancellation.Token.ThrowIfCancellationRequested();
                    TextBox1.Text += $"{url}...";
                    DownloadResult dlRes = await DownloadUrlAsync(url);
                    status = $"{Math.Round(dlRes.Html.Length / 1024.0)}KB";
                }
                catch(OperationCanceledException oce) {
                    status = "CANCELED";
                }
                catch (Exception) {
                    status = "error";
                }

                TextBox1.Text += $" {status}  {sw.ElapsedMilliseconds}ms\n";
                i++;
                Progress1.Value = i * 100 / urls.Length;
            }
        }

        async Task<DownloadResult> DownloadUrlAsync(string url) {
            DownloadResult result = new DownloadResult { Url = url };
            using (var client = new WebClient()) {
                string html = await client.DownloadStringTaskAsync(url);
                result.Html = html;
            }
            return result;
        }

        private void BtnClear1_Click(object sender, RoutedEventArgs e) {
            TextBox1.Text = "";
            Progress1.Value = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            _cancellation.Cancel();
        }

        private class DownloadResult
        {
            public string Url { get; set; }
            public string Html { get; set; }
        }
    }
}
