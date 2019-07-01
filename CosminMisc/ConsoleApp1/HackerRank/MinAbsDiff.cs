using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HackerRank
{
    class MinAbsDiff
    {
        static int minimumAbsoluteDifference(int[] arr) {
            int minDiff = int.MaxValue;
            Array.Sort(arr);

            for (int i = 0; i < arr.Length - 1; i++) {
                int diff = arr[i + 1] - arr[i];
                if (diff < minDiff)
                    minDiff = diff;
            }

            return minDiff;
        }
    }
}
