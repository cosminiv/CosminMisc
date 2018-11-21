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
        public int[] Sort(int[] data, int min, int max) {
            int bucketCount = data.Length;
            List.Node[] buckets = new List.Node[bucketCount];
            InsertionSort insertionSort = new InsertionSort();

            // Distribute numbers in buckets
            int bucketIndex = 0;
            for (int i = 0; i < data.Length; i++) {
                int n = data[i];
                List.Node newNode = new List.Node(n, null);
                bucketIndex = (int)((long)bucketCount * (n - min) / (max - min));
                List.Node bucket = buckets[bucketIndex];

                if (bucket == null)
                    buckets[bucketIndex] = newNode;
                else {
                    List.Node node;
                    
                    // Insert in sorted bucket
                    if (bucket.Value >= n) {
                        newNode.Next = bucket;
                        buckets[bucketIndex] = newNode;
                        continue;
                    }

                    List.Node prev = null;
                    for (node = bucket; node != null && node.Value < n; node = node.Next) {
                        prev = node;
                    }

                    List.Node next = prev.Next;
                    prev.Next = newNode;
                    newNode.Next = next;
                }
            }

            // Iterate through the buckets in order to produce the sorted output
            int[] result = new int[data.Length];
            int resultIndex = 0;

            for (int i = 0; i < buckets.Length; i++) {
                for (List.Node node = buckets[i]; node != null; node = node.Next) {
                    result[resultIndex] = node.Value;
                    //if (resultIndex > 0)
                    //    Debug.Assert(result[resultIndex] >= result[resultIndex-1]);
                    resultIndex++;
                }
            }

            return result;
        }
    }
}
