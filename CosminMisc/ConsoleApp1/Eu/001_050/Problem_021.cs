using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_021
    {
        public static long Solve()
        {
            HashSet<long> primes = Tools.GetPrimes(8000);
            HashSet<long> amicable = new HashSet<long>();

            for (int i = 1; i <= 10000; i++)
            {
                var divisors = Tools.GetDivisors(i, primes).OrderBy(d => d).Where(d => d < i);
                long properDivisorSum = divisors.Sum();

                var divisors2 = Tools.GetDivisors(properDivisorSum, primes).OrderBy(d => d).Where(d => d < properDivisorSum);
                long sum2 = divisors2.Sum();

                if (sum2 == i && properDivisorSum != i)
                {
                    amicable.Add(i);
                    amicable.Add(sum2);

                    //Debug.Print($"{i}: {String.Join(", ", divisors)}: {properDivisorSum}");
                    //Debug.Print($"{properDivisorSum}: {String.Join(", ", divisors2)}: {sum2}");
                    //Debug.Print("");
                }
            }

            long result = amicable.Sum();
            Debug.Print(result.ToString());

            return result;
        }
    }
}
