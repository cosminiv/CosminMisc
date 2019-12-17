using System.Diagnostics;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_010
    {
        public static long Solve()
        {
            int max = (int)2E+6 - 1;
            long sum = 2;
            int a = 0, b = 0, c = 0;

            for (int i = 3; i <= max; i += 2)
            {
                if (_Common.Tools.IsPrime(i))
                {
                    sum += i;

                    a = b;
                    b = c;
                    c = i;
                }
            }

            Debug.Print($"Last primes: {a}, {b}, {c}");
            return sum;
        }
    }
}
