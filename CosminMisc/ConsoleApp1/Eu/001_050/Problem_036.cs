using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_036
    {
        public static long Solve()
        {
            long sum = 0;

            for (int i = 1; i < 1000000; i++)
                if (Tools.IsPalindrome(i, 10) && Tools.IsPalindrome(i, 2))
                    sum += i;

            return sum;
        }
    }
}
