using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HackerRank
{
    // Given a list of integers arr, and a single integer, create an array of length k from elements 
    // of arr such that its unfairness is minimized.
    // Unfairness = max(subarr) - min(subarr)
    class MaxMin
    {
        static int maxMin(int k, int[] arr) {
            Array.Sort(arr);
            int minUnfairness = int.MaxValue;

            for (int i = 0; i < arr.Length - k + 1; i++) {
                int min = arr[i];
                int max = arr[i + k - 1];
                int unfairness = max - min;

                if (unfairness < minUnfairness)
                    minUnfairness = unfairness;
            }

            return minUnfairness;
        }
    }
}
