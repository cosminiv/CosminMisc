using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Algorithms
{
    /// <summary>
    /// Find the subarray with the maximum sum.
    /// </summary>
    public class MaxSubarray
    {
        public static MaxSubarrayResult SolveRecursive(int[] data)
        {
            return SolveRecursive(data, 0, data.Length - 1);
        }

        public static MaxSubarrayResult SolveBruteForce(int[] data)
        {
            int? maxSum = null;
            int? indexLeft = null;
            int? indexRight = null;

            for (int i = 0; i < data.Length; i++)
            {
                int sum = 0;
                for (int j = i; j < data.Length; j++)
                {
                    sum += data[j];
                    if (maxSum == null || sum > maxSum.Value)
                    {
                        maxSum = sum;
                        indexLeft = i;
                        indexRight = j;
                    }
                }
            }

            return new MaxSubarrayResult
            {
                MaxSum = maxSum.Value,
                StartIndex = indexLeft.Value,
                EndIndex = indexRight.Value
            };
        }

        public static MaxSubarrayResult SolveLinear(int[] data)
        {
            int? maxSum = null;
            int? indexLeft = null;
            int? indexRight = null;
            int? partialSum = null;
            int partialIndexLeft = 0;

            for (int i = 0; i < data.Length; i++)
            {
                int n = data[i];
                partialSum = (partialSum ?? 0) + n;

                if (maxSum == null || partialSum > maxSum.Value)
                {
                    maxSum = partialSum.Value;
                    indexLeft = partialIndexLeft;
                    indexRight = i;
                }

                if (partialSum < 0)
                {
                    partialIndexLeft = i + 1;
                    partialSum = null;
                }
            }

            return new MaxSubarrayResult
            {
                MaxSum = maxSum,
                StartIndex = indexLeft,
                EndIndex = indexRight
            };
        }

        private static MaxSubarrayResult SolveRecursive(int[] data, int lowIndex, int highIndex)
        {
            if (lowIndex == highIndex)
                return new MaxSubarrayResult { MaxSum = data[lowIndex], StartIndex = lowIndex, EndIndex = lowIndex };

            int midIndex = (lowIndex + highIndex) / 2;
            MaxSubarrayResult leftResult = SolveRecursive(data, lowIndex, midIndex);
            MaxSubarrayResult rightResult = SolveRecursive(data, midIndex + 1, highIndex);
            MaxSubarrayResult midResult = SolveMid(data, lowIndex, midIndex, highIndex);

            if (leftResult.MaxSum >= rightResult.MaxSum && leftResult.MaxSum >= midResult.MaxSum)
                return leftResult;
            else if (rightResult.MaxSum >= leftResult.MaxSum && rightResult.MaxSum >= midResult.MaxSum)
                return rightResult;
            else
                return midResult;
        }

        private static MaxSubarrayResult SolveMid(int[] data, int lowIndex, int midIndex, int highIndex)
        {
            int? maxSumLeft = null;
            int? indexLeft = null;
            int? maxSumRight = null;
            int? indexRight = null;
            int sum = 0;

            for (int i = midIndex; i >= lowIndex; i--)
            {
                sum += data[i];
                if (!maxSumLeft.HasValue || sum > maxSumLeft.Value)
                {
                    maxSumLeft = sum;
                    indexLeft = i;
                }
            }

            sum = 0;
            for (int i = midIndex + 1; i <= highIndex; i++)
            {
                sum += data[i];
                if (!maxSumRight.HasValue || sum > maxSumRight.Value)
                {
                    maxSumRight = sum;
                    indexRight = i;
                }
            }

            return new MaxSubarrayResult
            {
                MaxSum = maxSumLeft.Value + maxSumRight.Value,
                StartIndex = indexLeft.Value,
                EndIndex = indexRight.Value
            };
        }

        public static void TestSpeed()
        {
            for (int size = 1; size <= 400; size++)
            {
                //Console.Write("Element count = ");
                //string text = Console.ReadLine();

                int elementCount = size; // int.Parse(text);
                int repeatCount = 12;

                int minValue = -100;
                int maxValue = 100;

                Random random = new Random();


                for (int repeatIndex = 0; repeatIndex < repeatCount; repeatIndex++)
                {
                    int[] data = new int[elementCount];

                    for (int elemIndex = 0; elemIndex < elementCount; elemIndex++)
                        data[elemIndex] = random.Next(minValue, maxValue);

                    //string dataStr = string.Join(", ", data);
                    //Debug.Print(dataStr);

                    MaxSubarrayResult resultBruteForce = RunWithSpeedDetails(SolveBruteForce, data, "brute force");
                    MaxSubarrayResult resultRecursive = RunWithSpeedDetails(SolveRecursive, data, "recursive");
                    MaxSubarrayResult resultLinear = RunWithSpeedDetails(SolveLinear, data, "linear");

                    Debug.Assert(resultRecursive.Equals(resultLinear));
                    Debug.Assert(resultRecursive.Equals(resultBruteForce));

                    string file = @"C:\Temp\MaxSubarray.csv";
                    string fieldDelim = ";";
                    string line = $"{data.Length}{fieldDelim}{resultBruteForce.TimeElapsed}{fieldDelim}{resultRecursive.TimeElapsed}{fieldDelim}{resultLinear.TimeElapsed}";

                    if (!File.Exists(file))
                        File.AppendAllLines(file, new[] { $"Elems{fieldDelim}Brute force{fieldDelim}Recursive{fieldDelim}Linear" });

                    File.AppendAllLines(file, new[] { line });

                    Debug.Print("");
                }
            }
        }

        static MaxSubarrayResult RunWithSpeedDetails(Func<int[], MaxSubarrayResult> func, int[] data, string funcName)
        {
            Stopwatch sw = Stopwatch.StartNew();

            MaxSubarrayResult result = func(data);

            result.TimeElapsed = sw.ElapsedTicks;

            string text = $"max: {result.MaxSum}, {result.StartIndex} - {result.EndIndex}";
            //Debug.Print(text);
            Debug.Print($"{data.Length} items {funcName}: {result.TimeElapsed} ticks");

            return result;
        }
    }

    public class MaxSubarrayResult
    {
        public int? MaxSum { get; set; }
        public int? StartIndex { get; set; }
        public int? EndIndex { get; set; }
        // for performance testing
        public long? TimeElapsed { get; set; }

        public override bool Equals(object other)
        {
            MaxSubarrayResult otherResult = other as MaxSubarrayResult;

            if (otherResult == null)
                return false;

            return this.MaxSum == otherResult.MaxSum;
        }

        public override int GetHashCode() {
            return MaxSum ?? 0;
        }
    }
}
