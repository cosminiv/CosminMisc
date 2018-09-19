using ConsoleApp1.Eu.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_012
    {
        public static long Solve()
        {
            int targetDivisorCount = 500;   // 1800: 15s with 100k primes
            int primesToPrecompute = 10000;
            long n = 0;
            HashSet<long> primes = Tools.GetPrimes(primesToPrecompute);

            for (long i = 1; ; i++)
            {
                n += i;

                int count = Tools.GetDivisorCount(n, primes);
                
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
