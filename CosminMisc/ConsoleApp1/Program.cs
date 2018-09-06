using ConsoleApp1.Algorithms;
using ConsoleApp1.Eu;
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

            var p = Problem_004.Solve();

            var duration = sw.ElapsedMilliseconds;
            Debug.Print($"Took {duration}ms");
        }
    }
}
