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
            var a = GenerateWaysToSplitArray(new int[] { 3, 5, 7, 1, 2, 6 }, 4);
        }

        static List<int[]> GenerateWaysToSplitArray(int[] arr, int breakpointCount) {
            Debug.Assert(breakpointCount <= arr.Length);
            List<int[]> result = new List<int[]>();

            for (int[] bp = MakeInitialBreakpoints(breakpointCount); 
                bp != null; 
                bp = MakeNextBreakpoints(bp, arr.Length)) {
                result.Add(bp);
            }

            return result;
        }

        static int[] MakeInitialBreakpoints(int breakpointCount) {
            int[] breakpoints = new int[breakpointCount];
            for (int i = 0; i < breakpoints.Length; i++) {
                breakpoints[i] = i;
            }
            return breakpoints;
        }

        static int[] MakeNextBreakpoints(int[] bp, int arrayLength) {
            int[] bp2 = (int[])bp.Clone();

            for (int i = bp.Length - 1; i >= 0; i--) {
                int j = bp.Length - i + 1;
                if (bp[i] < arrayLength - j) {
                    bp[i]++;
                    return bp2;
                }
            }

            return null; // bp2;
        }
    }
}
