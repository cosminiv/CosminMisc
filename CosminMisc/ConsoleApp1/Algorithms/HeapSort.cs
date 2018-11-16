using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Algorithms
{
    class HeapSort
    {
        public void Test() {
            int[] data = { 3, 21, 6, 9, 2, 17, 5, 0 };
            Sort(data);

            //MaxHeapify(data, data.Length, 1);
            //BuildMaxHeap(data);

            string dataStr = string.Join(", ", data);
        }

        public void Sort(int[] data) {
            BuildMaxHeap(data);
            int heapSize = data.Length;

            while (heapSize > 1) {
                Swap(data, 0, heapSize - 1);    // Swap first (max) element with last
                heapSize--;                     // Decrease the size of heap we are examining
                MaxHeapify(data, heapSize, 0);  // Maintain max-heap property
            }
        }

        void BuildMaxHeap(int[] data) {
            for (int i = data.Length / 2; i >= 0; i--) {
                MaxHeapify(data, data.Length, i);
            }
        }

        void MaxHeapify(int[] data, int size, int heapifyIndex) {
            if (heapifyIndex < 0 || heapifyIndex >= data.Length)
                return;

            while (true) {
                int value = data[heapifyIndex];
                int max = value;
                int leftIndex = 2 * heapifyIndex + 1;
                int rightIndex = leftIndex + 1;
                bool hasLeftChild = leftIndex < size;
                bool hasRightChild = rightIndex < size;
                int leftValue = hasLeftChild ? data[leftIndex] : 0;
                int rightValue = hasRightChild ? data[rightIndex] : 0;
                int indexToSwap = -1;

                if (hasLeftChild && leftValue > max) {
                    max = leftValue;
                    indexToSwap = leftIndex;
                }

                if (hasRightChild && rightValue > max) {
                    max = rightValue;
                    indexToSwap = rightIndex;
                }

                if (indexToSwap > -1) {
                    Swap(data, heapifyIndex, indexToSwap);
                    heapifyIndex = indexToSwap;
                }
                else
                    break;
            }
        }

        void Swap(int[] data, int index1, int index2) {
            int temp = data[index1];
            data[index1] = data[index2];
            data[index2] = temp;
        }
    }
}
