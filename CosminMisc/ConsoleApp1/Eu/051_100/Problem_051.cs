using ConsoleApp1.Eu._Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_051
    {
        static Primes _primes;

        static Dictionary<int, Combinations> _cache = new Dictionary<int, Combinations>();
        static long[] _multOfTen = new long[] { 1, 10, 100, 1000, 10000,
         (long)1E+5, (long)1E+6, (long)1E+7, (long)1E+8, (long)1E+9 };
        static long[] _primesFound = new long[15];


        public static int Solve() {
            int MAX = 1000000;
            int TARGET_COUNT = 8;

            for (int digits = 1; digits <= 5; digits++) {
                GenerateCombinations(digits);
                //	PrintCombinations(digits);
            }

            _primes = new Primes(MAX);
            List<long> primesList = _primes.ToList();

            // start at prime > 10
            for (int i = 5; i < primesList.Count; i++) {
                long p = primesList[i];
                if (HasPrimesAfterSubstitutions(p, TARGET_COUNT)) {
                    Console.WriteLine($"Found: {_primesFound[0]}");
                    break;
                }
            }

            return (int)_primesFound[0];
        }

        static bool HasPrimesAfterSubstitutions(
            long prime, int targetCount) {

            int digits = (int)Math.Log10(prime);

            // We don't want to substitute the first digit 
            // to the right (it would eventually be even => non prime)
            long trimmedPrime = prime / 10;
            long lastDigit = prime % 10;

            //Console.WriteLine($"prime = {prime},  digits-1 = {digits-1}");
            if (!_cache.ContainsKey(digits)) {
                //Console.WriteLine($"Generating combinations of {digits} digits");
                GenerateCombinations(digits);
            }

            Combinations comb = _cache[digits];

            for (int confIdx = 0; confIdx < comb.Configs.Count; confIdx++) {
                Config config = comb.Configs[confIdx];
                int primesFound = 0;

                for (int digit = 0; digit <= 9; digit++) {
                    long subst = GenerateSubstitution(trimmedPrime, digit, config, false);
                    long candidate = subst * 10 + lastDigit;

                    if (_primes.IsPrime(candidate) && (int)Math.Log10(candidate) == digits) {
                        _primesFound[primesFound] = candidate;
                        if (++primesFound >= targetCount) {
                            Console.WriteLine($"{string.Join(", ", _primesFound)}");
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        static long GenerateSubstitution(long n, int newDigit, Config config, bool debug) {
            long n2 = n;
            for (int i = 0; i < config.Positions.Count; i++) {
                int pos = config.Positions[i];
                long multOfTen = _multOfTen[pos];
                long oldDigitMultOfTen = n2 % (multOfTen * 10) - n2 % multOfTen;
                n2 = n2 - oldDigitMultOfTen + newDigit * multOfTen;
            }
            return n2;
        }

        static void GenerateCombinations(int length) {
            if (length == 1) {
                _cache.Add(1, new Combinations {
                    Configs = new List<Config>() {
                new Config { Positions = new List<int> { 0 } }
              }
                });
                return;
            }

            List<Config> crtConfigs = new List<Config>(2 * _cache[length - 1].Configs.Count + 1); ;
            Combinations comb = new Combinations { Configs = crtConfigs };

            // Add previous 
            List<Config> prevConfigs = _cache[length - 1].Configs;
            crtConfigs.AddRange(prevConfigs);

            // Add current
            crtConfigs.Add(new Config {
                Positions = new List<int> { length - 1 }
            });

            // Add combinations of current and previous
            for (int i = 0; i < prevConfigs.Count; i++) {
                Config newConfig = new Config();
                newConfig.Positions.AddRange(prevConfigs[i].Positions);
                newConfig.Positions.Add(length - 1);
                crtConfigs.Add(newConfig);
            }

            // Order by length
            comb.Configs = comb.Configs.OrderBy(c => c.Positions.Count).ToList();

            _cache.Add(length, comb);
        }

        static void PrintCombinations(int length) {
            Combinations comb = _cache[length];
            Console.WriteLine(comb.ToString());
        }

        class Combinations
        {
            public List<Config> Configs = new List<Config>();

            public override string ToString() {
                string result = string.Join(", ", Configs) + $" (count = {Configs.Count})";
                return result;
            }
        }

        class Config
        {
            public List<int> Positions = new List<int>();

            public override string ToString() {
                string result = string.Join("", Positions);
                return result;
            }
        }
    }
}
