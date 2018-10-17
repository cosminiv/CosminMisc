using ConsoleApp1.Algorithms;
using ConsoleApp1.Eu;
using ConsoleApp1.Eu._051_100;
using ConsoleApp1.Eu._Common;
using ConsoleApp1.Eu.Common;
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
            //long a = 0;

            Primes primes = new Primes((long)1E+9);
            var a = primes.ToList().Count;

            var duration = sw.ElapsedMilliseconds;

            Console.WriteLine();
            Console.WriteLine($"Result: {a}");
            Console.WriteLine($"Took {duration}ms");
            Console.ReadLine();
            Debug.Print($"Took {duration}ms");
        }
    }
}
