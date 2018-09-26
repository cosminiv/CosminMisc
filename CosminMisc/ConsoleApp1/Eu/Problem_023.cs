using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_023
    {
        public static long Solve()
        {
            int max = 28123;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            HashSet<long> primes = Tools.GetPrimes(10000);
            List<long> abundantList = new List<long>();
            var duration1 = sw.ElapsedMilliseconds;

            for (int i = 1; i <= max; i++)
            {
                var divisors = Tools.GetDivisors(i, primes).Where(d => d < i);

                if (divisors.Sum() > i)
                {
                    abundantList.Add(i);
                    //Debug.Print($"{i}: {string.Join(", ", divisors)}: {divisors.Sum()}");
                }
            }

            var duration2 = sw.ElapsedMilliseconds - duration1;

            
            bool[] abundantSumsArr = new bool[abundantList.Count * abundantList.Count];
            long ops = 0;
            long addOps = 0;

            for (int i = 0; i < abundantList.Count; i++)
            {
                bool addedSum = false;

                for (int j = i; j < abundantList.Count; j++)
                {
                    ops++;
                    long sum = abundantList[i] + abundantList[j];
                    if (sum <= max)
                    {
                        abundantSumsArr[sum] = true;
                        addedSum = true;
                        addOps++;
                    }
                    else
                        break;
                }

                if (!addedSum)
                    break;
            }

            var duration3 = sw.ElapsedMilliseconds - duration2;

            long result = 0;

            for (int i = 1; i <= max; i++)
            {
                if (!abundantSumsArr[i])
                    result += i;
            }

            var duration4 = sw.ElapsedMilliseconds - duration3;

            var durationTotal = sw.ElapsedMilliseconds;

            return result;
        }
    }
}
