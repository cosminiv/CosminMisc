using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_077
    {
        static bool[] _sieve;
        static List<long> _primesList;
        static readonly int MAX_PRIME = (int)1000;
        static int[] _waysCache = new int[1000];

        public static long Solve() {
            Stopwatch sw = Stopwatch.StartNew();

            _sieve = GetPrimesUpTo(MAX_PRIME);
            _primesList = MakePrimeList();
            long result = 0;

            for (long n = 2; ; n++) {
                int ways = CountWaysToSplit(n);
                Console.WriteLine($"{n}: {ways}");
                if (ways >= 5000) {
                    result = n;
                    break;
                }
            }

            return result;
        }

        static int CountWaysToSplit(long n) {
            int result = 0;
            if (IsPrime(n)) result++;

            for (int i = 0; i < _primesList.Count; i++) {
                long prime = _primesList[i];
                if (prime > n / 2) break;
                long dif = n - prime;
                result += _waysCache[dif];
            }

            _waysCache[n] = result;
            return result;
        }

        static List<long> MakePrimeList() {
            List<long> result = new List<long>();
            for (int i = 2; i < _sieve.Length; i++)
                if (_sieve[i] == false)
                    result.Add(i);
            return result;
        }

        static bool IsPrime(long n) {
            return false == _sieve[n];
        }

        static bool[] GetPrimesUpTo(int max) {
            // false = prime, true = non-prime
            bool[] candidates = new bool[max + 1];
            candidates[0] = candidates[1] = true;

            for (int i = 2; i < max / 2; i++) {
                if (candidates[i] == true)
                    continue;

                for (int j = i + i; j <= max; j = j + i)
                    candidates[j] = true;
            }

            return candidates;
        }
    }
}
