using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HackerRank
{
    class MagicSquare
    {
        // TODO  https://www.hackerrank.com/challenges/magic-square-forming/problem
        public int Solve() {
            int[][] s = new int[3][]
            {
                new[] { 4, 9, 2 },
                new[] { 3, 5, 7 },
                new[] { 8, 1, 5 },
            };

            int res = Solve(s);
            return res;
        }

        int Solve(int[][] s) {


            for (int i = 0; i < s.Length; i++) {
                for (int j = 0; j < s[i].Length; j++) {

                    for (int n = 1; n <= 9; n++) {
                        s[i][j] = n;
                    }
                }
            }
            return 0;
        }


        int[] _counts = new int[10];

        bool IsMagic(int[][] s) {
            // Check if elements are unique 
            Array.Clear(_counts, 0, _counts.Length);
            int sum = s[0].Sum();

            for (int i = 0; i < s.Length; i++) {
                for (int j = 0; j < s[i].Length; j++) {
                    int n = s[i][j];
                    if (++_counts[n] > 1)
                        return false;
                }
            }

            // Check sums
            if (s[1].Sum() != sum || s[2].Sum() != sum
                || ColumnSum(s, 0) != sum || ColumnSum(s, 1) != sum || ColumnSum(s, 2) != sum)
                return false;

            int diag1Sum = s[0][0] + s[1][1] + s[2][2];
            if (diag1Sum != sum)
                return false;

            int diag2Sum = s[0][2] + s[1][1] + s[2][0];
            if (diag2Sum != sum)
                return false;

            return true;
        }

        int ColumnSum(int[][] s, int col) {
            int sum = s[0][col] + s[1][col] + s[2][col];
            return sum;
        }
    }
}
