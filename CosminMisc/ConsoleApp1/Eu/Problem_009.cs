using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_009
    {
        static readonly long sum = 300490;

        public static long Solve()
        {
            for (long a = 1; a < sum; a++)
            {
                long b = sum * (2 * a - sum) / (2 * (a - sum));
                long c = sum - a - b;

                if (b <= a || c <= b)
                    continue;

                if (a * a + b * b == c * c)
                {
                    Debug.Print($"{a}, {b}, {c}");
                    return a * b * c;
                }
            }

            return 0;
        }

        private static long SolveNaive()
        {
            for (long a = 1; a < sum; a++)
            {
                for (long b = a + 1; b < sum; b++)
                {
                    long c = sum - a - b;

                    if (a * a + b * b == c * c)
                    {
                        Debug.Print($"{a}, {b}, {c}");
                        return a * b * c;
                    }
                }
            }

            return 0;
        }
    }
}
