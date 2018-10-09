using ConsoleApp1.Eu._Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_058
    {
        public static long Solve() {
            int primes = 0;
            int nonPrimes = 1;  // 1
            float ratio = 0;

            for (long i = 3; ; i += 2) {
                long n = (i - 2) * (i - 2) + (i - 1);
                long n2 = n + i - 1;
                long n3 = n2 + i - 1;

                // A sieve would need to be too big, just check each number individually.
                if (Tools.IsPrime(n)) primes++; else nonPrimes++;
                if (Tools.IsPrime(n2)) primes++; else nonPrimes++;
                if (Tools.IsPrime(n3)) primes++; else nonPrimes++;
                nonPrimes++;  // one of the corners of the square is always a square number

                ratio = (float)primes / (float)(nonPrimes + primes);

                if ((i + 1) % 500 == 0)
                    Console.WriteLine($"square = {i+1}; n = {n}; ratio = {ratio * 100}%");

                if (ratio < .1) {
                    Console.WriteLine($"square = {i}; n = {n}; ratio = {ratio * 100}%");
                    return i;
                }
            }
        }
    }
}
