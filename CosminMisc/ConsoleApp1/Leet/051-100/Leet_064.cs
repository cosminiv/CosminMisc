using System;

namespace ConsoleApp1.Leet
{
    class Leet_064
    {
        public int MinPathSum(int[][] grid)
        {
            int[][] matrix = MakeMatrix(grid);
            return matrix[0][0];
        }

        private int[][] MakeMatrix(int[][] grid)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;

            int[][] matrix = new int[rows][];

            for (int i = 0; i < rows; i++)
                matrix[i] = new int[cols];

            InitBottomAndLeftEdges(grid, rows, cols, matrix);

            // Build the rest of the matrix starting from the edges.
            for (int row = rows - 2; row >= 0; row--)
            {
                for (int col = cols - 2; col >= 0; col--)
                {
                    int minPrevious = Math.Min(matrix[row + 1][col], matrix[row][col + 1]);
                    long x = grid[row][col] + minPrevious;
                    if (x > int.MaxValue)
                        x = int.MaxValue;

                    matrix[row][col] = (int)x;
                }
            }

            return matrix;
        }

        private static void InitBottomAndLeftEdges(int[][] grid, int rows, int cols, int[][] matrix)
        {
            // Init bottom-right element
            matrix[rows - 1][cols - 1] = grid[rows - 1][cols - 1];

            // Init last row
            for (int col = cols - 2; col >= 0; col--)
            {
                matrix[rows - 1][col] = grid[rows - 1][col] + matrix[rows - 1][col + 1];
            }

            // Init last column
            for (int row = rows - 2; row >= 0; row--)
            {
                matrix[row][cols - 1] = grid[row][cols - 1] + matrix[row + 1][cols - 1];
            }
        }
    }
}
