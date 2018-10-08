using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_046
    {
        public static long Solve() {
            int MAX = 1000000;
            HashSet<long> primes = Tools.GetPrimesUpTo(MAX);
            List<long> primesList = primes.OrderBy(a => a).ToList();
            
            for (int n = 3; n < MAX; n+=2) {
                if (!primes.Contains(n)) {
                    int primesIndex = 0;
                    for ( ; primesList[primesIndex] <= n - 2; primesIndex++) {
                        double x = Math.Sqrt((n - primesList[primesIndex]) / 2);
                        if (x == Math.Truncate(x)) break;
                    }

                    // Check if we tried all possible primes less than the current number
                    if (primesList[primesIndex] > n - 2) return n;
                }
            }

            return 0;
        }
    }
}
