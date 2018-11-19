using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Algorithms
{
    class QuickSort
    {
        // PARTITION: the pivot is 11
        // =====================================================================================
        // 13 19 9  5  12 8  7  4  21 2  6  11        j = 0, i = -1     13 > 11, don't swap
        // 13 19 9  5  12 8  7  4  21 2  6  11        j = 1, i = -1     19 > 11, don't swap
        // 9  19 13 5  12 8  7  4  21 2  6  11        j = 2, i = 0      9 < 11 => i=0, swap 9 with 13
        // 9  5  13 19 12 8  7  4  21 2  6  11        j = 3, i = 1      5 < 11 => i=1, swap 5 with 19
        // 9  5  13 19 12 8  7  4  21 2  6  11        j = 4, i = 1      12 > 11, don't swap
        // 9  5  8  19 12 13 7  4  21 2  6  11        j = 5, i = 2      8 < 11 => i=2, swap 8 with 13   
        // 9  5  8  7  12 13 19 4  21 2  6  11        j = 6, 
        // 9  5  8  7  4  13 19 12 21 2  6  11        j = 7, 
        // 9  5  8  7  4  13 19 12 21 2  6  11        j = 8, 
        // 9  5  8  7  4  2  19 12 21 13 6  11        j = 9, 
        // 9  5  8  7  4  2  6  12 21 13 19 11        j = 10,
        // 9  5  8  7  4  2  6  11 21 13 19 12        j = 11,           finally swap the pivot 11 with 12
        // RETURN index of pivot 11: 7 

        public void Test() {
            int[] data = { 13, 19, 9, 5, 12, 8, 7, 4, 21, 2, 6, 11 };
            Sort(data);

            string dataStr = string.Join(", ", data);
        }

        public void Sort(int[] data) {
            Sort(data, 0, data.Length - 1);
        }

        void Sort(int[] data, int startIndex, int endIndex) {
            if (endIndex > startIndex) {
                int pivotIndex = Partition(data, startIndex, endIndex);
                Sort(data, startIndex, pivotIndex - 1);
                Sort(data, pivotIndex + 1, endIndex);
            }
        }

        private int Partition(int[] data, int startIndex, int endIndex) {
            int swapIdx = startIndex - 1;
            int pivot = data[endIndex];
            int runningIdx;

            for (runningIdx = startIndex; runningIdx < endIndex; runningIdx++) {
                int n = data[runningIdx];
                if (n <= pivot) {
                    swapIdx++;
                    Swap(data, swapIdx, runningIdx);
                }
            }

            Swap(data, swapIdx + 1, runningIdx);
            return swapIdx + 1;
        }

        void Swap(int[] data, int index1, int index2) {
            int temp = data[index1];
            data[index1] = data[index2];
            data[index2] = temp;
        }
    }
}
