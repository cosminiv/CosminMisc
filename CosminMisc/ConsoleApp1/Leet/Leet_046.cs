using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    //
    // Given a collection of distinct integers, return all possible permutations.
    //
    class Leet_046 {
        public void Test() {
            var result = Permute(new int[] { 1, 2, 3 });
            foreach (var lst in result) {
                Console.WriteLine(String.Join(" ", lst));
            }
        }

        public IList<IList<int>> Permute(int[] nums) {
            return Solve_Recursive(nums, 0);
        }

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
                    solutions.Add(newSol);
                }
            }

            return solutions;
        }

        private static IList<IList<int>> Solve_Iterative(int[] nums) {
            List<MySolution> smallerSolutions = new List<MySolution>();
            int N = nums.Length;

            // Build solutions of size 0.
            smallerSolutions.Add(new MySolution {
                List = new List<int>(),
                Remaining = nums.ToList()
            });

            if (nums.Length == 0)
                return smallerSolutions.Select(s => (IList<int>)s.List).ToList();

            List<MySolution> biggerSolutions = null;

            // Build solutions of sizes 1 up to N
            for (int solSize = 1; solSize <= N; solSize++) {
                biggerSolutions = new List<MySolution>();

                for (int solIndex = 0; solIndex < smallerSolutions.Count; solIndex++) {
                    MySolution smallerSol = smallerSolutions[solIndex];

                    for (int numIndex = 0; numIndex < smallerSol.Remaining.Count; numIndex++) {
                        int num = smallerSol.Remaining[numIndex];
                        List<int> newList = new List<int>(smallerSol.List);
                        newList.Add(num);

                        List<int> newRemaining = smallerSol.Remaining.Where(n => n != num).ToList();

                        biggerSolutions.Add(new MySolution {
                            List = newList,
                            Remaining = newRemaining
                        });
                    }
                }

                smallerSolutions = biggerSolutions;
            }

            return biggerSolutions.Select(s => (IList<int>)s.List).ToList();
        }

        class MySolution
        {
            public List<int> List { get; set; }
            public List<int> Remaining { get; set; }
        }
    }
}
