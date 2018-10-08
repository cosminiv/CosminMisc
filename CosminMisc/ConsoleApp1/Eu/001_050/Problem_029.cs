using ConsoleApp1.Eu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_029
    {
        public static int Solve()
        {
            List<LargeNumber> numbers = new List<LargeNumber>();

            for (int a = 2; a <= 100; a++)
            {
                LargeNumber n = a;
                for (int b = 2; b <= 100; b++)
                {
                    n = n * a;
                    numbers.Add(n);
                }
            }

            int result = new HashSet<LargeNumber>(numbers).Count;
            return result;
        }
    }
}
