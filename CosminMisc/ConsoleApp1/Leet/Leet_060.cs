using System;
using System.Collections.Generic;

namespace ConsoleApp1.Leet
{
    class Leet_060
    {
        private static Dictionary<int, List<int>> _permutationsBySize;

        public void Solve()
        {
            GetPermutation(3, 3);
        }

        public string GetPermutation(int n, int k)
        {
            BuildPermutationsUpToSize(n);
            string result = _permutationsBySize[n][k - 1].ToString();
            return result;
        }

        private static void BuildPermutationsUpToSize(int size)
        {
            if (_permutationsBySize == null)
            {
                _permutationsBySize = new Dictionary<int, List<int>>
                {
                    {1, new List<int> {1}}
                };
            }

            for (int i = 2; i <= size; i++)
            {
                if (!_permutationsBySize.TryGetValue(i, out List<int> _))
				{
                    List<int> perm = BuildPermutationsWithSize(i);
					_permutationsBySize.Add(i, perm);
				}
            }
        }

        private static List<int> BuildPermutationsWithSize(int size)
        {
            int newDigit = size;
            List<int> smallerPermutations = _permutationsBySize[size - 1];
            List<int> newPermutations = new List<int>();

            foreach (int smallerPermutation in smallerPermutations)
            {
                for (int i = 0; i < size; i++)
                {
                    int newPerm = InsertDigit(smallerPermutation, newDigit, i);
                    newPermutations.Add(newPerm);
                }    
            }

            newPermutations.Sort();
            return newPermutations;
        }

        private static int InsertDigit(int number, int newDigit, int insertionIndex)
        {
            if (insertionIndex == newDigit - 1)
                return 10 * number + newDigit;

            int powerOfTen = (int)Math.Pow(10, newDigit - insertionIndex - 1);
            int result = 0;

            result += number / powerOfTen * powerOfTen;
            result += newDigit * powerOfTen;
            result += number % powerOfTen;

            return result;
        }
    }
}
