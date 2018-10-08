using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._Common
{
    public class Primes
    {
        // false = prime, true = non-prime
        bool[] _sieve;

        public Primes(long maxPrime) {
            _sieve = GetPrimesUpTo(maxPrime);
        }

        public bool IsPrime(long n) {
            return false == _sieve[n];
        }

        public List<long> ToList() {
            List<long> result = new List<long>();
            for (int i = 2; i < _sieve.Length; i++)
                if (_sieve[i] == false)
                    result.Add(i);
            return result;
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
    }
}
