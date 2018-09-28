using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_031
    {
        public static long Solve()
        {
            return Solve2();
        }

        // Wow, such performance, much speed!  19ms when quitting each loop early
        // TODO: Find a more elegant solution
        public static long Solve2()
        {
            List<int> numbers = new List<int> { 1, 2, 5, 10, 20, 50, 100, 200 };
            int targetSum = 200;
            int ways = 1;   // for taking 200 once

            for (int i1 = 0; i1 <= 200; i1++)
            {
                int sum1 = i1;

                for (int i2 = 0; i2 <= 100; i2++)
                {
                    int sum2 = sum1 + i2 * 2;
                    if (sum2 > targetSum) break;

                    for (int i3 = 0; i3 <= 40; i3++)
                    {
                        int sum3 = sum2 + i3 * 5;
                        if (sum3 > targetSum) break;

                        for (int i4 = 0; i4 <= 20; i4++)
                        {
                            int sum4 = sum3 + i4 * 10;
                            if (sum4 > targetSum) break;

                            for (int i5 = 0; i5 <= 10; i5++)
                            {
                                int sum5 = sum4 + i5 * 20;
                                if (sum5 > targetSum) break;

                                for (int i6 = 0; i6 <= 4; i6++)
                                {
                                    int sum6 = sum5 + i6 * 50;
                                    if (sum6 > targetSum) break;

                                    for (int i7 = 0; i7 <= 2; i7++)
                                    {
                                        int sum7 = sum6 + i7 * 100;

                                        if (sum7 == targetSum) { ways++; break; }
                                        if (sum7 > targetSum) break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ways;
        }

        // 5s
        public static long SolveBruteForce()
        {
            List<int> numbers = new List<int> { 1, 2, 5, 10, 20, 50, 100, 200 };
            int targetSum = 200;
            int ways = 1;   // for taking 200 once

            for (int i1 = 0; i1 <= 200; i1++)
                for (int i2 = 0; i2 <= 100; i2++)
                    for (int i3 = 0; i3 <= 40; i3++)
                        for (int i4 = 0; i4 <= 20; i4++)
                            for (int i5 = 0; i5 <= 10; i5++)
                                for (int i6 = 0; i6 <= 4; i6++)
                                    for (int i7 = 0; i7 <= 2; i7++)
                                            if (targetSum == i1 + i2 * 2 + i3 * 5 + i4 * 10 + i5 * 20 + i6 * 50 + i7 * 100)
                                                ways++;

            return ways;
        }
    }
}
