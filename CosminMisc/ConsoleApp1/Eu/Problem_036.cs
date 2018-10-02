using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_036
    {
        static readonly int[] _digits = new int[25];

        public static long Solve()
        {
            long sum = 0;

            for (int i = 1; i < 1000000; i++)
                if (IsPalindrome(i, 10) && IsPalindrome(i, 2))
                    sum += i;

            return sum;
        }

        private static bool IsPalindrome(int n, int @base)
        {
            int j = 0;

            for (int i = n; i > 0; i = i / @base)
                _digits[j++] = i % @base;

            for (int i = 0; i < j / 2; i++)
                if (_digits[i] != _digits[j - i - 1])
                    return false;

            return true;
        }
    }
}
