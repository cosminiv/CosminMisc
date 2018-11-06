using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    class Leet_004
    {
        public int Solve() {
            var tests = new[] {
                (new int[] { }, new int[] { 2 }, 2),
                (new int[] { 7 }, new int[] { }, 7),
                (new int[] { 2, 7 }, new int[] { }, 4.5),
                (new int[] { 3 }, new int[] { 7 }, 5),
                (new int[] { }, new int[] { 7, 13 }, 10),
                (new int[] { 3, 4 }, new int[] { 7 }, 4),
                (new int[] { -5 }, new int[] { 5, 200 }, 5),
                (new int[] { -5 }, new int[] { -1, 2000 }, -1),
                (new int[] { -5 }, new int[] { -1, 20, 30 }, 9.5),
                (Enumerable.Range(1, 500000).ToArray(), Enumerable.Range(500001, 500000).ToArray(), 500000.5),
            };

            foreach (var test in tests) {
                double res = FindMedianSortedArrays(test.Item1, test.Item2);
                Debug.Assert(res == test.Item3);
            }

            return 0;
        }

        public double FindMedianSortedArrays(int[] nums1, int[] nums2) {
            int[] merged = Merge(nums1, nums2);

            if (merged.Length % 2 == 1)
                return merged[merged.Length / 2];
            else {
                int mid = merged.Length / 2;
                double result = 0.5 * (merged[mid] + merged[mid - 1]);
                return result;
            }
        }

        private int[] Merge(int[] nums1, int[] nums2) {
            int[] result = new int[nums1.Length + nums2.Length];

            unsafe {
                int i1 = 0;
                int i2 = 0;
                int i = 0;

                while (true) {
                    if (i1 == nums1.Length) {
                        for (; i2 < nums2.Length; i2++)
                            result[i++] = nums2[i2];
                        break;
                    }
                    if (i2 == nums2.Length) {
                        for (; i1 < nums1.Length; i1++)
                            result[i++] = nums1[i1];
                        break;
                    }

                    if (nums1[i1] <= nums2[i2]) {
                        result[i++] = nums1[i1];
                        i1++;
                    }
                    else {
                        result[i++] = nums2[i2];
                        i2++;
                    }
                }
            }

            return result;
        }
    }
}
