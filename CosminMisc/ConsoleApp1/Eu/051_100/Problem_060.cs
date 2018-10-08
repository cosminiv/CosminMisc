using ConsoleApp1.Eu._Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_060
    {
        static Primes _primes;
        static List<long> _primesList;
        static readonly int MAX_PRIME = (int)1E+8;
        static readonly int MAX_CANDIDATE = (int)1E+4 - 1;

        static readonly byte CAN_STICK = 1;
        static readonly byte CAN_NOT_STICK = 2;

        static HashSet<Combination> _solutionsFound = new HashSet<Combination>();
        static long _stickChecks = 0;
        static byte[][] _stickCache;

        // TODO: Takes 1.5 minutes, make it faster.
        public static int Solve() {
            Stopwatch sw = Stopwatch.StartNew();

            _primes = new Primes(MAX_PRIME);
            long time1 = sw.ElapsedMilliseconds;

            _primesList = _primes.ToList();
            long time2 = sw.ElapsedMilliseconds;

            _stickCache = new byte[MAX_CANDIDATE + 1][];
            for (int i = 0; i < _stickCache.Length; i++)
                _stickCache[i] = new byte[MAX_CANDIDATE + 1];
            long time3 = sw.ElapsedMilliseconds;

            long time4 = 0, time5 = 0, time6 = 0;

            List<Combination> solutions = null;
            for (int setSize = 2; setSize <= 5; setSize++) {
                solutions = GenerateSolutions(setSize, solutions);
                if (setSize == 2) time4 = sw.ElapsedMilliseconds;
                if (setSize == 3) time5 = sw.ElapsedMilliseconds;
                if (setSize == 4) time6 = sw.ElapsedMilliseconds;
                PrintSolutions(solutions, setSize == 5);
            }
            int minSum = (int)solutions.OrderBy(s => s.Items.Sum()).First().Items.Sum();

            Console.WriteLine();
            //Console.WriteLine($"{_stickChecks} stick checks");
            Console.WriteLine($"Sieve: {time1}ms, list: {time2 - time1}ms, cache:{time3 - time2}ms, " +
                $"sol2: {time4 - time3}ms, sol3: {time5 - time4}ms, sol4: {time6 - time5}ms ");
            return minSum;
        }

        static void PrintSolutions(List<Combination> solutions, bool printElems) {
            string elems = printElems ? (": " + string.Join("  ", solutions.OrderBy(s => s.Items.Sum()).Take(10))) : "";
            Console.WriteLine($"{solutions.Count} elems" + elems + "\n");
        }

        static List<Combination> GenerateSolutions(int solutionSize, List<Combination> prevSizeSol) {
            if (solutionSize == 2)
                return GenerateSolutionsBaseCase();

            List<Combination> result = new List<Combination>();

            for (int i1 = 0; i1 < prevSizeSol.Count; i1++) {
                for (int i2 = i1 + 1; i2 < prevSizeSol.Count; i2++) {
                    Combine(prevSizeSol[i1], prevSizeSol[i2], result);
                }
            }

            return result;
        }

        static void Combine(Combination s1, Combination s2, List<Combination> result) {
            for (int i1 = 0; i1 < s1.Items.Count; i1++)
                Combine(s2, s1.Items[i1], result);

            for (int i2 = 0; i2 < s2.Items.Count; i2++)
                Combine(s1, s2.Items[i2], result);
        }

        static void Combine(Combination s, long item, List<Combination> result) {
            if (s.Items.All(solItem => CanStick(item, solItem))) {
                Combination newSolution = new Combination(s.Items, item);
                if (!_solutionsFound.Contains(newSolution)) {
                    result.Add(newSolution);
                    _solutionsFound.Add(newSolution);
                }
            }
        }

        static List<Combination> GenerateSolutionsBaseCase() {
            List<Combination> result = new List<Combination>();
            for (int i = 0; i < _primesList.Count; i++) {
                long n1 = _primesList[i];
                if (n1 > MAX_CANDIDATE) break;
                for (int j = i + 1; j < _primesList.Count; j++) {
                    long n2 = _primesList[j];
                    if (n2 > MAX_CANDIDATE) break;
                    if (CanStick(n1, n2))
                        result.Add(new Combination(new[] { n1, n2 }));
                }
            }
            return result;
        }

        static bool CanStick(long n1, long n2) {
            if (n1 == n2) return false;
            if (n2 < n1) return CanStick(n2, n1);
            byte cachedValue = _stickCache[n1][n2];
            if (cachedValue == CAN_STICK) return true;
            if (cachedValue == CAN_NOT_STICK) return false;

            _stickChecks++;
            // Test n1 followed by n2
            long m2 = GetDigitCountAsMultOfTen(n2);
            long n = n2 + m2 * 10 * n1;
            if (!_primes.IsPrime(n)) {
                _stickCache[n1][n2] = CAN_NOT_STICK;
                return false;
            }

            // Test n2 followed by n1
            long m1 = GetDigitCountAsMultOfTen(n1);
            n = n1 + m1 * 10 * n2;
            if (!_primes.IsPrime(n)) {
                _stickCache[n1][n2] = CAN_NOT_STICK;
                return false;
            }
            else {
                _stickCache[n1][n2] = CAN_STICK;
                return true;
            }
        }

        static long GetDigitCountAsMultOfTen(long n) {
            long multipleOfTen = 1;
            for (long n2 = n / 10; n2 > 0; n2 = n2 / 10)
                multipleOfTen *= 10;
            return multipleOfTen;
        }
    }
}
