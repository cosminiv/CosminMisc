using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_010
    {
        public static long Solve()
        {
            int max = (int)2E+6 - 1;
            long sum = 2;
            int a = 0, b = 0, c = 0;

            for (int i = 3; i <= max; i += 2)
            {
                if (Common.IsOddNumberPrime(i))
                {
                    sum += i;

                    a = b;
                    b = c;
                    c = i;
                }
            }

            Debug.Print($"Last primes: {a}, {b}, {c}");
            return sum;
        }
    }
}
