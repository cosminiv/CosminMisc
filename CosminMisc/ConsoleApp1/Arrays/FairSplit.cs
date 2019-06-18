using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Arrays
{
    public class FairSplit
    {
        public static void Test() {
            var a = GenerateWaysToSplitArray(new int[] { 1,2,3,4,5,6,7,8,9,10 }, 5);
        }

        static List<int[]> GenerateWaysToSplitArray(int[] arr, int breakpointCount) {
            Debug.Assert(breakpointCount <= arr.Length);
            List<int[]> result = new List<int[]>();

            for (int[] bp = MakeInitialBreakpoints(breakpointCount); 
                bp != null; 
                bp = MakeNextBreakpoints(bp, arr.Length)) {
                result.Add(bp);
                PrintArray(bp);
            }

            return result;
        }

        private static void PrintArray(int[] bp) {
            String val = string.Join(" ", bp);
            Debug.Print(val);
            //Console.WriteLine(val);
        }

        static int[] MakeInitialBreakpoints(int breakpointCount) {
            int[] breakpoints = new int[breakpointCount];
            for (int i = 0; i < breakpoints.Length; i++) {
                breakpoints[i] = i;
            }
            return breakpoints;
        }

        static int[] MakeNextBreakpoints(int[] bp, int arrayLength) {
            //int[] bp = (int[])bp.Clone();

            // Find the element which we can increment
            int incrementedIndex = -1;
            for (int i = bp.Length - 1; i >= 0; i--) {
                int j = bp.Length - i;
                if (bp[i] < arrayLength - j) {
                    bp[i]++;
                    incrementedIndex = i;
                    break;
                }
            }

            if (incrementedIndex < 0)
                return null;

            // Adjust the elements after the one incremented 
            int value = bp[incrementedIndex] + 1;
            for (int i = incrementedIndex + 1; i < bp.Length; i++) {
                bp[i] = value++; 
            }

            return bp;
        }
    }
}
