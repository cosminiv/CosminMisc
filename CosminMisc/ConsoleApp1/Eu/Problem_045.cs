using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_045
    {
        public static long Solve()
        {
            long MAX = 1000000;
            HashSet<long> pentSet = new HashSet<long>();
            HashSet<long> hexSet = new HashSet<long>();

            for (long n = 144; n < MAX; n++)
            {
                long t = n * (n + 1) / 2;
                if (hexSet.Contains(t) && pentSet.Contains(t))
                    return t;
                pentSet.Add(n * (3 * n - 1) / 2);
                hexSet.Add(n * (2 * n - 1));
            }

            return 0;
        }
    }
}
