using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_007
    {
        public static long Solve()
        {
            int target = 10001;
            int primesFound = 1; // found 2 :)
 
            for (long i = 3; ; i += 2)
            {
                if (Tools.IsPrime(i))
                    primesFound++;

                if (primesFound == target)
                    return i;
            }
        }
    }
}
