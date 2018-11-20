using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Algorithms
{
    class CountingSort
    {
        // *** Useful when the number of possible values is less than the number of values  ***


        // Input data: 6 0 2 0 1 3 4 6 1 3 2
        //
        // Counts vector: 
        // 0 1 2 3 4 5 6 
        // 2 2 2 2 1 0 2
        //
        // Running counts vector:
        // 0 1 2 3 4 5 6
        // 2 4 6 8 9 9 11
        //
        // Output:                          Running counts: 
        // 0 1 2 3 4 5 6 7 8 9 10           0 1 2 3 4 5 6
        // -------------------------------------------------
        //                                  2 4 6 8 9 9 11
        //           2                      2 4 5 8 9 9 11    Took 2 from the end of input, looked up position in running counts
        //           2   3                  2 4 5 7 9 9 11    Took 3 from the end of input
        //       1   2   3                  2 3 5 7 9 9 11            
        //       1   2   3     6            2 3 5 7 9 9 10
        //       1   2   3 4   6            2 3 5 7 8 9 10
        //       1   2 3 3 4   6            2 3 5 6 8 9 10
        //     1 1   2 3 3 4   6            2 2 5 6 8 9 10
        //   0 1 1   2 3 3 4   6            1 2 5 6 8 9 10
        //   0 1 1 2 2 3 3 4   6            1 2 4 6 8 9 10
        // 0 0 1 1 2 2 3 3 4   6            0 2 4 6 8 9 10
        // 0 0 1 1 2 2 3 3 4 6 6            0 2 4 6 8 9 10


        public void Test() {
            int[] data = { 6, 0, 2, 0, 1, 3, 4, 6, 1, 3, 2 };
            string[] dataStr = data.Select(d => $"-{d}-").ToArray();
            Func<string, int, int> keySelector = ((str, obj) => int.Parse(str.Substring(1, 1)));

            string[] sorted = SortV2(dataStr, keySelector, 0, 6);
            //int[] sorted = Sort(data, data.Max());

            string sortedStr = string.Join(" ", sorted);
        }

        public T[] SortV2<T>(T[] data, Func<T, int, int> keySelector, int keySelectorParam, int maxValue) {
            int[] runningCounts = BuildRunningCountsV2(data, keySelector, keySelectorParam, maxValue);
            T[] result = BuildSortedArrayV2(data, keySelector, keySelectorParam, runningCounts);
            return result;
        }

        private int[] BuildRunningCountsV2<T>(T[] data, Func<T, int, int> keySelector, int keySelectorParam, int maxValue) {
            int[] result = new int[maxValue + 1];

            for (int i = 0; i < data.Length; i++) {
                int n = keySelector(data[i], keySelectorParam);
                result[n]++;
            }

            for (int j = 1; j < result.Length; j++) {
                result[j] += result[j - 1];
            }

            return result;
        }

        private T[] BuildSortedArrayV2<T>(T[] data, Func<T, int, int> keySelector, int keySelectorParam, int[] runningCounts) {
            T[] result = new T[data.Length];

            for (int i = data.Length - 1; i >= 0; i--) {
                int n = keySelector(data[i], keySelectorParam);
                result[runningCounts[n] - 1] = data[i];
                runningCounts[n]--;
            }

            return result;
        }

        public int[] Sort(int[] data, int maxValue) {
            int[] runningCounts = BuildRunningCounts(data, maxValue);
            int[] result = BuildSortedArray(data, runningCounts);
            return result;
        }

        private int[] BuildRunningCounts(int[] data, int maxValue) {
            int[] result = new int[maxValue + 1];

            for (int i = 0; i < data.Length; i++) {
                int n = data[i];
                result[n]++;
            }

            for (int j = 1; j < result.Length; j++) {
                result[j] += result[j - 1];
            }

            return result;
        }

        private int[] BuildSortedArray(int[] data, int[] runningCounts) {
            int[] result = new int[data.Length];

            for (int i = data.Length - 1; i >= 0; i--) {
                int n = data[i];
                result[runningCounts[n] - 1] = n;
                runningCounts[n]--;
            }

            return result;
        }
    }
}
