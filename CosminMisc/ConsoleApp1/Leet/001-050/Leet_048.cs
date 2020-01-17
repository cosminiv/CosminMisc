
using System;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1.Leet
{
    // You are given an n x n 2D matrix representing an image.
    // Rotate the image by 90 degrees(clockwise).
    class Leet_048
    {
        public void Test() {
            //int[][] matrix = new int[][] {
            //    new[] { 1, 2 },
            //    new[] { 3, 4 },
            //};

            //int[][] matrix = new int[][] {
            //    new[] { 1, 2, 3 },
            //    new[] { 4, 5, 6 },
            //    new[] { 7, 8, 9 },
            //};

            //int[][] matrix = new int[][] {
            //    new[] { 1, 2, 3, 4 },
            //    new[] { 5, 6, 7, 8 },
            //    new[] { 9, 10, 11, 12 },
            //    new[] { 13, 14, 15, 16 }
            //};

            int i = 1;
            int[][] matrix = new int[][] {
                new[] { i++, i++, i++, i++, i++, i++, },
                new[] { i++, i++, i++, i++, i++, i++, },
                new[] { i++, i++, i++, i++, i++, i++, },
                new[] { i++, i++, i++, i++, i++, i++, },
                new[] { i++, i++, i++, i++, i++, i++, },
                new[] { i++, i++, i++, i++, i++, i++, },
            };

            Debug.WriteLine(MatrixToString(matrix));

            Rotate(matrix);
            //Debug.WriteLine(MatrixToString(matrix));
        }

        public void Rotate(int[][] matrix) {
            if (matrix == null || matrix.Length == 1)
                return;

            int n = matrix.Length;

            for (int shell = 0; shell <= n / 2; shell++) {
                for (int i = 0; i < n - 2 * shell - 1; i++) {
                    var (row1, col1) = (shell, shell + i);
                    var (row2, col2) = (n - shell - 1 - i, shell);
                    var (row3, col3) = (n - shell - 1, n - shell - 1 - i);
                    var (row4, col4) = (shell + i, n - shell - 1);

                    int temp = matrix[row1][col1];
                    matrix[row1][col1] = matrix[row2][col2];
                    matrix[row2][col2] = matrix[row3][col3];
                    matrix[row3][col3] = matrix[row4][col4];
                    matrix[row4][col4] = temp;
                }
            }
        }

        string MatrixToString(int[][] matrix) {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < matrix.Length; i++) {
                for (int j = 0; j < matrix[i].Length; j++) {
                    sb.Append(matrix[i][j].ToString().PadLeft(2));
                    sb.Append(" ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
