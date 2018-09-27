using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_030
    {
        public static long Solve()
        {
            long result = 0;

            for (int i = 10; i < 1000000; i++)
            {
                int digitPowerSum = 0;

                for (int j = i; j > 0; j = j / 10)
                {
                    int d = j % 10;
                    digitPowerSum += d * d * d * d * d;
                }

                if (digitPowerSum == i)
                {
                    Debug.Print(i.ToString());
                    result += i;
                }
            }

            return result;
        }
    }
}
