namespace ConsoleApp1.Eu._001_050
{
    public class Problem_036
    {
        public static long Solve()
        {
            long sum = 0;

            for (int i = 1; i < 1000000; i++)
                if (_Common.Tools.IsPalindrome(i, 10) && _Common.Tools.IsPalindrome(i, 2))
                    sum += i;

            return sum;
        }
    }
}
