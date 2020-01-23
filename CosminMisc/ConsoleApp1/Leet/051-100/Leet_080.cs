
using System;
using System.Linq;
using System.Diagnostics;
using ConsoleApp1.Common;

namespace ConsoleApp1.Leet._051_100
{
    public class Leet_080
    {
        public void Solve()
        {
            (int[], int[])[] testCases = 
            {
                (new int[0], new int[0]),
                (new[] {1}, new[]{1}),
                (new[] {1, 1}, new[] {1, 1}),
                (new[] {1, 2}, new[] {1, 2}),
                (new[] {1, 1, 1}, new[] {1, 1}),
                (new[] {1, 1, 2}, new[] {1, 1, 2}),
                (new[] {1, 2, 2}, new[] {1, 2, 2}),
                (new[] {1, 2, 3}, new[] {1, 2, 3}),
                (new[] {1, 2, 2, 2}, new[] {1, 2, 2}),
                (new[] {1, 2, 2, 2}, new[] {1, 2, 2}),
                (new[] {2, 2, 2, 2}, new[] {2, 2}),
                (new[] {1, 2, 3, 4}, new[] {1, 2, 3, 4}),
                (new[] {1, 2, 2, 3}, new[] {1, 2, 2, 3}),
                (new[] {2, 2, 2, 3, 3, 3}, new[] {2, 2, 3, 3}),
                (new[] {2, 2, 2, 3, 4, 4, 4}, new[] {2, 2, 3, 4, 4}),
                (new[] {2, 2, 2, 3, 3, 4, 4, 4}, new[] {2, 2, 3, 3, 4, 4}),
            };

            foreach ((int[], int[]) testCase in testCases)
            {
                int[] input = testCase.Item1;
                int[] inputClone = (int[])input.Clone();
                int[] expectedArray = testCase.Item2;
                int expectedNumber = expectedArray.Length;

                int result = RemoveDuplicates(input);
                Debug.Assert(result == expectedNumber, Tools.CollectionToString(inputClone));

                int[] resultArray = input.Take(result).ToArray();
                string resultArrayStr = Tools.CollectionToString(resultArray);
                Debug.Assert(Tools.AreCollectionsEqual(resultArray, expectedArray), Tools.CollectionToString(inputClone));
            }
        }

        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length < 3) return nums.Length;

            int readIndex = 1, writeIndex = 0;
            int numberRead = nums[0], countRead = 1;
            
            while (readIndex < nums.Length)
            {
                if (nums[readIndex] != numberRead)
                {
                    numberRead = nums[readIndex];
                    countRead = 1;
                }
                else
                {
                    countRead++;
                }

                bool shouldWrite = countRead <= 2 && writeIndex < readIndex && writeIndex > 1;

                if (shouldWrite)
                {
                    nums[writeIndex] = numberRead;
                    writeIndex++;
                }

                readIndex++;

                if (countRead <= 2 && !shouldWrite)
                    writeIndex++;
            }

            int result = Math.Min(writeIndex + 1, nums.Length);

            return result;
        }
    }
}
