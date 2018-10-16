using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_061
    {
        static readonly Dictionary<int, List<int>> _polygonalNumbers = new Dictionary<int, List<int>>();

        // The key is of the form "x_y", where x and y are polygon sizes (3 through 8).
        static readonly Dictionary<string, List<Solution>> _intermediarySolutions = new Dictionary<string, List<Solution>>();

        static readonly int MIN_NUMBER = 1000;
        static readonly int MAX_NUMBER = 9999;

        public static int Solve() {
            GeneratePolygonalNumbers();
            List<string> polygonPermutations = Problem_024.GeneratePermutations("345678");

            foreach (string polygonPerm in polygonPermutations) {
                int sum = FindSolution(polygonPerm);
                if (sum > 0)
                    return sum;
            }

            return 0;
        }

        private static int FindSolution(string polygonPerm) {
            List<Solution> candidateSolutions = new List<Solution>();
            string key = "";
            int pairIndex = 0;

            for (pairIndex = 0; pairIndex < polygonPerm.Length - 1; pairIndex++) {
                int polygon1 = int.Parse(polygonPerm[pairIndex].ToString());
                int polygon2 = int.Parse(polygonPerm[(pairIndex + 1) % polygonPerm.Length].ToString());

                string pairKey = $"{polygon1}_{polygon2}";
                key += (key == "") ? pairKey : $"_{polygon2}";

                List<Solution> solutions = new List<Solution>();
                bool areSolutionsComputed = _intermediarySolutions.TryGetValue(key, out solutions);

                if (!areSolutionsComputed) {
                    List<Solution> cyclicalPairs = new List<Solution>();
                    bool arePairsComputed = _intermediarySolutions.TryGetValue(pairKey, out cyclicalPairs);

                    if (!arePairsComputed) {
                        cyclicalPairs = ComputeCyclicalPairs(polygon1, polygon2);
                        _intermediarySolutions.Add(pairKey, cyclicalPairs);
                    }

                    if (cyclicalPairs.Count == 0) return 0;  // If we found nothing, no point going further.

                    solutions = ComputeBiggerSolution(candidateSolutions, cyclicalPairs, key);
                    if (!_intermediarySolutions.ContainsKey(key))
                        _intermediarySolutions.Add(key, solutions);
                }

                candidateSolutions = solutions;
                if (candidateSolutions.Count == 0) return 0;   // If we found nothing, no point going further.
            }

            // Check that the last and first numbers are cyclical as well.
            candidateSolutions = candidateSolutions.Where(s => 
                s.Items[s.Items.Count - 1] % 100 == s.Items[0] / 100).ToList();
            if (!candidateSolutions.Any()) return 0;

            Console.WriteLine($"Permutation: {polygonPerm}");
            Console.WriteLine($"Solution: " + string.Join(", ", candidateSolutions[0].Items));

            return candidateSolutions[0].Items.Sum();
        }

        private static List<Solution> ComputeBiggerSolution(List<Solution> candidateSolutions, List<Solution> nextCyclicalPairs, string debugMsg) {
            List<Solution> result = new List<Solution>();

            if (candidateSolutions.Count == 0) {
                result.AddRange(nextCyclicalPairs.Select(p => new Solution(p.Items)));
                return result;
            }

            for (int i = 0; i < candidateSolutions.Count; i++) {
                Solution sol = candidateSolutions[i];
                int lastSolutionElem = sol.Items[sol.Items.Count - 1];
                int lastSolutionDigits = lastSolutionElem % 100;

                for (int j = 0; j < nextCyclicalPairs.Count; j++) {
                    Solution pair = nextCyclicalPairs[j];
                    int firstPairElem = pair.Items[0];
                    int secondPairElem = pair.Items[1];

                    if (lastSolutionElem == firstPairElem)  // && lastSolutionDigits == firstPairDigits
                        result.Add(new Solution(sol.Items.Append(secondPairElem).ToList()));
                }
            }

            return result;
        }

        private static List<Solution> ComputeCyclicalPairs(int polygon1, int polygon2) {
            List<Solution> result = new List<Solution>();
            List<int> numbers1 = _polygonalNumbers[polygon1];
            List<int> numbers2 = _polygonalNumbers[polygon2];

            foreach (int n1 in numbers1) {
                int finalDigitsOfN1 = n1 % 100;
                foreach (int n2 in numbers2) {
                    int startDigitsOfN2 = n2 / 100;
                    if (finalDigitsOfN1 == startDigitsOfN2)
                        result.Add(new Solution(n1, n2));
                }
            }
            
            return result;
        }

        class Solution
        {
            public List<int> Items = new List<int>();
            public Solution(List<int> items) {
                Items = items;
            }
            public Solution(params int[] items) {
                Items = items.ToList();
            }
            public override string ToString() {
                return string.Join(", ", Items);
            }
        }

        private static void GeneratePolygonalNumbers() {
            for (int i = 3; i <= 8; i++) {
                _polygonalNumbers.Add(i, new List<int>());
            }

            GeneratePolygonalNumbers(3, n => n * (n + 1) / 2);
            GeneratePolygonalNumbers(4, n => n * n);
            GeneratePolygonalNumbers(5, n => n * (3 * n - 1) / 2);
            GeneratePolygonalNumbers(6, n => n * (2 * n - 1));
            GeneratePolygonalNumbers(7, n => n * (5 * n - 3) / 2);
            GeneratePolygonalNumbers(8, n => n * (3 * n - 2));
        }

        private static void GeneratePolygonalNumbers(int polygonSize, Func<int, int> formula) {
            
            for (int i = 1; ; i++) {
                int n = formula(i);

                if (n < MIN_NUMBER) continue;
                if (n > MAX_NUMBER) return;

                if (n % 100 != 0)
                    _polygonalNumbers[polygonSize].Add(n);
            }
        }
    }
}
