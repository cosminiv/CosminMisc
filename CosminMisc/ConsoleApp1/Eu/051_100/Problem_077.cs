using ConsoleApp1.Eu._Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_077
    {
        static Primes _primes;
        static List<long> _primesList;
        static readonly int MAX_PRIME = (int)1000;
        static Dictionary<long, Solution> _solutions = new Dictionary<long, Solution>();

        public static long Solve() {
            Stopwatch sw = Stopwatch.StartNew();

            _primes = new Primes(MAX_PRIME);
            _primesList = _primes.ToList();
            long result = 0;

            for (long n = 2; ; n++) {
                int ways = GenerateWaysToSplit(n);
                Console.WriteLine($"{n}: {ways}");
                if (ways >= 5000) {
                    result = n;
                    break;
                }
            }

            return result;
        }

        static int GenerateWaysToSplit(long n) {
            Solution solution = new Solution();
            if (_primes.IsPrime(n)) 
                solution.Combinations.Add(new Combination(n));

            for (int i = 0; i < _primesList.Count; i++) {
                long prime = _primesList[i];
                if (prime > n / 2) break;
                long dif = n - prime;
                Solution smallerSolution = _solutions[dif];

                foreach (Combination smallerComb in smallerSolution.Combinations) {
                    Combination newComb = new Combination(smallerComb.Items, prime);
                    solution.Combinations.Add(newComb);
                }
            }

            _solutions.Add(n, solution);
            return solution.Combinations.Count;
        }

        class Solution
        {
            public HashSet<Combination> Combinations = new HashSet<Combination>();
        }


    }
}
