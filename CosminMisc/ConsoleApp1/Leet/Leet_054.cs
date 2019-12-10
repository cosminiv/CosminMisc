using System;
using System.Collections.Generic;

namespace ConsoleApp1.Leet
{
    public class Leet_054
    {
        private int[][] _matrix;
        private bool[][] _matrixVisited;
        private int _rows;
        private int _columns;
        private List<int> _result;
        private int _currentRow;
        private int _currentColumn;
        private Direction _currentDirection;

        public void Solve()
        {
            var matrix = new[]
            {
                new [] {1, 2, 3, 4},
                new [] {5, 6, 7, 8},
                new [] {9, 10, 11, 12},
            };

            IList<int> result = SpiralOrder(matrix);
        }

        public IList<int> SpiralOrder(int[][] matrix)
        {
            bool ok = InitState(matrix);
            if (!ok) return new List<int>();

            bool couldMoveRightAtLeastOnce, couldMoveDownAtLeastOnce, couldMoveLeftAtLeastOnce, couldMoveUpAtLeastOnce;

            do
            {
                couldMoveRightAtLeastOnce = false;
                couldMoveDownAtLeastOnce = false;
                couldMoveLeftAtLeastOnce = false;
                couldMoveUpAtLeastOnce = false;

                while (_currentDirection == Direction.Right)
                {
                    bool couldMove = GoRight();
                    couldMoveRightAtLeastOnce = couldMove || couldMoveRightAtLeastOnce;
                    if (!couldMove) _currentDirection = Direction.Down;
                }

                while (_currentDirection == Direction.Down)
                {
                    bool couldMove = GoDown();
                    couldMoveDownAtLeastOnce = couldMove || couldMoveDownAtLeastOnce;
                    if (!couldMove) _currentDirection = Direction.Left;
                }

                while (_currentDirection == Direction.Left)
                {
                    bool couldMove = GoLeft();
                    couldMoveLeftAtLeastOnce = couldMove || couldMoveLeftAtLeastOnce;
                    if (!couldMove) _currentDirection = Direction.Up;
                }

                while (_currentDirection == Direction.Up)
                {
                    bool couldMove = GoUp();
                    couldMoveUpAtLeastOnce = couldMove || couldMoveUpAtLeastOnce;
                    if (!couldMove) _currentDirection = Direction.Right;
                }
            } while (couldMoveRightAtLeastOnce || couldMoveDownAtLeastOnce || couldMoveLeftAtLeastOnce || couldMoveUpAtLeastOnce);

            return _result;
        }

        private bool InitState(int[][] matrix)
        {
            _matrix = matrix;
            _rows = matrix.Length;
            if (_rows > 0)
                _columns = matrix[0].Length;
            _result = new List<int>();
            _currentRow = 0;
            _currentColumn = -1;
            _currentDirection = Direction.Right;

            _matrixVisited = new bool[_rows][];
            for (int row = 0; row < _rows; row++)
            {
                _matrixVisited[row] = new bool[_columns];
            }

            return _columns > 0 && _rows > 0;
        }

        private bool GoRight()
        {
            if (_currentColumn == _columns - 1 || _matrixVisited[_currentRow][_currentColumn + 1]) return false;
            _currentColumn++;
            VisitCurrent();
            return true;
        }

        private void VisitCurrent()
        {
            _result.Add(_matrix[_currentRow][_currentColumn]);
            _matrixVisited[_currentRow][_currentColumn] = true;
        }

        private bool GoDown()
        {
            if (_currentRow == _rows - 1 || _matrixVisited[_currentRow + 1][_currentColumn]) return false;
            _currentRow++;
            VisitCurrent();
            return true;
        }

        private bool GoLeft()
        {
            if (_currentColumn == 0 || _matrixVisited[_currentRow][_currentColumn - 1]) return false;
            _currentColumn--;
            VisitCurrent();
            return true;
        }

        private bool GoUp()
        {
            if (_currentRow == 0 || _matrixVisited[_currentRow - 1][_currentColumn]) return false;
            _currentRow--;
            VisitCurrent();
            return true;
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
