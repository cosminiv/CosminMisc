using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConsoleApp1.Leet
{
    [TestFixture]
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

        [TestCase(1, 2, 0, ExpectedResult = 21)]
        [TestCase(1, 2, 1, ExpectedResult = 12)]
        [TestCase(12, 3, 0, ExpectedResult = 312)]
        [TestCase(12, 3, 1, ExpectedResult = 132)]
        [TestCase(12, 3, 2, ExpectedResult = 123)]
        [TestCase(123, 4, 0, ExpectedResult = 4123)]
        [TestCase(123, 4, 1, ExpectedResult = 1423)]
        [TestCase(123, 4, 2, ExpectedResult = 1243)]
        [TestCase(123, 4, 3, ExpectedResult = 1234)]
        [TestCase(12345, 6, 2, ExpectedResult = 126345)]
        public static int InsertDigit(int number, int newDigit, int insertionIndex)
        {
            int numberFactor = insertionIndex == 0 ? 1 : 10;
            int powerOfTen = (int)Math.Pow(10, newDigit - insertionIndex - 1);
            int result = 0;

            int multipliedNumber = number * numberFactor;
            int digitsAtStart = multipliedNumber - multipliedNumber % (powerOfTen * 10);

            result += digitsAtStart;
            result += newDigit * powerOfTen;
            result += number % powerOfTen;

            return result;
        }
    }
}
