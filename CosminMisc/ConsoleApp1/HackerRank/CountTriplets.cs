using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp1.HackerRank
{
    class CountTriplets
    {
        public static void Solve() {
            var a = countTriplets(new List<long> { 16, 8, 1, 10, 4, 2, 16, 32, 16, 8, 2, 8, 32, 8 }, 4);
            Console.WriteLine(a);
        }

        static long countTriplets(List<long> arr, long r) {
            Dictionary<long, List<long>> data = PrepareData(arr);
            long result = 0;

            for (int i = 0; i < arr.Count; i++) {
                long n1 = arr[i];
                long n2 = n1 * r;

                FindNumber(data, n2, i, out List<long> indexList2, out int startIndex2);

                if (startIndex2 >= 0) {
                    long n3 = n2 * r;
                    for (int j = startIndex2; j < indexList2.Count; j++) {
                        FindNumber(data, n3, indexList2[j], out List<long> indexList3, out int startIndex3);
                        if (startIndex3 >= 0) {
                            int howMany = indexList3.Count - startIndex3;
                            result += howMany;
                            //if (howMany > 0)
                            //    Console.WriteLine($"({n1} {n2} {n3}) x {howMany}");
                        }
                    }
                }
            }

            return result;
        }

        static void FindNumber(Dictionary<long, List<long>> data, long number, long prevNumberIndex, out List<long> indexes, out int startIndex) {
            bool found = data.TryGetValue(number, out indexes);
            startIndex = -1;

            if (found) {
                int indexOfI = indexes.BinarySearch(prevNumberIndex);
                if (indexOfI < 0)
                    startIndex = ~indexOfI;
                else
                    startIndex = indexOfI + 1;
            }
        }

        private static Dictionary<long, List<long>> PrepareData(List<long> arr) {
            Dictionary<long, List<long>> result = new Dictionary<long, List<long>>();

            for (int i = 0; i < arr.Count; i++) {
                List<long> list;
                bool found = result.TryGetValue(arr[i], out list);
                if (!found) {
                    list = new List<long>() { i };
                    result.Add(arr[i], list);
                }
                else
                    list.Add(i);
            }

            return result;
        }
    }
}
