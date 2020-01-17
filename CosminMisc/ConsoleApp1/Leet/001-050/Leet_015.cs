using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    class Leet_015
    {
        public int Solve() {
            int[] nums = { -1, 0, 1, 2, -1, -4 };
            var result = ThreeSum(nums);
            return result.Count;
        }

        public IList<IList<int>> ThreeSum(int[] nums) {
            IList<IList<int>> result = new List<IList<int>>();
            if (CheckEasyCases(nums, ref result))
                return result;

            PrepareNumbers(nums, out Dictionary<int, int> numsDict, out List<int> numsNoTripl);
            HashSet<int> tripletsReturned = new HashSet<int>();

            for (int i = 0; i < numsNoTripl.Count - 1; i++) {
                int n1 = numsNoTripl[i];
                
                for (int j = i + 1; j < numsNoTripl.Count; j++) {
                    numsDict[n1]--;
                    int n2 = numsNoTripl[j];
                    numsDict[n2]--;

                    // Undo change if not enough n2
                    if (numsDict[n2] < 0) {
                        numsDict[n1]++;
                        numsDict[n2]++;
                        continue;
                    }

                    int n3 = -1 * (n1 + n2);
                    if (numsDict.ContainsKey(n3) && numsDict[n3] > 0) {
                        List<int> triplet = new[] { n1, n2, n3 }.OrderBy(n => n).ToList();
                        //MakeSortedTriplet(n1, n2, n3);
                        int hash = $"{triplet[0]}_{triplet[1]}_{triplet[2]}".GetHashCode();
                            //GetHashCode(triplet[0], triplet[1], triplet[2]);

                        if (!tripletsReturned.Contains(hash)) {
                            result.Add(new List<int>(triplet));
                            tripletsReturned.Add(hash);
                        }

                        numsDict[n1]++;
                        numsDict[n2]++;
                    }
                    else {
                        numsDict[n1]++;
                        numsDict[n2]++;     // Undo change if not enough n3
                    }
                }
            }

            return result;
        }

        private bool CheckEasyCases(int[] nums, ref IList<IList<int>> result) {
            if (nums.Length < 100)
                return false;

            bool allPositive = true;
            bool allNegative = true;
            bool allZero = true;

            foreach (int n in nums) {
                if (n < 0) {
                    allPositive = false;
                    allZero = false;
                }
                if (n > 0) {
                    allNegative = false;
                    allZero = false;
                }
            }

            if (allZero) {
                result = new List<IList<int>> { new List<int> { 0, 0, 0 } };
                return true;
            }

            if (allPositive || allNegative) {
                result = new List<IList<int>> { };
            }

            return false;
        }

        private int GetHashCode(int a, int b, int c) {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + a;
                hash = hash * 23 + b;
                hash = hash * 23 + c;
                return hash;
            }
        }

        private void PrepareNumbers(int[] nums, out Dictionary<int, int> numberCounts, out List<int> numbersWithoutTriplicates) {
            numberCounts = new Dictionary<int, int>();
            numbersWithoutTriplicates = new List<int>();

            foreach (int n in nums) {
                int c = numberCounts.ContainsKey(n) ? (numberCounts[n] + 1) : 1;

                bool shouldKeepNumber = (c < 3 || (n == 0 && c == 3));

                if (shouldKeepNumber) {
                    numberCounts[n] = c;
                    numbersWithoutTriplicates.Add(n);
                }
            }

            if (numbersWithoutTriplicates.Count < (nums.Length * 0.9))
                Console.WriteLine($"{nums.Length} -> {numbersWithoutTriplicates.Count}");
        }
    }
}
