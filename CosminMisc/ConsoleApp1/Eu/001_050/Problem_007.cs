namespace ConsoleApp1.Eu._001_050
{
    public class Problem_007
    {
        public static long Solve()
        {
            int target = 10001;
            int primesFound = 1; // found 2 :)
 
            for (long i = 3; ; i += 2)
            {
                if (_Common.Tools.IsPrime(i))
                    primesFound++;

                if (primesFound == target)
                    return i;
            }
        }
    }
}
