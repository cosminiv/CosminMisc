using System.Collections.Generic;
using ConsoleApp1.Eu._Common;

namespace ConsoleApp1.Eu._001_050
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
