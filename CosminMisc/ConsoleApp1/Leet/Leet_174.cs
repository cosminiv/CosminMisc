using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using ConsoleApp1.Common;

namespace ConsoleApp1.Leet
{
    class Leet_174
    {
        public void Solve()
        {
            int[][] dungeon = LoadBigDungeon();

            int[][] dungeon2 =
            {
                new [] {-2, -3, 3},
                new [] {-5, -10, 1},
                new [] {10, 30, -5},
            };

            int[][] dungeon4 =
            {
                new [] {1, -3, 3},
                new [] {0, -2, 0},
                new [] {-3, -3, -3},
            };

            int result = CalculateMinimumHP(dungeon);
        }

        public int CalculateMinimumHP(int[][] dungeon)
        {
            int rows = dungeon.Length;
            int cols = dungeon[0].Length;

            int[][] solution = MakeMatrix<int>(rows, cols);

            // Compute last solution element
            solution[rows - 1][cols - 1] = dungeon[rows - 1][cols - 1] > 0 ? 1 : 0 - dungeon[rows - 1][cols - 1] + 1;

            // Compute last column
            int row;
            int col = cols - 1;
            for (row = rows - 2; row >= 0; row--)
            {
                int dungeonElem = dungeon[row][col];
                int prevSolutionElem = solution[row + 1][col];
                solution[row][col] = Math.Max(prevSolutionElem - dungeonElem, 1);
            }

            // Compute last row
            row = rows - 1;
            for (col = cols - 2; col >= 0; col--)
            {
                int dungeonElem = dungeon[row][col];
                int prevSolutionElem = solution[row][col + 1];
                solution[row][col] = Math.Max(prevSolutionElem - dungeonElem, 1);
            }

            // Compute the rest of the solution
            for (row = rows - 2; row >= 0; row--)
            {
                for (col = cols - 2; col >= 0; col--)
                {
                    int dungeonElem = dungeon[row][col];
                    int prevSolutionElem = Math.Min(solution[row][col + 1], solution[row + 1][col]);
                    solution[row][col] = Math.Max(prevSolutionElem - dungeonElem, 1);
                }
            }

            return solution[0][0];
        }

        public static T[][] MakeMatrix<T>(int rows, int cols)
        {
            T[][] result = new T[rows][];

            for (var index = 0; index < result.Length; index++)
            {
                result[index] = new T[cols];
            }

            return result;
        }

        private int[][] LoadBigDungeon()
        {
            string file = @"C:\Temp\aaaa 2.txt";
            string[] lines = File.ReadAllLines(file).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            int rows = lines.Length;
            int[][] dungeon = new int[rows][];
            int lineIndex = 0;

            foreach (string line in lines)
            {
                dungeon[lineIndex] = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x.Trim())).ToArray();
                lineIndex++;
            }

            return dungeon;
        }

        void Write(string text)
        {
            Debug.Write(text);
        }

        void WriteLine(string text)
        {
            Debug.WriteLine(text);
        }
    }
}
