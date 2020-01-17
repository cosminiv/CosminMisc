using System.Linq;

namespace ConsoleApp1.Leet
{
    class Leet_075
    {
        public void SortColors(int[] nums)
        {
            int n0 = nums.Count(n => n == 0);
            int n1 = nums.Count(n => n == 1);
            int n2 = nums.Count(n => n == 2);

            for (int i = 0; i < n0; i++)
                nums[i] = 0;

            for (int i = n0; i < n0 + n1; i++)
                nums[i] = 1;

            for (int i = n0 + n1; i < n0 + n1 + n2; i++)
                nums[i] = 2;
        }
    }
}
