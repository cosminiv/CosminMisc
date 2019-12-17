namespace ConsoleApp1.Eu._001_050
{
    public class Problem_001
    {
        public static int Solve()
        {
            int sum = 0;
            int max = 999;

            for (int i = 3; i <= max; i+=3)
                sum += i;

            for (int i = 5; i <= max; i+=5)
                if (i % 3 != 0)     // Otherwise was already added
                    sum += i;

            return sum;
        }
    }
}
