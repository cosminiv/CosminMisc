using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    class Problem_004
    {
        public static int Solve()
        {
            int max = 9999;
            int min = 1000;

            int result = SolveBruteForce(min, max);
            return result;

            
            int count = max - min + 1;
            int prevProduct = 0;

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Debug.Print($"{i} {j}");

                    int n1 = max - i;
                    int n2 = max - j;
                    int n = n1 * n2;

                    if (prevProduct > 0)
                        Debug.Assert(n < prevProduct);

                    prevProduct = n;

                    if (IsPalindrome(n))
                        return n;
                }
            }

            return 0;
        }

        static int SolveGeneratePalindromesFirst(int min, int max)
        {
            int minProduct = min * min;
            int maxProduct = max * max;
            int minProductDigits = CountDigits(minProduct);
            int maxProductDigits = CountDigits(maxProduct);

            for (int i = minProductDigits; i <= maxProductDigits; i++)
            {
                List<int> palindromes = GeneratePalindromes(i);
            }

            return -1;
        }

        private static List<int> GeneratePalindromes(int digits)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < digits / 2; i++)
            {
                for (int digit = 9; digit >= 0; digit--)
                {
                    if (i == 0 && digit == 0)
                        continue;

                    //int n = digit
                }
            }

            return result;
        }

        private static int CountDigits(int n)
        {
            int result = 0;

            for (int i = n; i > 0; i = i / 10)
                result++;

            return result;
        }

        private static int SolveBruteForce(int min, int max)
        {
            int count = max - min + 1;
            int maxPalindrome = 0;

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    int n1 = max - i;
                    int n2 = max - j;
                    int n = n1 * n2;

                    if (IsPalindrome(n) && n > maxPalindrome)
                        maxPalindrome = n;
                }
            }

            return maxPalindrome;
        }

        private static bool IsPalindrome(int n)
        {
            List<int> digits = new List<int>();

            for (int i = n; i > 0; i = i / 10)
            {
                digits.Insert(0, i % 10);
            }

            for (int i = 0; i < digits.Count / 2; i++)
            {
                if (digits[i] != digits[digits.Count - i - 1])
                    return false;
            }

            return true;
        }
    }
}
