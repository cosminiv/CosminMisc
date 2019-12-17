using System;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_043
    {
        // TODO: Takes 3.3s, perhaps make it faster
        public static long Solve()
        {
            var numbers = Problem_041.GeneratePermutations(1234567890);
            long sum = 0;

            foreach (var n in numbers)
            {
                long digit3 = GetSubNumber(n, 3, 1);
                long digit5 = GetSubNumber(n, 5, 1);

                if (n >= 1000000000 &&
                    digit3 % 2 == 0 &&
                    GetSubNumber(n, 2, 3) % 3 == 0 &&
                    digit5 % 5 == 0 &&
                    GetSubNumber(n, 4, 3) % 7 == 0 &&
                    GetSubNumber(n, 5, 3) % 11 == 0 &&
                    GetSubNumber(n, 6, 3) % 13 == 0 &&
                    GetSubNumber(n, 7, 3) % 17 == 0)
                {
                    sum += n;
                }
            }

            return sum;
        }

        static long GetSubNumber(long n, int pos, int howMany)
        {
            long result = 0;
            int digits = _Common.Tools.GetDigitCount(n);
            n = (long)(n % Math.Pow(10, digits - pos));
            digits -= pos;

            for (int i = 0; i < howMany; i++)
            {
                long multOfTen = (long)Math.Pow(10, digits - i - 1);
                long digit = n / multOfTen;
                result += (long)(digit * Math.Pow(10, howMany - i - 1));
                n = n % multOfTen;
            }

            return result;
        }
    }
}
