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

            ConsoleApp1.GCJ.Common.SolveTestCases(new Year_2017_1A_A().Solve,
                @"C:\Temp\GCJ - 2017\1A\A-small-practice.in",
                @"C:\Temp\GCJ - 2017\1A\A-small-practice.out");

            var duration = sw.ElapsedMilliseconds;

            Console.WriteLine();
            Console.WriteLine($"Result: {a}");
            Console.WriteLine($"Took {duration}ms");
            Console.ReadLine();
            Debug.Print($"Took {duration}ms");
        }
    }
}
