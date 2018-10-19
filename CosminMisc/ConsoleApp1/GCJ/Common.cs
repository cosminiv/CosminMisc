using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GCJ
{
    class Common
    {
        public static void SolveTestCases(Func<string, string> solver, string inputFile, string outputFile) {
            StringBuilder sb = new StringBuilder();
            int i = 0;

            foreach (string line in File.ReadLines(inputFile)) {
                if (i > 0 && line.Length > 0) {
                    string result = solver(line);
                    string outputLine = $"Case #{i}: {result}";

                    sb.AppendLine(outputLine);
                    Console.WriteLine(outputLine);
                }

                i++;
            }

            File.WriteAllText(outputFile, sb.ToString());
        }
    }
}
