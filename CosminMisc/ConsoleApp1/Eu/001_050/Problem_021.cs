using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_021
    {
        public static long Solve()
        {
            HashSet<long> primes = _Common.Tools.GetPrimes(8000);
            HashSet<long> amicable = new HashSet<long>();

            for (int i = 1; i <= 10000; i++)
            {
                var divisors = _Common.Tools.GetDivisors(i, primes).OrderBy(d => d).Where(d => d < i);
                long properDivisorSum = divisors.Sum();

                var divisors2 = _Common.Tools.GetDivisors(properDivisorSum, primes).OrderBy(d => d).Where(d => d < properDivisorSum);
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
