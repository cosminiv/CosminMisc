﻿using System.Collections.Generic;

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

        public IList<int> Solve()
        {
            var matrix = new[]
            {
                new [] {1, 2, 3, 4},
                new [] {5, 6, 7, 8},
                new [] {9, 10, 11, 12},
            };

            IList<int> result = SpiralOrder(matrix);
            return result;
        }

        public IList<int> SpiralOrder(int[][] matrix)
        {
            bool ok = InitState(matrix);
            if (!ok) return new List<int>();

            bool couldMoveAtLeastOnce;

            do
            {
                couldMoveAtLeastOnce = false;

                while (_currentDirection == Direction.Right)
                {
                    bool couldMove = GoRight();
                    if (!couldMove) _currentDirection = Direction.Down;
                    couldMoveAtLeastOnce = couldMove || couldMoveAtLeastOnce;
                }

                while (_currentDirection == Direction.Down)
                {
                    bool couldMove = GoDown();
                    if (!couldMove) _currentDirection = Direction.Left;
                    couldMoveAtLeastOnce = couldMove || couldMoveAtLeastOnce;
                }

                while (_currentDirection == Direction.Left)
                {
                    bool couldMove = GoLeft();
                    if (!couldMove) _currentDirection = Direction.Up;
                    couldMoveAtLeastOnce = couldMove || couldMoveAtLeastOnce;
                }

                while (_currentDirection == Direction.Up)
                {
                    bool couldMove = GoUp();
                    if (!couldMove) _currentDirection = Direction.Right;
                    couldMoveAtLeastOnce = couldMove || couldMoveAtLeastOnce;
                }
            } while (couldMoveAtLeastOnce);

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
