using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_003
    {
        static long _iterations = 0;

        public static long LargestFactor(long n)
        {
            Stopwatch sw = Stopwatch.StartNew();

            List<long> factors = new List<long>{ n };
            HashSet<long> primes = new HashSet<long>() { 2, 3, 5, 7, 11 };
            
            bool foundFactors = true;
            
            while (foundFactors)
            { 
                foundFactors = Factorize(factors, primes);
            }

            var a = sw.ElapsedMilliseconds;
            return factors.Last();
        }

        static bool Factorize(List<long> factors, HashSet<long> primes)
        {
            long n = factors.Last();

            if (primes.Contains(n))
                return false;

            for (long i = 2; i <= n / 2; i++)
            {
                _iterations++;

                long q = n / i;
                long r = n % i;

                if (r == 0)
                { 
                    factors.RemoveAt(factors.Count - 1);
                    factors.Add(i);
                    factors.Add(q);
                    return true;
                }
            }

            primes.Add(n);
            return false;
        }
    }
}
