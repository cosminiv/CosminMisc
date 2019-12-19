using System;
using System.Diagnostics;

namespace ConsoleApp1.Leet
{
    class Leet_174
    {
        private int _rows;
        private int _cols;

        public void Solve()
        {
            int[][] dungeon = new[]
            {
                new [] {1, -3, 3},
                new [] {0, -2, 0},
                new [] {-3, -3, -3},
            };

            int[][] dungeon2 = new []
            {
                new [] {-5, 10},
                new [] {-100, -2},
            };

            int result = CalculateMinimumHP(dungeon);
        }

        public int CalculateMinimumHP(int[][] dungeon)
        {
            _rows = dungeon.Length;
            _cols = dungeon[0].Length;

            int[][] additiveHpMatrix = MakeAdditiveHpMatrix(dungeon);
            int[] bestAdditiveHpVector = GetBestRouteHpVector(additiveHpMatrix, dungeon, out int minHp);
            int result = minHp > 0 ? 1 : (-1 * minHp + 1);
            return result;
        }

        private int[] GetBestRouteHpVector(int[][] additiveHpMatrix, int[][] dungeon, out int minHp)
        {
            int[] result = new int[_rows + _cols - 1];
            int row = 0, col = 0;
            result[0] = dungeon[0][0];
            minHp = result[0];

            for (int i = 1; ; i++)
            {
                int? neighborToRight = (col < _cols - 1) ? additiveHpMatrix[row][col + 1] : (int?)null;
                int? neighborToBottom = (row < _rows - 1) ? additiveHpMatrix[row + 1][col] : (int?)null;

                if (neighborToRight.HasValue && neighborToBottom.HasValue)
                {
                    if (neighborToRight > neighborToBottom)
                    {
                        result[i] = result[i - 1] + dungeon[row][col + 1];
                        col++;
                    }
                    else
                    {
                        result[i] = result[i - 1] + dungeon[row + 1][col];
                        row++;
                    }
                }
                else if (neighborToRight.HasValue)
                {
                    result[i] = result[i - 1] + dungeon[row][col + 1];
                    col++;
                }
                else if (neighborToBottom.HasValue)
                {
                    result[i] = result[i - 1] + dungeon[row + 1][col];
                    row++;
                }

                if (i >= result.Length)
                    break;

                if (result[i] < minHp)
                    minHp = result[i];
            }

            return result;
        }

        private int[][] MakeAdditiveHpMatrix(int[][] dungeon)
        {
            int[][] matrix = new int[_rows][];

            for (int i = 0; i < _rows; i++)
                matrix[i] = new int[_cols];

            InitBottomAndRightEdges(dungeon, matrix);

            // Build the rest of the matrix starting from the edges.
            for (int row = _rows - 2; row >= 0; row--)
            {
                for (int col = _cols - 2; col >= 0; col--)
                {
                    int minPrevious = Math.Max(matrix[row + 1][col], matrix[row][col + 1]);
                    long x = dungeon[row][col] + minPrevious;
                    if (x > int.MaxValue)
                        x = int.MaxValue;

                    matrix[row][col] = (int)x;
                }
            }

            return matrix;
        }

        private void InitBottomAndRightEdges(int[][] dungeon, int[][] matrix)
        {
            matrix[_rows - 1][_cols - 1] = dungeon[_rows - 1][_cols - 1];

            for (int col = _cols - 2; col >= 0; col--)
                matrix[_rows - 1][col] = dungeon[_rows - 1][col] + matrix[_rows - 1][col + 1];

            for (int row = _rows - 2; row >= 0; row--)
                matrix[row][_cols - 1] = dungeon[row][_cols - 1] + matrix[row + 1][_cols - 1];
        }
    }
}
