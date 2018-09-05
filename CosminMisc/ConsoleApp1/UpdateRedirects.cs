using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public static class UpdateRedirects
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="existingRedirectsFile"></param>
        /// <param name="newRedirectsFile">Must have tab-separated values on each line: key\tvalue</param>
        /// <param name="sectionName"></param>
        public static void UpdateRedirectsInConfig(string existingRedirectsFile, string newRedirectsFile, string sectionName)
        {
            Dictionary<string, string> existingRedirects = GetExistingRedirects(existingRedirectsFile);
            Dictionary<string, string> newRedirects = GetNewRedirects(newRedirectsFile);

            UpdateExisting(existingRedirectsFile, newRedirects);
            InsertNew(existingRedirectsFile, sectionName, existingRedirects, newRedirects);
        }

        private static Dictionary<string, string> GetNewRedirects(string newRedirectsFile)
        {
            RedirectEqualityComparer comparer = new RedirectEqualityComparer();

            return File.ReadAllLines(newRedirectsFile)
                            .Select(line => line.Split('\t'))
                            .Select(arr => new Redirect { Key = arr[0].XmlEncode(), Value = arr[1].XmlEncode() })
                            .Distinct(comparer)
                            .ToDictionary(arr => arr.Key, arr => arr.Value);
        }

        static string XmlEncode(this string str)
        {
            return str.Replace("&", "&amp;");
        }

        private static Dictionary<string, string> GetExistingRedirects(string existingRedirectsFile)
        {
            RedirectEqualityComparer comparer = new RedirectEqualityComparer();

            return File.ReadAllLines(existingRedirectsFile)
                .Where(line => line.Contains("<add key="))
                .Select(line => ParseLineInConfig(line))
                .Distinct(comparer)
                .ToDictionary(arr => arr.Key, arr => arr.Value);
        }

        private static void InsertNew(string existingRedirectsFile, string sectionName, Dictionary<string, string> existingRedirects, Dictionary<string, string> newRedirects)
        {
            Dictionary<string, string> redirectsToInsert = newRedirects
                .Where(pair => !existingRedirects.ContainsKey(pair.Key))
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            List<string> linesToInsert = redirectsToInsert
                .Select(p => RedirectToConfigLine(p.Key, p.Value))
                .ToList();

            List<string> existingRedirectsLines = File.ReadAllLines(existingRedirectsFile).ToList();
            int insertIndex = existingRedirectsLines.IndexOf($"  <rewriteMap name=\"{sectionName}\">");
            if (insertIndex < 0)
                throw new Exception("Section not found: " + sectionName);

            existingRedirectsLines.InsertRange(insertIndex + 1, linesToInsert);
            File.WriteAllLines(existingRedirectsFile, existingRedirectsLines);
        }

        static Redirect ParseLineInConfig(string line)
        {
            string[] tokens = line.Split('"');
            string key = tokens[1].XmlEncode();
            string value = tokens[3].XmlEncode();
            return new Redirect { Key = key, Value = value };
        }

        private static void UpdateExisting(string redirectsFile, Dictionary<string, string> newRedirects)
        {
            StringBuilder sb = new StringBuilder();
            int replacedCount = 0;

            foreach (string line in File.ReadLines(redirectsFile))
            {
                string newLine = line;

                if (line.StartsWith("    <add"))
                {
                    string[] tokens = line.Split('"');
                    string key = tokens[1];
                    string value = tokens[3];

                    if (newRedirects.TryGetValue(key, out string newValue))
                    {
                        value = newValue;
                        replacedCount++;
                    }

                    newLine = RedirectToConfigLine(key, value);
                }

                sb.AppendLine(newLine);
            }

            if (replacedCount > 0)
                File.WriteAllText(redirectsFile, sb.ToString());
        }

        static string RedirectToConfigLine(string key, string value)
        {
            string line = $"    <add key=\"{key}\" value=\"{value}\" />";
            return line;
        }

        public static void DeleteDuplicateLines(string file, string fileOut)
        {
            int i = 0;
            bool isInsideRewrite = false;
            HashSet<string> existingKeys = new HashSet<string>();
            bool keepLine = true;

            File.Delete(fileOut);

            foreach (string line in File.ReadLines(file))
            {
                i++;
                keepLine = true;
                string lineTrimmed = line.Trim();

                if (lineTrimmed == "<rewrite>")
                    isInsideRewrite = true;

                if (lineTrimmed == "</rewrite>")
                    isInsideRewrite = false;


                if (isInsideRewrite && lineTrimmed.StartsWith("<add key=\""))
                {
                    int endIndex = lineTrimmed.IndexOf('"', 10);
                    string key = lineTrimmed.Substring(10, endIndex - 10);

                    if (!existingKeys.Contains(key))
                    {
                        existingKeys.Add(key);
                        keepLine = true;
                    }
                    else
                    {
                        Debug.WriteLine(i.ToString());
                        keepLine = false;
                    }
                }

                if (keepLine)
                    File.AppendAllText(fileOut, $"{line}\r\n");
            }
        }

        class Redirect
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        class RedirectEqualityComparer : IEqualityComparer<Redirect>
        {
            public bool Equals(Redirect x, Redirect y)
            {
                return x.Key == y.Key;
            }

            public int GetHashCode(Redirect obj)
            {
                return obj.Key.GetHashCode();
            }
        }
    }
}
