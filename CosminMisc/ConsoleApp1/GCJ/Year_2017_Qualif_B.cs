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
        int _testCases = 0;

        public string Solve(string line) {
            if (_testCases == 0) {
                _testCases = int.Parse(line);
                return null;
            }

            StringBuilder sb = new StringBuilder(line);
            return Solve(sb, 1, line.Length);
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
