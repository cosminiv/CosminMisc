using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HackerRank
{
    class ArrayNewYear
    {
        static void minimumBribes(int[] q) {
            int sum = 0;

            for (int i = 0; i < q.Length; i++) {
                if (q[i] > i + 1) {
                    int diff = q[i] - (i + 1);
                    if (diff > 2) {
                        Console.WriteLine("Too chaotic");
                        return;
                    }
                    sum += diff;
                }
                else if (i + 1 < q.Length && q[i] > q[i + 1]) {
                    sum++;
                }
            }

            Console.WriteLine(sum);
        }
    }
}
