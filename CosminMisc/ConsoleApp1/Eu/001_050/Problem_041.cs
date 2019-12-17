using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_041
    {
        static byte[] _digitsFound = new byte[10];

        public static long Solve()
        {
            // Sum of digits up to 9 and 8 is divisible by 3, so skip.
            long start = 7654321;
            int digitCount = 7;

            for (long digits = start; digitCount > 0; digits = (int)(digits % Math.Pow(10, digitCount)))
            {
                List<long> perm = GeneratePermutations(digits);

                foreach (long a in perm)
                    if (_Common.Tools.IsPrime(a))
                        return a;

                digitCount--;
            }

            return 0;
        }

        public static List<long> GeneratePermutations(long n)
        {
            List<long> result = new List<long>();
            if (n < 10) { result.Add(n); goto end; }

            int digits = _Common.Tools.GetDigitCount(n);
            result = new List<long>(digits * (digits - 1));
            int firstDigit = (int)(n / (long)Math.Pow(10, digits - 1));
            long restOfDigits = n % (long)Math.Pow(10, digits - 1);
            List<long> smallerPermutations = GeneratePermutations(restOfDigits);

            for (int j = 0; j <= digits - 1; j++) {
                for (long i = 0; i < smallerPermutations.Count; i++) {
                    long prevPerm = smallerPermutations.ElementAt((int)i);
                    long newPerm = InsertDigit(prevPerm, digits - 1, firstDigit, j);
                    result.Add(newPerm);
                }
            }

            end: return result;
        }

        private static long InsertDigit(long number, int numberLength, int digit, int pos)
        {
            long multOfTen = (long)Math.Pow(10, numberLength - pos);

            long a = number / multOfTen * multOfTen * 10;
            long b = digit * multOfTen;
            long c = number % multOfTen;

            long result = a + b + c;
            //Debug.Print($"{a} + {b} + {c} = {newNumber}");

            return result;
        }
    }
}
