using System;
using System.Linq;

namespace ConsoleApp1.Eu._001_050 {
    public class Problem_047 
    {
        // TODO: Try to make it faster by generating numbers starting from prime numbers, 
        // instead of computing the factors for each number.

        public static long Solve() {
            long MAX = 1000000;
            var primes = _Common.Tools.GetPrimesUpTo((int)MAX).OrderBy(a => a).ToList();
            long result = 0;
            int numbersWithFourFactors = 0;

            for (long n = 2; n < MAX; n++) {
                bool has4factors = _Common.Tools.GetPrimeFactors(n, primes).Count == 4;
                if (has4factors) {
                    if (++numbersWithFourFactors == 4) {
                        result = n - 3;
                        break;
                    }
                }
                else numbersWithFourFactors = 0;
            }

            for (int i = 0; i < 4; i++) {
                Console.WriteLine($"{result + i} = " + string.Join(" x ", _Common.Tools.GetPrimeFactors(result + i, primes)));
            }

            return result;
        }

    }
}
