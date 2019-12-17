using System.Linq;
using ConsoleApp1.Eu._Common;

namespace ConsoleApp1.Eu._001_050
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
