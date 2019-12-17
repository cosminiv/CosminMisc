using System;
using System.Collections.Generic;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_012
    {
        public static long Solve()
        {
            int targetDivisorCount = 500;   // 1800: 15s with 100k primes
            int primesToPrecompute = 10000;
            long n = 0;
            HashSet<long> primes = _Common.Tools.GetPrimes(primesToPrecompute);

            for (long i = 1; ; i++)
            {
                n += i;

                long count = _Common.Tools.GetDivisorCount(n, primes);
                
                if (count > targetDivisorCount)
                {
                    Console.Write($"{n}   ");
                    Console.Write($"  Count = {count}  ");
                    Console.WriteLine();
                    return n;
                }
            }
        }
    }
}
