using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet._051_100
{
    class Leet_080
    {
        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0) return 0;

            int readIndex = 1, writeIndex = 0;
            int numberRead = nums[0], countRead = 1;
            int result = 1;

            while (readIndex < nums.Length)
            {
                if (nums[readIndex] != numberRead)
                {
                    numberRead = nums[readIndex];
                    countRead = 1;
                }
                else
                {
                    countRead++;

                    if (countRead > 2)
                        writeIndex++;
                }

                readIndex++;
            }

            return result;
        }
    }
}
