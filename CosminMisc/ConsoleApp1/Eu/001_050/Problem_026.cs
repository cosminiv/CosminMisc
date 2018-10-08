using ConsoleApp1.Eu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_026
    {
        public static int Solve() {
            int MAX = 1000;
            int MAX_DIGITS = 100;
            LargeNumber N = LargeNumber.Power(10, MAX_DIGITS);
            int MAX_SKIP = 20;

            int maxLenFound = 6;
            int numberForMaxRun = 7;

            for (int divisor = 8; divisor < MAX; divisor++) {
                LargeNumber q = LargeNumber.Divide(N, divisor, out LargeNumber remainder);
                string digits = q.ToString();

                for (int skip = 0; skip < MAX_SKIP; skip++) {
                    int maxLenToCheck = (MAX_DIGITS - skip) / 2;
                    for (int len = maxLenFound + 1; len < length; len++) {
                        string candidate1 = digits.Substring(skip, len)
                    }
                }

                //Console.WriteLine($"\t{i}:\t{q}");
            }

            return 0;
        }
    }
}
