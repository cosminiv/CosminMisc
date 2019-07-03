using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Algorithms
{
    class BinaryHeap<T>
    {
        private T[] _data;
        private Func<T, T, int> _comparer;

        public static void Test() {
            BinaryHeap<int> heap = new BinaryHeap<int>((x, y) => x - y);

            heap.Insert(-10);
            //Console.WriteLine($"{heap.Peek()}");

            //heap.Insert(1);
            //Console.WriteLine($"{heap.Peek()}");

            //heap.Insert(9);
            //Console.WriteLine($"{heap.Peek()}");

            Console.WriteLine(heap);

            //while (heap.Size > 0) {
            //    Console.Write($"{heap.Extract()} ");
            //}
        }

        public BinaryHeap(Func<T, T, int> comparer) {
            _data = new T[16];
            _comparer = comparer;
            Size = _data.Length;
        }

        public void Insert(T val) {
            if (Size == _data.Length)
                Enlarge();

            _data[Size] = val;
            Size++;

            ReCalculateUp();
            //Debug_ValidateHeap();
        }

        private void ReCalculateUp() {
            var index = Size - 1;
            while (!IsRoot(index) && _comparer(_data[index], GetParent(index)) < 0) {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }

        private void Debug_ValidateHeap() {
            for (int i = 0; i < Size / 2; i++) {
                if (HasLeftChild(i) && _comparer(GetLeftChild(i), _data[i]) < 0)
                    throw new Exception($"Left child is smaller than element ({GetLeftChild(i)} < {_data[i]})");
                if (HasRightChild(i) && _comparer(GetRightChild(i), _data[i]) < 0)
                    throw new Exception($"Right child is smaller than element ({GetRightChild(i)} < {_data[i]})");
            }
        }

        private void Enlarge() {
            int newSize = (Size == 0 ? 16 : _data.Length * 2);
            T[] bigger = new T[newSize];
            Array.Copy(_data, bigger, _data.Length);
            _data = bigger;
        }

        public T Extract() {
            T result = _data[0];
            _data[0] = _data[Size - 1];
            Size--;

            ReCalculateDown();
            //Debug_ValidateHeap();
            return result;
        }

        private void ReCalculateDown() {
            int index = 0;
            while (HasLeftChild(index)) {
                var smallerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && _comparer(GetRightChild(index), GetLeftChild(index)) < 0) 
                    smallerIndex = GetRightChildIndex(index);

                if (_comparer(_data[smallerIndex], _data[index]) >= 0)
                    break;

                Swap(smallerIndex, index);
                index = smallerIndex;
            }
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;
        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < Size;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < Size;
        private T GetLeftChild(int elementIndex) => _data[GetLeftChildIndex(elementIndex)];
        private T GetRightChild(int elementIndex) => _data[GetRightChildIndex(elementIndex)];
        private bool IsRoot(int elementIndex) => elementIndex == 0;
        private T GetParent(int elementIndex) => _data[GetParentIndex(elementIndex)];

        public T Peek() {
            if (Size > 0)
                return _data[0];
            else
                throw new Exception("Heap is empty");
        }

        public int Size { get; private set; }

        //void MinHeapify(int heapifyIndex) {
        //    if (heapifyIndex < 0 || heapifyIndex >= _data.Length)
        //        return;

        //    while (true) {
        //        T value = _data[heapifyIndex];
        //        T min = value;
        //        int leftIndex = 2 * heapifyIndex + 1;
        //        int rightIndex = leftIndex + 1;
        //        bool hasLeftChild = leftIndex < Size;
        //        bool hasRightChild = rightIndex < Size;
        //        T leftValue = hasLeftChild ? _data[leftIndex] : default;
        //        T rightValue = hasRightChild ? _data[rightIndex] : default;
        //        int indexToSwap = -1;

        //        if (hasLeftChild && _comparer(leftValue, min) < 0) {
        //            min = leftValue;
        //            indexToSwap = leftIndex;
        //        }

        //        if (hasRightChild && _comparer(rightValue, min) < 0) {
        //            min = rightValue;
        //            indexToSwap = rightIndex;
        //        }

        //        if (indexToSwap > -1) {
        //            Swap(heapifyIndex, indexToSwap);
        //            heapifyIndex = indexToSwap;
        //        }
        //        else
        //            break;
        //    }
        //}

        void Swap(int index1, int index2) {
            T temp = _data[index1];
            _data[index1] = _data[index2];
            _data[index2] = temp;
        }

        public override string ToString() {
            return string.Join(" ", _data.Take(Size));
        }
    }
}
