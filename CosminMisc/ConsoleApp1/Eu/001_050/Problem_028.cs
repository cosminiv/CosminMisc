namespace ConsoleApp1.Eu._001_050
{
    public class Problem_028
    {
        public static long Solve()
        {
            long sum = 1;

            for (int i = 3; i <= 1001; i+=2)
            {
                long latestN = i * i;
                //sum += latestN + (latestN - (i - 1)) + (latestN - 2 * (i - 1)) + (latestN - 3 * (i - 1));
                sum += 4 * latestN - 6 * (i - 1);
            }

            return sum;
        }
    }
}
