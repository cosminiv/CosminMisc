using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_033
    {
        public static int Solve()
        {
            int numerator = 1;
            int denominator = 1;
            int tries = 0;

            for (int a = 10; a < 100; a++)
            {
                for (int b = a + 1; b < 100; b++)
                {
                    tries++;
                    int commonDigit = GetCommonDigit(a, b);

                    if (commonDigit > 0 && (double)a / b == (double)RemoveDigit(a, commonDigit) / RemoveDigit(b, commonDigit))
                    {
                        numerator *= a;
                        denominator *= b;
                    }
                }
            }

            int result = (int)((double)denominator / numerator);
            return result;
        }

        private static double RemoveDigit(int a, int digit)
        {
            int[] digitsA = { a % 10, (a / 10) % 10 };
            if (digitsA[0] == digit)
                return digitsA[1];
            else return digitsA[0];
        }

        private static int GetCommonDigit(int a, int b)
        {
            int[] digitsA = { a % 10, (a / 10) % 10 };
            int[] digitsB = { b % 10, (b / 10) % 10 };

            if (digitsA[0] != 0 && (digitsA[0] == digitsB[0] || digitsA[0] == digitsB[1]))
                return digitsA[0];
            if (digitsA[1] != 0 && (digitsA[1] == digitsB[0] || digitsA[1] == digitsB[1]))
                return digitsA[1];
            return -1;
        }
    }
}
