using ConsoleApp1.Algorithms;
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

            string input =
@"1
96 1 94 47 0 1
";
//@"4
//11 5 16 5 0 0
//3 1 3 2 2 0
//3 1 3 2 1 0
//2 1 5 1 1 1";

            IEnumerable<string> output = new Year_2017_1A_C().Solve(input.Split('\n'));
            foreach (string line in output) {
                Console.WriteLine(line);
            }

            //new Year_2017_1A_C().Solve(
            //    @"C:\Temp\GCJ - 2017\1A\C-small-practice.in",
            //    @"C:\Temp\GCJ - 2017\1A\C-small-practice.out"
            //);

            var duration = sw.ElapsedMilliseconds;

            Console.WriteLine();
            Console.WriteLine($"Result: {a}");
            Console.WriteLine($"Took {duration}ms");
            Console.ReadLine();
            Debug.Print($"Took {duration}ms");
        }
    }
}
