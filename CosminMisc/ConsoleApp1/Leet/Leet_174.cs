using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1.Leet
{
    class Leet_174
    {
        private int _rows;
        private int _cols;
        private int[][] _dungeon;

        public void Solve()
        {
            int[][] dungeon3 =
            {
                new [] {-2, -3, 3},
                new [] {-5, -10, 1},
                new [] {10, 30, -5},
            };

            int[][] dungeon2 =
            {
                new [] {1, -3, 3},
                new [] {0, -2, 0},
                new [] {-3, -3, -3},
            };

            int[][] dungeon =
            {
                new [] {-5, 10},
                new [] {-100, -2},
            };

            int result = CalculateMinimumHP(dungeon);
        }

        public int CalculateMinimumHP(int[][] dungeon)
        {
            _dungeon = dungeon;
            _rows = dungeon.Length;
            _cols = dungeon[0].Length;

            int maxMinHp = int.MinValue;

            foreach (Path path in GenerateAllPaths())
            {
                PrintPath(path);
                int minHp = ComputeMinimumAdditiveHp(path);

                if (minHp > maxMinHp)
                    maxMinHp = minHp;

                Debug.Write($"minHp: {minHp} \n");
            }

            int result = maxMinHp > 0 ? 1 : (-1 * maxMinHp + 1);

            Debug.WriteLine($"\nResult = {result}");

            return result;
        }

        private int ComputeMinimumAdditiveHp(Path path)
        {
            int sum = 0;
            int minSum = int.MaxValue;

            for (int i = path.Coordinates.Count - 1; i >= 0; i--)
            {
                Coords coords = path.Coordinates[i];
                int val = _dungeon[coords.Row][coords.Col];
                sum += val;
                if (sum < minSum)
                    minSum = sum;
            }

            return minSum;
        }

        private void PrintPath(Path path)
        {
            foreach (Coords coords in path.Coordinates)
            {
                Debug.Write($"{_dungeon[coords.Row][coords.Col]} ");
            }
            Debug.Write($"({path.Hp}) ");
        }

        IEnumerable<Path> GenerateAllPaths()
        {
            Queue<Path> queue = new Queue<Path>();

            // Add last cell as path
            Path path0 = new Path();
            path0.Coordinates.Add(new Coords { Row = _rows - 1, Col = _cols - 1 });
            path0.Hp = _dungeon[_rows - 1][_cols - 1];
            queue.Enqueue(path0);

            // Breadth-first search
            while (queue.Count > 0)
            {
                Path path = queue.Dequeue();

                if (path.Coordinates.Count == _rows + _cols - 1)
                    yield return path;

                Coords lastCoords = path.Coordinates[path.Coordinates.Count - 1];

                if (lastCoords.Col > 0)
                {
                    var newCoordsList = CopyAndAdd(path.Coordinates, new Coords { Row = lastCoords.Row, Col = lastCoords.Col - 1 });
                    Path path2 = new Path { Coordinates = newCoordsList, Hp = path.Hp + _dungeon[lastCoords.Row][lastCoords.Col - 1] };
                    queue.Enqueue(path2);
                }

                if (lastCoords.Row > 0)
                {
                    var newCoordsList = CopyAndAdd(path.Coordinates, new Coords { Row = lastCoords.Row - 1, Col = lastCoords.Col });
                    Path path2 = new Path { Coordinates = newCoordsList, Hp = path.Hp + _dungeon[lastCoords.Row - 1][lastCoords.Col] };
                    queue.Enqueue(path2);
                }
            }
        }

        List<T> CopyAndAdd<T>(List<T> sourceList, T newElement)
        {
            List<T> result = new List<T>(sourceList.Count + 1);

            foreach (T elem in sourceList)
            {
                result.Add(elem);
            }

            result.Add(newElement);
            return result;
        }

        class Path
        {
            public List<Coords> Coordinates = new List<Coords>();
            public int Hp { get; set; }
        }

        class Coords
        {
            public int Row { get; set; }
            public int Col { get; set; }
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
