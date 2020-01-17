using System;
using System.Collections.Generic;
using ConsoleApp1.Common;

namespace ConsoleApp1.Leet
{
    //
    // Given a collection of numbers that might contain duplicates, return all possible unique permutations.
    // 
    class Leet_047
    {
        static int _duplicates = 0;

        public void Test() {
            var result = PermuteUnique(new int[] { 1, 2, 3, 1, 7, 2, 9, 4, 0, 5 });
            //StringBuilder sb = new StringBuilder();
            //foreach (var lst in result)
            //    sb.AppendLine(String.Join(" ", lst));
            //Console.Write(sb.ToString());
            Console.WriteLine($"{result.Count} results, {_duplicates} duplicates");
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
            HashSet<int> solutionsHashes = new HashSet<int>();

            foreach (var smallerSol in smallerSolutions) {
                for (int i = 0; i <= smallerSol.Count; i++) {

                    List<int> newSol = MakeNewSolution(smallerSol, n, i);
                    int solHash = newSol.ComputeHashCode();

                    if (!solutionsHashes.Contains(solHash)) {
                        solutions.Add(newSol);
                        solutionsHashes.Add(solHash);
                    }
                    else
                        _duplicates++;
                }
            }

            return solutions;
        }

        private static List<int> MakeNewSolution(IList<int> smallerSol, int newNumber, int insertionIndex) {
            List<int> newSol = new List<int>(smallerSol.Count + 1);

            for (int i = 0; i < insertionIndex && i < smallerSol.Count; i++)
                newSol.Add(smallerSol[i]);

            newSol.Add(newNumber);

            for (int i = insertionIndex + 1; i <= smallerSol.Count; i++)
                newSol.Add(smallerSol[i - 1]);

            return newSol;
        }

    }
}
