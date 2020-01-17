using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    //
    // Given a collection of candidate numbers (candidates) and a target number (target), find all unique combinations in candidates where the candidate numbers sums to target.

    // Each number in candidates may only be used once in the combination.

    // Note:

    // All numbers (including target) will be positive integers.
    // The solution set must not contain duplicate combinations.
    //
    class Leet_040
    {
        int _states = 0;

        public void Test() {
            var res = CombinationSum2(new[] { 10, 1, 2, 7, 6, 1, 5 }, 8);

            Console.WriteLine("\n==================\n");

            foreach (var solution in res) {
                Console.WriteLine(String.Join(" ", solution));
            }

            Console.WriteLine("\n==================\n");
            Console.WriteLine($"\n{res.Count} solutions, {_states} states");
        }

        public IList<IList<int>> CombinationSum2(int[] candidates, int target) {
            return Solve_Leet(candidates, target);
        }

        private IList<IList<int>> Solve_Leet(int[] candidates, int target) {
            List<IList<int>> list = new List<IList<int>>();
            Array.Sort(candidates);
            Console.WriteLine(String.Join(" ", candidates));
            Solve_Leet(list, new List<int>(), candidates, target, 0);
            return list;
        }

        private void Solve_Leet(List<IList<int>> solutions, List<int> candidate, int[] nums,
            int remain, int start) {

            _states++;
            Console.Write(String.Join(" ", candidate));

            if (remain < 0) {
                Console.WriteLine();
                return;
            }
            else if (remain == 0) {
                solutions.Add(new List<int>(candidate));
                Console.WriteLine("\tOK");
            }
            else {
                Console.WriteLine();
                for (int i = start; i < nums.Length && nums[i] <= remain; i++) {
                    if (i > start && nums[i] == nums[i - 1])
                        continue;
                    candidate.Add(nums[i]);
                    Solve_Leet(solutions, candidate, nums, remain - nums[i], i + 1);
                    candidate.RemoveAt(candidate.Count - 1);
                }
            }
        }
    }
}
