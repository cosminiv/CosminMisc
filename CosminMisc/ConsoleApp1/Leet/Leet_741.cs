using System;
using ConsoleApp1.Common;

namespace ConsoleApp1.Leet
{
    public class Leet_741
    {
        public void Solve()
        {
            int[][] grid =
            {
                new[] {1,1,1,1,0,0,0},
                new[] {0,0,0,1,0,0,0},
                new[] {0,0,0,1,0,0,1},
                new[] {1,0,0,1,0,0,0},
                new[] {0,0,0,1,0,0,0},
                new[] {0,0,0,1,0,0,0},
                new[] {0,0,0,1,1,1,1}
            };

            int[][] grid3 =
            {
                new[] { 1, 1, -1 },
                new[] { 1, -1, 1 },
                new[] { -1, 1,  1 }
            };

            int[][] grid2 =
            {
                new[] { 0, 1, -1 },
                new[] { 1, 0, -1 },
                new[] { 1, 1,  1 }
            };

            int a = CherryPickup(grid);
        }

        public int CherryPickup(int[][] grid)
        {
            int score1 = ComputeBestPathTopLeftToBottomRight(grid, out var solution);
            int score2 = ComputeBestPathBottomRightToTopLeft(grid, solution);
            return score1 + score2;
        }

        int ComputeBestPathTopLeftToBottomRight(int[][] grid, out int[][] solution)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;

            solution = MakeMatrix<int>(rows, cols);

            // Compute last solution element
            solution[rows - 1][cols - 1] = grid[rows - 1][cols - 1];

            // Compute last column
            int row;
            int col = cols - 1;
            for (row = rows - 2; row >= 0; row--)
            {
                int gridElem = grid[row][col];
                int prevSolutionElem = solution[row + 1][col];
                solution[row][col] = gridElem == -1 || prevSolutionElem == -1 ? -1 : gridElem + prevSolutionElem;
            }

            // Compute last row
            row = rows - 1;
            for (col = cols - 2; col >= 0; col--)
            {
                int gridElem = grid[row][col];
                int prevSolutionElem = solution[row][col + 1];
                solution[row][col] = gridElem == -1 || prevSolutionElem == -1 ? -1 : gridElem + prevSolutionElem;
            }

            // Compute the rest of the solution
            for (row = rows - 2; row >= 0; row--)
            {
                for (col = cols - 2; col >= 0; col--)
                {
                    int gridElem = grid[row][col];
                    int prevSolutionElem = Math.Max(solution[row][col + 1], solution[row + 1][col]);
                    solution[row][col] = gridElem == -1 || prevSolutionElem == -1 ? -1 : gridElem + prevSolutionElem;
                }
            }

            // Walk the best path and set zeros in the grid
            row = 0;
            col = 0;

            while (row < rows && col < cols)
            {
                grid[row][col] = 0;

                if (col == cols - 1) row++;
                else if (row == rows - 1) col++;
                else if (solution[row][col + 1] > solution[row + 1][col]) col++;
                else row++;
            }

            return Math.Max(solution[0][0], 0);
        }

        static T[][] MakeMatrix<T>(int rows, int cols)
        {
            T[][] result = new T[rows][];

            for (var index = 0; index < result.Length; index++)
            {
                result[index] = new T[cols];
            }

            return result;
        }

        int ComputeBestPathBottomRightToTopLeft(int[][] grid, int[][] solution)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;

            // Compute first solution element
            solution[0][0] = grid[0][0];

            // Compute first column
            int row;
            int col = 0;
            for (row = 1; row < rows; row++)
            {
                int gridElem = grid[row][col];
                int prevSolutionElem = solution[row - 1][col];
                solution[row][col] = gridElem == -1 || prevSolutionElem == -1 ? -1 : gridElem + prevSolutionElem;
            }

            // Compute first row
            row = 0;
            for (col = 1; col < cols; col++)
            {
                int gridElem = grid[row][col];
                int prevSolutionElem = solution[row][col - 1];
                solution[row][col] = gridElem == -1 || prevSolutionElem == -1 ? -1 : gridElem + prevSolutionElem;
            }

            // Compute the rest of the solution
            for (row = 1; row < rows; row++)
            {
                for (col = 1; col < cols; col++)
                {
                    int gridElem = grid[row][col];
                    int prevSolutionElem = Math.Max(solution[row][col - 1], solution[row - 1][col]);
                    solution[row][col] = gridElem == -1 || prevSolutionElem == -1 ? -1 : gridElem + prevSolutionElem;
                }
            }

            return Math.Max(solution[rows - 1][cols - 1], 0);
        }
    }
}
