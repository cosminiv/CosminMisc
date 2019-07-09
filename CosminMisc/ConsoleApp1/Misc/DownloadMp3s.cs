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
        readonly string _folder = @"C:\Cosmin\Deutsch\DW - Warum nicht";

        public void Download() {
            IEnumerable<string> urls = ReadUrls();
            foreach (string url in urls) {
                //Console.WriteLine(url);
                Download(url);
            }
        }

        private List<string> ReadUrls() {
            List<string> result = new List<string>();

            for (int i = 1; i <= 4; i++) {
                string file = $"DKpodcast_dwn{i}_en.xml";
                string filePath = Path.Combine(_folder, file);
                XDocument xdoc = XDocument.Load(filePath);
                foreach (var enc in xdoc.Descendants("enclosure")) {
                    result.Add(enc.Attribute("url").Value);
                }
            }

            result.Sort();
            return result;
        }

        private void Download(string url) {
            int idx = url.LastIndexOf("/");
            string name = url.Substring(idx + 1);
            string filePath = Path.Combine(_folder, name);
            Console.WriteLine(name);

            using (WebClient client = new WebClient()) {
                if (!File.Exists(filePath))
                    client.DownloadFile(url, filePath);
            }
        }
    }
}
