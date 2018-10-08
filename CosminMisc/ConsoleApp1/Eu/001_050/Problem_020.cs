using ConsoleApp1.Eu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_020
    {
        public static int Solve()
        {
            LargeNumber n = 1;

            for (int i = 2; i <= 100; i++)
                n = n * i;

            int result = n.ToString().Sum(d => int.Parse(d.ToString()));
            return result;
        }
    }
}
