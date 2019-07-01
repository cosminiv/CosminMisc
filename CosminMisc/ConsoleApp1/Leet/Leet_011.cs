using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    //
    // Find the container with most water
    //
    class Leet_011
    {
        public void Test() {
            Console.WriteLine(
                MaxArea(new[] { 1, 2, 1 }));
        }

        public int MaxArea(int[] height) {
            return MaxArea_Greedy(height);
        }

        public int MaxArea_Greedy(int[] height) {
            int max = 0;
            int i = 0, j = height.Length - 1;

            while (i < j) {
                int area = 0;
                
                // If the right line is longer than the left, no point in checking
                // the following right lines
                if (height[j] >= height[i]) {
                    area = (j - i) * height[i];
                    i++;
                }
                // If the left line is longer than the right, no point in checking
                // the following left lines
                else {
                    area = (j - i) * height[j];
                    j--;
                }

                if (area > max)
                    max = area;
            }

            return max;
        }

        int MaxArea_BruteForce(int[] height) {
            int max = 0;

            for (int i = 0; i < height.Length; i++) {
                for (int j = i + 1; j < height.Length; j++) {
                    int area = (j - i) * Math.Min(height[i], height[j]);
                    if (area > max)
                        max = area;
                }
            }

            return max;
        }
    }
}
