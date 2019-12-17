using System.Collections.Generic;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_027
    {
        public static long Solve()
        {
            int maxPrimeCount = 0;
            int maxA = 0;
            int maxB = 0;
            HashSet<long> primes = _Common.Tools.GetPrimesUpTo(1000000);

            for (int a = -999; a <= 999; a++)
            {
                for (int b = -999; b <= 999; b++)
                {
                    int primeCount = 0;

                    for (int n = 0; ; n++)
                    {
                        long x = n * n + a * n + b;
                        if (primes.Contains(x)) primeCount++;
                        else break;
                    }

                    if (primeCount > maxPrimeCount)
                    {
                        maxPrimeCount = primeCount;
                        maxA = a;
                        maxB = b;
                    }
                }
            }

            return maxA * maxB;
        }
    }
}
