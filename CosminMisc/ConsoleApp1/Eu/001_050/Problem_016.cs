using ConsoleApp1.Eu.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_016
    {
        public static int Solve()
        {
            return SolveWithLargeNumber();
        }

        private static int SolveWithLargeNumber()
        {
            LargeNumber ln = 1;

            for (int exp = 1; exp <= 1000; exp++)
                ln = ln * 2;

            int digitSum = ln.ToString().Sum(c => int.Parse(c.ToString()));

            return digitSum;
        }
    }
}

