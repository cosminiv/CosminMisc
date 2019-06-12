using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HackerRank
{
    class ArrayRotation
    {
        static int[] rotLeft(int[] a, int d) {
            if (d == a.Length)
                return a;

            int[] res = new int[a.Length];

            for (int i = 0; i < a.Length; i++) {
                int i2 = i - d;
                if (i2 < 0)
                    i2 = a.Length + i2;
                res[i2] = a[i];
            }

            return res;
        }
    }
}
