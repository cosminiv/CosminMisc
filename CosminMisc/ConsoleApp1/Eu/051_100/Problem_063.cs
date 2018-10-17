using ConsoleApp1.Eu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_063
    {
        // Used an Excel sheet in addition to this.
        public static int Solve() {
            int result = 0;
            int pow = 11;
            LargeNumber x = LargeNumber.Power(9, pow - 1);

            for (; ; pow++) {
                x = x * 9;
                int dc = x.DigitCount;

                if (dc != pow) break;
                if (dc == pow) result++;
            }

            return result;
        }
    }
}
