using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Algorithms
{
    class MergeSort
    {
        public static void Sort(int[] data, out int inversions)
        {
            inversions = 0;
            Sort(data, 0, data.Length - 1, ref inversions);
        }

        private static void Sort(int[] data, int indexStart, int indexEnd, ref int inversionCount)
        {
            // Base case - recursion end
            if (indexEnd - indexStart < 1)
                return;

            // Divide and conquer
            int length = indexEnd - indexStart + 1;
            Sort(data, indexStart, indexStart + length / 2 - 1, ref inversionCount);
            Sort(data, indexStart + length / 2, indexEnd, ref inversionCount);

            // Merge
            MergeSortedData(data, indexStart, indexStart + length / 2 - 1, indexEnd, ref inversionCount);
        }

        static void DisplayArray(string text, int[] arr)
        {
            string arrStr = String.Join(", ", arr);
            Debug.Print(text + arrStr);
        }

        private static void MergeSortedData(int[] data, int indexStart, int mid, int indexEnd, ref int inversionCount)
        {
            int i = indexStart;
            int j = mid + 1;
            List<int> sortedData = new List<int>();

            while (i <= mid && j <= indexEnd)
            {
                bool isInversion = data[i] > data[j];
                if (isInversion)
                {
                    sortedData.Add(data[j]);
                    int inversionIncrement = mid - i + 1;
                    Debug.Assert(inversionIncrement > 0);
                    inversionCount += inversionIncrement;
                    j++;
                }
                else
                { 
                    sortedData.Add(data[i]);
                    i++;
                }
            }

            // Add the remaining data in the two sub-arrays
            while (i <= mid)
            {
                sortedData.Add(data[i]);
                i++;
            }

            while (j <= indexEnd)
            {
                sortedData.Add(data[j]);
                j++;
            }

            // Update initial array
            for (int k = 0; k < sortedData.Count; k++)
                data[indexStart + k] = sortedData[k];
        }
    }
}
