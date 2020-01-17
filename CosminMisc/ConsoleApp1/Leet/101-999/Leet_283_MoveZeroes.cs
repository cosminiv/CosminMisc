using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    class Leet_283_MoveZeroes
    {
        public void Test() {
            int[] nums = new[] { 0, 1, 0, 3, 12 };
            Solve(nums);
        }

        // [0,1,0,3,12] -> [1,3,12,0,0]
        public void Solve(int[] nums) {
            int firstIndexOfZero = FindFirstIndexOfZero(nums, 0);
            if (firstIndexOfZero < 0)
                return;

            int firstIndexOfNonZero = FindFirstIndexOfNonZero(nums, firstIndexOfZero + 1);

            while(firstIndexOfNonZero >= 0 &&
                firstIndexOfZero >= 0 &&
                firstIndexOfNonZero > firstIndexOfZero) 
            {
                Swap(nums, firstIndexOfZero, firstIndexOfNonZero);
                firstIndexOfZero = FindFirstIndexOfZero(nums, firstIndexOfZero + 1);
                firstIndexOfNonZero = FindFirstIndexOfNonZero(nums, firstIndexOfNonZero + 1);
            }
        }

        private void Swap(int[] nums, int index1, int index2) {
            //int temp = nums[index1];
            nums[index1] = nums[index2];
            nums[index2] = 0;
        }

        private int FindFirstIndexOfZero(int[] nums, int startIndex) {
            for (int i = startIndex; i < nums.Length; i++) {
                if (nums[i] == 0)
                    return i;
            }

            return -1;
        }

        private int FindFirstIndexOfNonZero(int[] nums, int startIndex) {
            for (int i = startIndex; i < nums.Length; i++) {
                if (nums[i] != 0)
                    return i;
            }

            return -1;
        }
    }
}
