using ConsoleApp1.Eu._Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_078
    {
        static readonly int TARGET = (int)1E+6;
        static Dictionary<long, Solution> _solutions = new Dictionary<long, Solution>();

        public static long Solve() {
            Stopwatch sw = Stopwatch.StartNew();

            _solutions.Add(1, new Solution());
            _solutions[1].Combinations.Add(new Combination(1));

            long result = 0;
            for (long n = 2; n < 50; n++) {
                int ways = GenerateWaysToSplit(n);
                Console.WriteLine($"{n}\t{ways}");
                Debug.Print($"{n}\t{ways}");
                //if (ways < 100) PrintWaysToSplit(n);
                if (ways % TARGET == 0) {
                    result = n;
                    break;
                }
            }

            return result;
        }

        private static void PrintWaysToSplit(long n) {
            string txt = string.Join("\n", _solutions[n].Combinations);
            Console.WriteLine(txt);
            Console.WriteLine();
        }

        static int GenerateWaysToSplit(long n) {
            Solution solution = new Solution();
            solution.Combinations.Add(new Combination(n));

            for (int i = 1; i <= n / 2; i++) {
                long dif = n - i;
                Solution smallerSolution = _solutions[dif];

                foreach (Combination smallerComb in smallerSolution.Combinations) {
                    Combination newComb = new Combination(smallerComb.Items, i);
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
