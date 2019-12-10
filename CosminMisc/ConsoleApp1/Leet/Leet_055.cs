using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleApp1.Leet
{
    public class Leet_055
    {
        public void Solve()
        {
            bool a = CanJump(new[] {2, 3, 1, 1, 4});
            bool b = CanJump(new[] {3, 2, 1, 0, 4});
            bool c = CanJump(MakeBigArray());
            bool d = CanJump(new []{2, 5, 0, 0});
            bool e = CanJump(new[] {1, 1, 2, 2, 0, 1, 1});
        }

        int[] MakeBigArray()
        {
            int[] array = new int[25_003];
            array[array.Length - 2] = 1;
            for (int i = 0; i < 25_000; i++)
            {
                array[i] = 25_000 - i;
            }

            return array;
        }

        public bool CanJump(int[] nums)
        {
            Stack<int> stack = new Stack<int>();
            int[] lastPoppedIndexes = new int[nums.Length];
            stack.Push(0);

            while (stack.Count > 0)
            {
                int currentIndex = stack.Peek();
                
                int currentNumber = nums[currentIndex];
                int indexToPush = currentIndex + currentNumber;

                if (indexToPush >= nums.Length - 1) return true;

                if (lastPoppedIndexes[currentIndex] > 0)
                    indexToPush = lastPoppedIndexes[currentIndex] - 1;

                if (indexToPush > currentIndex)
                    stack.Push(indexToPush);
                else
                {
                    int popped = stack.Pop();
                    if (stack.Count > 0)
                        lastPoppedIndexes[stack.Peek()] = popped;
                }

                //PrintStack(stack);
            }

            return false;
        }

        private void PrintStack(Stack<int> stack)
        {
            StringBuilder sb = new StringBuilder();

            foreach (int n in stack)
            {
                sb.Insert(0, " ");
                sb.Insert(0, n);
            }

            Debug.Print(sb.ToString());
        }

        bool CanJump(int[] nums, Stack<int> stack, HashSet<int> notPossibleIndexes)
        {
            int currentIndex = stack.Peek();

            if (currentIndex == nums.Length - 1) return true;

            if (notPossibleIndexes.Contains(currentIndex))
            {
                //Debug.Print($"Found {currentIndex} in not possible cache");
                return false;
            }

            for (int i = 1; i <= nums[currentIndex]; i++)
            {
                int index = currentIndex + i;
                if (index == nums.Length) break;
                stack.Push(index);
                //if (CanJump(nums, index, notPossibleIndexes)) return true;
            }

            notPossibleIndexes.Add(currentIndex);

            return false;
        }
    }
}
