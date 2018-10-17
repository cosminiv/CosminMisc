using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._Common
{
    public class Primes
    {
        // false = prime, true = non-prime
        bool[] _sieve;

        public Primes(long maxPrime) {
            _sieve = GetPrimesUpTo(maxPrime);
            //_sieve = GetPrimesUpToParallel(maxPrime);
        }

        public bool IsPrime(long n) {
            return false == _sieve[n];
        }

        public List<long> ToList() {
            return ToListSerial();
            //return ToListParallel();
        }

        private List<long> ToListSerial() {
            List<long> result = new List<long>();
            for (int i = 2; i < _sieve.Length; i++)
                if (_sieve[i] == false)
                    result.Add(i);
            return result;
        }

        private List<long> ToListParallel() {
            //List<long> result = new List<long>();
            ConcurrentBag<long> result = new ConcurrentBag<long>();

            Parallel.For(2, _sieve.Length, i => {
                if (_sieve[i] == false)
                    result.Add(i);
            });

            Console.WriteLine($"ConcurrentBag size: {result.Count}");
            return new List<long>();
        }

        bool[] GetPrimesUpTo(long max) {
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

        bool[] GetPrimesUpToParallel(long max) {
            // false = prime, true = non-prime
            bool[] candidates = new bool[max + 1];
            candidates[0] = candidates[1] = true;

            // this will be "global" for all threads.
            long startingPrime = 2;

            IEnumerable<long> primes = GetStartingPrimes();

            Parallel.ForEach(primes, p => {
                for (long j = p + p; j <= max; j = j + p)
                    candidates[j] = true;
            });

            return candidates;

            IEnumerable<long> GetStartingPrimes() {
                for (long i = startingPrime; i < max / 2; i++) {
                    if (candidates[i] == false) {
                        Interlocked.Increment(ref startingPrime);
                        yield return i;
                    }
                }
            }
        }
    }
}
