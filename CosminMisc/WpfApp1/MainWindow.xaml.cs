using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            using (HttpClient client = new HttpClient()) {
                try {
                    Stopwatch sw = Stopwatch.StartNew();
                    string txt = await client.GetStringAsync("http://sicherheits-charta.ch");
                    TextBox1.Text = $"{sw.Elapsed.Seconds}s\n\n{txt}";
                }
                catch (Exception ex) {
                    TextBox1.Text = ex.ToString();
                }
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e) {
            TextBox2.Text += "Hello!  ";
        }

        private void BtnClear1_Click(object sender, RoutedEventArgs e) {
            TextBox1.Text = "";
        }
    }
}
