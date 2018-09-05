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
            long a = Problem_004.Solve();
            var duration = sw.ElapsedMilliseconds;
        }

        private static List<int> GeneratePalindromes(int digits)
        {
            List<int> result = new List<int>();

            for (int digit = 9; digit >= 0; digit--)
            {
                for (int i = 0; i < digits / 2; i++)
                {
                    if (i == 0 && digit == 0)
                        continue;

                    //Debug.Print($"{}{}");
                }
            }

            return result;
        }

    }
}
