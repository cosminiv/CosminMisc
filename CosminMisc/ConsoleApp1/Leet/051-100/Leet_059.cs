using System.Collections.Generic;

namespace ConsoleApp1.Leet
{
    class Leet_059
    {
        private int[][] _matrix;
        private int _currentRow;
        private int _currentColumn;
        private int _elemsAdded;

        public int[][] Solve()
        {
            int[][] d = GenerateMatrix(4);
            return d;
        }

        public int[][] GenerateMatrix(int n)
        {
            InitState(n);

            foreach ((Direction, int) move in GetNextMove(n))
            {
                MakeMove(move);
            }

            return _matrix;
        }

        private IEnumerable<(Direction, int)> GetNextMove(int n)
        {
            yield return (Direction.Right, n);

            int i = n - 1;
            if (i == 0) yield break;

            while (true)
            {
                yield return (Direction.Down, i);
                yield return (Direction.Left, i);
                if (--i == 0) break;

                yield return (Direction.Up, i);
                yield return (Direction.Right, i);
                if (--i == 0) break;
            }
        }

        private void MakeMove((Direction, int) move)
        {
            for (int i = 0; i < move.Item2; i++)
            {
                if (move.Item1 == Direction.Right) GoRight();
                else if (move.Item1 == Direction.Down) GoDown();
                else if (move.Item1 == Direction.Left) GoLeft();
                else if (move.Item1 == Direction.Up) GoUp();
            }
        }

        private static int[][] InitMatrix(int n)
        {
            int[][] matrix = new int[n][];

            for (int i = 0; i < n; i++)
                matrix[i] = new int[n];
            return matrix;
        }

        private void InitState(int n)
        {
            _matrix = InitMatrix(n);
            _currentRow = 0;
            _currentColumn = -1;
        }

        private void GoRight()
        {
            _currentColumn++;
            VisitCurrent();
        }

        private void VisitCurrent()
        {
            ++_elemsAdded;
            _matrix[_currentRow][_currentColumn] = _elemsAdded;
        }

        private void GoDown()
        {
            _currentRow++;
            VisitCurrent();
        }

        private void GoLeft()
        {
            _currentColumn--;
            VisitCurrent();
        }

        private void GoUp()
        {
            _currentRow--;
            VisitCurrent();
        }

        enum Direction
        {
            Right,
            Down,
            Left,
            Up
        }
    }
}
