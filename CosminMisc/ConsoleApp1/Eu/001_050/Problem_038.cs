using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp1.Eu
{
    public class Problem_038
    {
        static int[] _digitsFoundCounts = new int[10];
        static int _digitsFoundCount = 0;
        static int[] _digitsFound = new int[9];

        public static long Solve()
        {
            long max = 0;

            for (long n = 2; n < 100000; n++)
            {
                ResetDigitsFound();

                for (int mult = 1; ; mult++)
                {
                    long prod = n * mult;
                    ParseDigitsResult result = ParseDigits(prod);
                    if (result == ParseDigitsResult.FoundZeroOrRepeat) break;
                    if (_digitsFoundCount == 9)
                    {
                        long x = MakeNumberFromDigits(_digitsFound);
                        if (x > max)
                        {
                            max = x;
                            Debug.Print($"n = {n}, mult = {mult}");
                        }
                    }
                }
            }

            return max;
        }

        static long MakeNumberFromDigits(int[] digits)
        {
            long result = 0;
            long multipleOfTen = 1;
            for (int i = 0; i < digits.Length; i++)
            {
                long toAdd = multipleOfTen * digits[digits.Length - 1 - i];
                result += toAdd;
                multipleOfTen *= 10;
            }
            return result;
        }

        private static void ResetDigitsFound()
        {
            Array.Clear(_digitsFoundCounts, 0, _digitsFoundCounts.Length);
            Array.Clear(_digitsFound, 0, _digitsFound.Length);
            _digitsFoundCount = 0;
        }

        private static ParseDigitsResult ParseDigits(long n)
        {
            if (n == 0) return ParseDigitsResult.FoundZeroOrRepeat;

            int multipleOfTen = 1;

            // Update digit counts
            for (long n2 = n; n2 > 0; n2 = n2 / 10)
            {
                int digit = (int)(n2 % 10);
                if (digit == 0) return ParseDigitsResult.FoundZeroOrRepeat;
                if (++_digitsFoundCounts[digit] > 1) return ParseDigitsResult.FoundZeroOrRepeat;
                multipleOfTen *= 10;
            }

            // Add the actual digits
            long n3 = n;
            for (int j = multipleOfTen / 10; n3 > 0; j = j / 10)
            {
                int digit = (int)(n3 / j);
                _digitsFound[_digitsFoundCount++] = digit;
                n3 -= j * digit;
            }

            return ParseDigitsResult.Ok;
        }

        enum ParseDigitsResult
        {
            Ok = 0,
            FoundZeroOrRepeat = 2
        }
    }
}
