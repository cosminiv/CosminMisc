using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp1.Eu._001_050
{
    class Problem_004
    {
        public static long Solve()
        {
            int min = 100; //100000;
            int max = 999; //999999;
            
            long result = SolveGeneratePalindromesFirst(min, max);
            
            return result;
        }

        static long SolveGeneratePalindromesFirst(long min, long max)
        {
            Debug.Assert(CountDigits(min) == CountDigits(max));

            long minProduct = min * min;
            long maxProduct = max * max;
            int minProductDigits = CountDigits(minProduct);
            int maxProductDigits = CountDigits(maxProduct);
            int factorsDigits = CountDigits(min);
            int iter = 1;

            for (int i = maxProductDigits; i >= minProductDigits; i--)
            {
                IEnumerable<long> palindromes = GeneratePalindromes(i);
                long prevPal = -1;

                foreach (long pal in palindromes)
                {
                    if (prevPal > 0)
                        Debug.Assert(pal < prevPal);

                    bool canFactorize = TryFactorize(pal, min, max, out Tuple<long, long> factors);

                    if (canFactorize)
                    {
                        Debug.Print($"{iter} iterations");
                        Debug.Print($"{pal} = {factors.Item1} x {factors.Item2}");
                        return pal;
                    }

                    prevPal = pal;
                    iter++;
                }
            }

            return -1;
        }

        private static bool TryFactorize(long n, long minFactor, long maxFactor, out Tuple<long, long> factors)
        {
            for (long factor1 = maxFactor; factor1 >= minFactor; factor1--)
            {
                long factor2 = n / factor1;
                long r = n % factor1;

                if (r == 0 && factor2 >= minFactor && factor2 <= maxFactor)
                {
                    factors = new Tuple<long, long>(factor2, factor1);
                    return true;
                }
            }

            factors = new Tuple<long, long>(0, 0);
            return false;
        }

        static IEnumerable<long> GeneratePalindromes(int digits)
        {
            long maxNumberHalf = 1;

            for (long i = 1; i <= digits / 2; i++)
            {
                maxNumberHalf *= 10;
            }

            long minNumberHalf = maxNumberHalf / 10;
            maxNumberHalf--;

            if (digits % 2 == 0)
            { 
                for (long i = maxNumberHalf; i >= minNumberHalf; i--)
                {
                    long mirrorNumber = RevertDigits(i);
                    long palindrome = i * (maxNumberHalf + 1) + mirrorNumber;
                    yield return palindrome;
                }
            }
            else
            {
                for (long i = maxNumberHalf; i >= minNumberHalf; i--)
                {
                    for (int middleDigit = 9; middleDigit >= 0; middleDigit--)
                    {
                        long mirrorNumber = RevertDigits(i);
                        long palindrome =
                            i * (maxNumberHalf + 1) * 10 +
                            middleDigit * (maxNumberHalf + 1) +
                            mirrorNumber;
                        yield return palindrome;
                    }
                }
            }

            //Debug.Print($"Generated {result.Count} palindromes of length {digits}");
            //return result;
        }

        private static long RevertDigits(long n)
        {
            long result = 0;

            for (long i = n; i > 0; i = i / 10)
            {
                int digit = (int)(i % 10);
                result = result * 10 + digit;
            }

            return result;
        }

        private static int CountDigits(long n)
        {
            int result = 0;

            for (long i = n; i > 0; i = i / 10)
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
