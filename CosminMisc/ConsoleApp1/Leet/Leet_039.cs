using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    //
    // Given a set of candidate numbers (candidates) (without duplicates) and a target number (target),
    // find all unique combinations in candidates where the candidate numbers sums to target.
    //
    // The same repeated number may be chosen from candidates unlimited number of times.
    //
    class Leet_039
    {
        int _duplicates = 0;

        public void Test() {
            var res = CombinationSum(new[] { 1, 3, 5 }, 17);
            foreach (var solution in res) {
                Console.WriteLine(String.Join(" ", solution));
            }
            Console.WriteLine($"\n{res.Count} solutions, {_duplicates} duplicates");
        }

        public IList<IList<int>> CombinationSum(int[] candidates, int target) {
            return Solve_Tree(candidates, target);
        }

        //
        // Generate a tree of all possible states, do a breadth-first traversal on it.
        //
        private IList<IList<int>> Solve_Tree(int[] candidates, int target) {
            IList<IList<int>> solutions = new List<IList<int>>();
            HashSet<int> solutionHashes = new HashSet<int>();
            long longTarget = target;

            Queue<int[]> factorsQueue = new Queue<int[]>();
            // root of tree
            factorsQueue.Enqueue(new int[candidates.Length]);

            while (factorsQueue.Count > 0) {
                int[] factors = factorsQueue.Dequeue();
                long sum = ComputeSum(candidates, factors);

                if (sum == longTarget) {
                    var solution = MakeListForResult(candidates, factors);
                    int solutionHash = ComputeHash(solution);
                    if (!solutionHashes.Contains(solutionHash)) {
                        solutions.Add(solution);
                        solutionHashes.Add(solutionHash);
                    }
                    else
                        _duplicates++;
                }
                else if (sum < longTarget) {
                    // generate and enqueue all possible next states from factors array
                    for (int i = 0; i < factors.Length; i++) {
                        int[] newFactors = new int[factors.Length];
                        Array.Copy(factors, newFactors, factors.Length);
                        newFactors[i]++;
                        factorsQueue.Enqueue(newFactors);
                    }
                }
            }

            return solutions;
        }

        private int ComputeHash(IEnumerable<int> list) {
            unchecked {
                int hash = 19;
                foreach (var x in list) 
                    hash = hash * 31 + x;
                return hash;
            }
        }

        private long ComputeSum(int[] candidates, int[] factors) {
            long result = 0;
            for (int i = 0; i < candidates.Length; i++) {
                result += candidates[i] * factors[i];
            }
            return result;
        }

        private IList<int> MakeListForResult(int[] candidates, int[] factors) {
            List<int> result = new List<int>(factors.Sum());
            for (int i = 0; i < candidates.Length; i++) {
                for (int j = 0; j < factors[i]; j++) {
                    result.Add(candidates[i]);
                }
            }
            return result;
        }
    }
}
