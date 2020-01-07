
namespace ConsoleApp1.Leet
{
    public class Leet_070
    {
        public int ClimbStairs(int n)
        {
            if (n == 1) return 1;

            int n1 = 1, n2 = 2;
            
            for (int i = 3; i <= n; i++)
            {
                int n3 = n1 + n2;
                n1 = n2;
                n2 = n3;
            }

            return n2;
        }
    }
}
