using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GCJ
{
    class Year_2017_Qualif_B
    {
        public int Solve(string inputFile, string outputFile) {
            Year_2017_Qualif_B solver = new Year_2017_Qualif_B();
            StringBuilder sb = new StringBuilder();
            int i = 0;

            foreach (string line in File.ReadLines(inputFile)) {
                if (i > 0 && line.Length > 0) {
                    string result = solver.Solve(line);
                    string outputLine = $"Case #{i}: {result}";

                    sb.AppendLine(outputLine);
                    Console.WriteLine(outputLine);
                }

                i++;
            }

            File.WriteAllText(outputFile, sb.ToString());
            return 0;
        }

        private string Solve(string inputNumber) {
            StringBuilder sb = new StringBuilder(inputNumber);
            return Solve(sb, 1, inputNumber.Length);
        }

        private string Solve(StringBuilder sb, int startIndex, int endIndex) {
            bool foundInversion;
            int i;

            do {
                foundInversion = false;
                for (i = startIndex; i < endIndex && i > 0; i++) {
                    if (sb[i] < sb[i - 1]) {
                        // Set the tail to 9.
                        for (int j = i; j < endIndex; j++) {
                            sb[j] = '9';
                        }

                        // Decrement previous digit
                        int prevDigit = sb[i - 1] - '0';
                        prevDigit--;
                        sb[i - 1] = prevDigit.ToString()[0];

                        foundInversion = true;
                        startIndex = i - 1;
                        break;
                    }
                }

                // Remove leading zero and adjust indexes
                if (sb[0] == '0') {
                    sb = sb.Remove(0, 1);
                    i--;
                    startIndex--;
                }
            }
            while (foundInversion);
            
            return sb.ToString();
        }
    }
}
