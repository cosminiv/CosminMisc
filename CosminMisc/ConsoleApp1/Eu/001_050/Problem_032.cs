using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_032
    {
        static byte[] _digitsFound = new byte[10];
        static long tries = 0;

        public static int Solve()
        {
            bool shouldQuit = false;
            HashSet<int> products = new HashSet<int>();

            for (int a = 2; shouldQuit == false; a++)
            {
                for (int b = a + 1; ; b++)
                {
                    tries++;

                    int n = a * b;
                    int digitCount = DigitCount(a) + DigitCount(b) + DigitCount(n);

                    if (digitCount > 9)
                    {
                        if (b == a + 1) { shouldQuit = true; }  // The smallest b is already too big; no point going higher
                        break;
                    }

                    if (digitCount == 9 && ArePandigital(a, b, n))
                    {
                        products.Add(n);
                        Debug.Print($"{a} x {b} = {n}");
                    }
                }
            }

            return products.Sum();
        }

        private static bool ArePandigital(int a, int b, int n)
        {
            Array.Clear(_digitsFound, 0, _digitsFound.Length);

            foreach (int x in new[] { a, b, n })
            {
                for (int i = x; i > 0; i = i / 10)
                {
                    int d = i % 10;
                    if (d == 0 || ++_digitsFound[d] > 1) return false;
                }
            }
            
            return true;
        }

        public static int DigitCount(long a)
        {
            return (int)Math.Ceiling(Math.Log10(a));
        }
    }
}
