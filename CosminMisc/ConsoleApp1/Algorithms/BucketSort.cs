using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Algorithms
{
    class BucketSort
    {
        public int[] Sort(int[] data, int max) {
            int bucketCount = max + 1;
            int[] buckets = new int[bucketCount];
            InsertionSort insertionSort = new InsertionSort();

            // Distribute numbers in buckets. Each bucket contains how many times that number appeared in the input array.
            int bucketIndex = 0;
            for (int i = 0; i < data.Length; i++) {
                int n = data[i];
                bucketIndex = n;
                buckets[bucketIndex]++;
            }

            // Iterate through the buckets in order to produce the sorted output
            int[] result = new int[data.Length];
            int resultIndex = 0;

            for (int i = 0; i < buckets.Length; i++) {
                for (int j = buckets[i]; j > 0; j--) {
                    result[resultIndex] = i;
                    //if (resultIndex > 0)
                    //    Debug.Assert(result[resultIndex] >= result[resultIndex-1]);
                    resultIndex++;
                }
            }

            return result;
        }
    }
}
