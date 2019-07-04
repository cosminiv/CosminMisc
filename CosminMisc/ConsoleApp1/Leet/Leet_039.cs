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
        int _states = 0;
        int _smaller = 0;
        int _bigger = 0;
        int _duplicates = 0;

        public void Test() {
            var res = CombinationSum(new[] { 1, 3, 4, 17 }, 23);
            foreach (var solution in res) {
                Console.WriteLine(String.Join(" ", solution));
            }
            Console.WriteLine($"\n{res.Count} solutions, {_states} states, {_duplicates} duplicates, {_smaller} smaller, {_bigger} bigger");
        }

        public IList<IList<int>> CombinationSum(int[] candidates, int target) {
            return Solve_Leet(candidates, target);
        }

        private IList<IList<int>> Solve_Leet(int[] candidates, int target) {
            List<IList<int>> list = new List<IList<int>>();
            Array.Sort(candidates);
            Solve_Leet(list, new List<int>(), candidates, target, 0);
            return list;
        }

        private void Solve_Leet(List<IList<int>> solutions, List<int> candidate, int[] nums, 
            int remain, int start) {
            _states++;
            if (remain < 0)
                return;
            else if (remain == 0)
                solutions.Add(new List<int>(candidate));
            else {
                for (int i = start; i < nums.Length && nums[i] <= remain; i++) {
                    candidate.Add(nums[i]);
                    Solve_Leet(solutions, candidate, nums, remain - nums[i], i); // not i + 1 because we can reuse same elements
                    candidate.RemoveAt(candidate.Count - 1);
                }
            }
        }

        //
        // Generate a tree of all possible states, do a breadth-first traversal on it.
        //
        private IList<IList<int>> Solve_Tree(int[] candidates, int target) {
            IList<IList<int>> solutions = new List<IList<int>>();
            HashSet<int> solutionHashes = new HashSet<int>();
            long longTarget = target;

            Queue<int[]> factorsQueue = new Queue<int[]>();
            factorsQueue.Enqueue(new int[candidates.Length]);  // root of tree

            while (factorsQueue.Count > 0) {
                int[] factors = factorsQueue.Dequeue();
                long sum = ComputeSum(candidates, factors);
                _states++;

                if (sum == longTarget) {
                    int solutionHash = ComputeHash(factors);

                    if (!solutionHashes.Contains(solutionHash)) {
                        var solution = MakeSolution(candidates, factors);
                        solutions.Add(solution);
                        solutionHashes.Add(solutionHash);
                    }
                    else
                        _duplicates++;
                }
                else if (sum < longTarget) {
                    _smaller++;
                    //Console.WriteLine($"Looking for {longTarget - sum}");
                    // generate and enqueue all possible next states from factors array
                    for (int i = 0; i < factors.Length; i++) {
                        int[] newFactors = new int[factors.Length];
                        Array.Copy(factors, newFactors, factors.Length);
                        newFactors[i]++;
                        factorsQueue.Enqueue(newFactors);
                    }
                }
                else
                    _bigger++;
                // only add children if sum is less than target; otherwise we know we would go over
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

        private IList<int> MakeSolution(int[] candidates, int[] factors) {
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
