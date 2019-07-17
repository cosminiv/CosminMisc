using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1.Misc
{
    class DownloadMp3s
    {
        public void Download_DW_WarumNicht() {
            string folder = @"C:\Cosmin\Deutsch\DW - Warum nicht";
            IEnumerable<string> urls = ReadUrls_DW(folder);
            foreach (string url in urls) {
                //Console.WriteLine(url);
                Download(url, folder);
            }
        }

        public void Download_DW_Misc() {
            string folder = @"C:\Cosmin\Deutsch\DW - Misc";
            string rootUrl = "https://radiodownloaddw-a.akamaihd.net/Events/dwelle/dira/mp3/deu/";

            string[] mp3s = new[] {
                ""
            };

            foreach (string mp3 in mp3s) {
                Download(rootUrl + mp3, folder);
            }
        }

        private List<string> ReadUrls_DW(string folder) {
            List<string> result = new List<string>();

            for (int i = 1; i <= 4; i++) {
                string file = $"DKpodcast_dwn{i}_en.xml";
                string filePath = Path.Combine(folder, file);
                XDocument xdoc = XDocument.Load(filePath);
                foreach (var enc in xdoc.Descendants("enclosure")) {
                    result.Add(enc.Attribute("url").Value);
                }
            }

            result.Sort();
            return result;
        }

        private void Download(string url, string folder) {
            int idx = url.LastIndexOf("/");
            string name = url.Substring(idx + 1);
            string filePath = Path.Combine(folder, name);
            Console.WriteLine(name);

            using (WebClient client = new WebClient()) {
                if (!File.Exists(filePath))
                    client.DownloadFile(url, filePath);
            }
        }
    }
}
