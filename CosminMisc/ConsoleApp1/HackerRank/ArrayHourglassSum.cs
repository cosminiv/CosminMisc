using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HackerRank
{
    public class ArrayHourglassSum
    {
        public static void Test() {
            string input =
@"
1 1 1 0 0 0
0 1 0 0 0 0
1 1 1 0 0 0
0 0 2 4 4 0
0 0 0 2 0 0
0 0 1 2 4 0
";

            int[][] arr = new int[6][];
            string[] lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < 6; i++) {
                arr[i] = Array.ConvertAll(lines[i].Split(' '), arrTemp => Convert.ToInt32(arrTemp));
            }

            int result = hourglassSum(arr);

            Console.WriteLine(result);
        }

        static int hourglassSum(int[][] arr) {
            int maxSum = -1000;

            for (int i = 0; i <= arr.Length - 3; i++) {
                for (int j = 0; j <= arr[0].Length - 3; j++) {
                    int sum = arr[i][j] + arr[i][j + 1] + arr[i][j + 2] +
                        arr[i + 1][j + 1] + arr[i + 2][j] + arr[i + 2][j + 1] + arr[i + 2][j + 2];
                    if (sum > maxSum)
                        maxSum = sum;
                    Console.WriteLine($"({i}, {j}) -> {sum}");
                }
            }

            return maxSum;
        }
    }
}
