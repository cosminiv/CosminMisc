using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Eu._Common;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_053
    {
        // TODO: Takes 7s; maybe find a faster way, which doesn't require LargeNumber.
        public static int Solve() {
            LargeNumber TARGET = 1000000;
            int MAX_N = 100;

            int howMany = 0;

            for (int n = 1; n <= MAX_N; n++) {
                for (int k = 1; k <= n; k++) {
                    LargeNumber C = ComputeCombinationCount(n, k);
                    //Console.WriteLine($"C({n}, {k}) = {C}");
                    if (C > TARGET)
                        howMany++;
                }
            }

            return howMany;
        }

        private static LargeNumber ComputeCombinationCount(int n, int k) {
            LargeNumber result = 1;

            if (k > n / 2) {
                for (int i = k + 1; i <= n; i++) 
                    result *= i;
                result /= ComputeFactorial(n - k);
            }
            else {
                for (int i = n - k + 1; i <= n; i++)
                    result *= i;
                result /= ComputeFactorial(k);
            }

            return result;
        }

        private static LargeNumber ComputeFactorial(int n) {
            LargeNumber result = 1;
            for (int i = 2; i <= n; i++) {
                result *= i;
            }
            return result;
        }
    }
}
