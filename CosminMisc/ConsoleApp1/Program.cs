using ConsoleApp1.Algorithms;
using ConsoleApp1.AppliedAlgorithms;
using ConsoleApp1.Eu;
using ConsoleApp1.Eu._051_100;
using ConsoleApp1.Eu._Common;
using ConsoleApp1.Eu.Common;
using ConsoleApp1.GCJ;
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
            int a = 0;

            int maxDigits = 4;
            int[] numbers = MakeNumbers(0, (int)Math.Pow(10, maxDigits) - 1, (int)1E+7);
            //int[] numbersCopy = numbers.ToArray();
            int[] numbers1 = new int[0];
            int[] numbers2 = new int[0];

            sw.Restart();

            new QuickSort().Sort(numbers);
            numbers1 = numbers;

            //numbers2 = new RadixSort().Sort(numbers, maxDigits); // running time: d*(n+b)
            
            //new RadixSort().Test();

            var duration = sw.ElapsedMilliseconds;

            VerifyNumbersAreSorted(numbers1);
            VerifyNumbersAreSorted(numbers2);

            Console.WriteLine();
            Console.WriteLine($"Result: {a}");
            Console.WriteLine($"Took {duration}ms");
            //Console.ReadLine();
            Debug.Print($"Took {duration}ms");
        }

        private static void VerifyNumbersAreSorted(int[] arr) {
            for (int i = 1; i < arr.Length; i++) {
                if (arr[i] < arr[i - 1])
                    Debug.Assert(false);
            }
        }

        private static int[] MakeNumbers(int min, int max, int howMany) {
            int[] result = new int[howMany];
            Random rnd = new Random();
            for (int i = 0; i < howMany; i++) {
                result[i] = rnd.Next(max);
            }
            return result;
        }
    }
}
