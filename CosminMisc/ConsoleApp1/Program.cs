using ConsoleApp1.Algorithms;
using ConsoleApp1.HackerRank;
using ConsoleApp1.Leet;
using ConsoleApp1.Misc;
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

            new MagicSquare().Solve();

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
