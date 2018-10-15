using ConsoleApp1.Eu.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_056
    {
        readonly static int MAX = 100;

        // TODO: takes 8s
        public static int Solve() {
            int maxSum = 0;
            string txtForMaxSum = "";

            for (int i = 0; i < MAX; i++) {
                for (int j = 0; j < MAX; j++) {
                    LargeNumber n = LargeNumber.Power(i, j);
                    int digitSum = n.ToString().ToCharArray().Sum(c => c - '0');
                    if (digitSum > maxSum) {
                        maxSum = digitSum;
                        txtForMaxSum = $"{i} ^ {j} = {n}";
                    }
                }
            }

            Console.WriteLine(txtForMaxSum);

            return maxSum;
        }
    }
}
