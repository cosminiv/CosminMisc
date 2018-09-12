using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_012
    {
        public static long Solve()
        {
            long triangle = 0;

            for (long i = 1; ; i++)
            {
                triangle += i;
                int divisors = Tools.GetDivisorCount(triangle);

                if (divisors > 400)
                    Debug.Print($"400 - {triangle.ToString().Length} digits");
                else if (divisors > 300)
                    Debug.Print($"300 - {triangle.ToString().Length} digits");
                else if (divisors > 200)
                    Debug.Print($"200 - {triangle.ToString().Length} digits");


                if (divisors > 500)
                    return triangle;
            }
        }
    }
}
