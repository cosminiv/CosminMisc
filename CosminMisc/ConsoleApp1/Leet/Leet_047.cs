using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    //
    // Given a collection of numbers that might contain duplicates, return all possible unique permutations.
    // 
    class Leet_047
    {
        public void Test() {
            var result = PermuteUnique(new int[] { 1, 2, 3, 1 });
            foreach (var lst in result) {
                Console.WriteLine(String.Join(" ", lst));
            }
        }

        public IList<IList<int>> PermuteUnique(int[] nums) {
            return Solve_Recursive(nums, 0);
        }

        // ==========================================

        private static IList<IList<int>> Solve_Recursive(int[] nums, int startIndex) {
            if (startIndex >= nums.Length)
                return new List<IList<int>>() { new List<int>() };

            var smallerSolutions = Solve_Recursive(nums, startIndex + 1);
            return Combine(smallerSolutions, nums[startIndex]);
        }

        private static IList<IList<int>> Combine(IList<IList<int>> smallerSolutions, int n) {
            IList<IList<int>> solutions = new List<IList<int>>();

            foreach (var smallerSol in smallerSolutions) {
                for (int i = 0; i <= smallerSol.Count; i++) {
                    List<int> newSol = new List<int>(smallerSol);
                    newSol.Insert(i, n);
                    if (!FindListInLists(solutions, newSol))
                        solutions.Add(newSol);
                }
            }

            return solutions;
        }

        private static bool FindListInLists(IList<IList<int>> lists, List<int> list) {
            foreach (var crtList in lists) {
                bool found = true;
                
                for (int i = 0; i < crtList.Count; i++) {
                    if (crtList[i] != list[i]) {
                        found = false;
                        break;
                    }
                }

                if (found)
                    return true;
            }

            return false;
        }
    }
}
