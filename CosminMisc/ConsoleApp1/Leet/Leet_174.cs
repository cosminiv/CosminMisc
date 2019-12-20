using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using ConsoleApp1.Common;

namespace ConsoleApp1.Leet
{
    class Leet_174
    {
        private int _rows;
        private int _cols;
        private int[][] _dungeon;

        // paths with maximum of the minimum hp on the path
        private Dictionary<Coords, Path> _bestPathsWithEnding = new Dictionary<Coords, Path>();

        public void Solve()
        {
            int[][] dungeon2 = LoadDungeon();

            int[][] dungeon4 =
            {
                new [] {-2, -3, 3},
                new [] {-5, -10, 1},
                new [] {10, 30, -5},
            };

            int[][] dungeon =
            {
                new [] {1, -3, 3},
                new [] {0, -2, 0},
                new [] {-3, -3, -3},
            };

            int result = CalculateMinimumHP(dungeon);
        }

        public int CalculateMinimumHP(int[][] dungeon)
        {
            _dungeon = dungeon;
            _rows = dungeon.Length;
            _cols = dungeon[0].Length;

            int maxMinHp = int.MinValue;

            foreach (Path path in GeneratePaths())
            {
                Write("\n");
                WritePath(path);

                if (path.MinHp > maxMinHp)
                    maxMinHp = path.MinHp;

                Write($"\tMinHp: {path.MinHp} \n");
            }

            int result = maxMinHp > 0 ? 1 : (-1 * maxMinHp + 1);

            WriteLine($"\nResult = {result}");

            return result;
        }

        List<Path> GeneratePaths()
        {
            List<Path> result = new List<Path>();
            Queue<Path> queue = new Queue<Path>();

            // Add first cell as path
            Path path0 = new Path();
            path0.Coordinates.Add(new Coords(0, 0));
            path0.Hp = _dungeon[0][0];
            queue.Enqueue(path0);

            // Breadth-first search
            while (queue.Count > 0)
            {
                //Write($"{queue.Count}  ");
                Path path = queue.Dequeue();

                Coords lastCoords = path.Coordinates[path.Coordinates.Count - 1];

                // If there's a better path to this cell, don't expand the current one.
                if (_bestPathsWithEnding.TryGetValue(lastCoords, out Path bestPath) && bestPath.MinHp > path.MinHp)
                {
                    WriteLine($"Path is not good enough, skipping: {PathToString(path)}  ({bestPath.MinHp} > {path.MinHp})");
                    WriteLine($"Best: {PathToString(bestPath)}");
                    continue;
                }

                if (path.Coordinates.Count == _rows + _cols - 1)
                    result.Add(path);

                if (lastCoords.Col < _cols - 1)
                {
                    Coords newCoords = new Coords(lastCoords.Row, lastCoords.Col + 1);
                    int dungeonValue = _dungeon[lastCoords.Row][lastCoords.Col + 1];
                    Path newPath = new Path(path, newCoords, dungeonValue);
                    AddToQueue(queue, newCoords, newPath);
                }

                if (lastCoords.Row < _rows - 1)
                {
                    Coords newCoords = new Coords(lastCoords.Row + 1, lastCoords.Col);
                    int dungeonValue = _dungeon[lastCoords.Row + 1][lastCoords.Col];
                    Path newPath = new Path(path, newCoords, dungeonValue);
                    AddToQueue(queue, newCoords, newPath);
                }
            }

            return result;
        }

        private void AddToQueue(Queue<Path> queue, Coords newCoords, Path newPath)
        {
            if (newCoords == null) return;

            // If there's a better path to this cell, don't add the current one.
            if (_bestPathsWithEnding.TryGetValue(newCoords, out Path existingBestPath) && existingBestPath.MinHp > newPath.MinHp)
            {
                WriteLine($"Path doesn't beat best:\t\t\t{PathToString(newPath)}");
                return;
            }

            WriteLine($"Path is best, adding to queue:\t{PathToString(newPath)}");
            _bestPathsWithEnding[newCoords] = newPath;
            queue.Enqueue(newPath);
        }

        public static List<T> CopyAndAdd<T>(List<T> sourceList, T newElement)
        {
            List<T> result = new List<T>(sourceList.Count + 1);

            foreach (T elem in sourceList)
            {
                result.Add(elem);
            }

            result.Add(newElement);
            return result;
        }

        private class Path
        {
            public Path()
            {
            }

            public Path(Path other, Coords newCoords, int dungeonValue)
            {
                Coordinates = CopyAndAdd(other.Coordinates, newCoords);
                MinHp = other.MinHp;
                Hp = other.Hp + dungeonValue;
            }

            public readonly List<Coords> Coordinates = new List<Coords>();

            private int _hp;

            public int Hp
            {
                get => _hp;

                set
                {
                    _hp = value;
                    if (_hp < MinHp)
                        MinHp = _hp;
                }
            }

            public int MinHp { get; private set; } = int.MaxValue;

            public override bool Equals(object obj)
            {
                Path otherPath = obj as Path;
                if (otherPath == null) return false;
                if (Coordinates.Count != otherPath.Coordinates.Count) return false;

                for (int i = 0; i < Coordinates.Count; i++)
                    if (!Coordinates[i].Equals(otherPath.Coordinates[i]))
                        return false;

                return true;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        private class Coords
        {
            public Coords(int row, int col)
            {
                Row = row;
                Col = col;
            }

            public int Row { get; }

            public int Col { get; }

            public override int GetHashCode()
            {
                return (Row << 20) + Col;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Coords otherCoords))
                    return false;

                return otherCoords.Col == Col && otherCoords.Row == Row;
            }
        }

        private int[][] LoadDungeon()
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

        private void WritePath(Path path)
        {
            Write(PathToString(path));
        }

        private string PathToString(Path path)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Coords coords in path.Coordinates)
            {
                sb.Append($"{_dungeon[coords.Row][coords.Col].ToString().PadLeft(4)} ");
            }

            sb.Append($"({path.MinHp}) ");

            return sb.ToString();
        }
    }
}
