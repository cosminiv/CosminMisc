using ConsoleApp1.Algorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            int min = 0;
            int max = (int)1E+7;
            int howMany = max;
            int[] numbers = MakeRandomNumbers(min, max, howMany);

            //int[] numbers = { 7, 1, 2, 9, 5, 0, 4, 6, 15, 8};
            //howMany = numbers.Length;
            //min = numbers.Min();
            //max = numbers.Max() + 1;

            sw.Restart();
            int[] bucketSortResult = new BucketSort().Sort(numbers, max);
            long timeBucketSort = sw.ElapsedMilliseconds;

            sw.Restart();
            new QuickSort().Sort(numbers);  // sort in-place
            long timeQuickSort = sw.ElapsedMilliseconds;

            Console.WriteLine($"QuickSort: {timeQuickSort}ms");
            Console.WriteLine($"BucketSort: {timeBucketSort}ms");

            Debug.Assert(bucketSortResult.Length == howMany);
            Debug.Assert(numbers.Length == howMany);
            VerifyNumbersAreSorted(bucketSortResult);
            VerifyNumbersAreSorted(numbers);

            long duration = sw.ElapsedMilliseconds;
            Console.WriteLine();
            //Console.WriteLine($"Result: {a}");
            Console.WriteLine($"Took {duration}ms");
            Console.ReadLine();
            Debug.Print($"Took {duration}ms");
        }

        private static void VerifyNumbersAreSorted(int[] arr) {
            for (int i = 1; i < arr.Length; i++) {
                if (arr[i] < arr[i - 1])
                    Debug.Assert(false);
            }
        }

        private static int[] MakeRandomNumbers(int min, int max, int howMany) {
            int[] result = new int[howMany];
            Random rnd = new Random();
            for (int i = 0; i < howMany; i++) {
                result[i] = rnd.Next(max);
            }
            return result;
        }
    }
}
