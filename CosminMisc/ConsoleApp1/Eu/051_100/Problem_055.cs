using ConsoleApp1.Eu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_055
    {
        static readonly int MAX_NUMBER = 10000;
        static readonly int MAX_STEPS = 50;
        static readonly int[] _digits = new int[50];

        public static int Solve() {
            int howMany = Enumerable.Range(1, MAX_NUMBER)
                .Count(n => IsLychrelNumber(n));
            return howMany;
        }

        private static bool IsLychrelNumber(int n) {
            LargeNumber n2 = n;
            for (int i = 0; i < MAX_STEPS; i++) {
                string revStr = new string(n2.ToString().Reverse().ToArray());
                n2 = n2 + new LargeNumber(revStr);

                revStr = new string(n2.ToString().Reverse().ToArray());
                if (n2.ToString() == revStr)
                    return false;
            }
            return true;
        }

        private static long Reverse(long n) {
            int j = 0;
            long multOfTen = 1;

            for (long i = n; i > 0; i = i / 10) {
                _digits[j++] = (int)(i % 10);
                multOfTen *= 10;
            }

            long result = 0;
            j = 0;
            for (long i = multOfTen / 10; i > 0; i = i / 10) {
                result += _digits[j++] * i;
            }

            return result;
        }
    }
}
