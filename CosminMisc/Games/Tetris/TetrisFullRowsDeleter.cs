using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CosminIv.Games.Tetris
{
    class TetrisFullRowsDeleter
    {
        private readonly TetrisBrick[][] Bricks;

        public TetrisFullRowsDeleter(TetrisBrick[][] bricks) {
            this.Bricks = bricks;
        }

        public void DeleteFullRows() {
            List<int> fullRowsIndexes = GetIndexesOfFullRows();
            DeleteFullRows(fullRowsIndexes);
        }

        private List<int> GetIndexesOfFullRows() {
            List<int> result = new List<int>();

            for (int rowIndex = Bricks.Length - 1; rowIndex >= 0; rowIndex--) {
                TetrisBrick[] row = Bricks[rowIndex];

                if (IsFullRow(row))
                    result.Add(rowIndex);
                else if (IsEmptyRow(row))
                    break;
            }

            return result;
        }

        private bool IsFullRow(TetrisBrick[] row) {
            return row.All(brick => brick != null);
        }

        private bool IsEmptyRow(TetrisBrick[] row) {
            return row.All(brick => brick == null);
        }

        private void DeleteFullRows(List<int> fullRowsIndexes) {
            int indexOfTopRowCopied = CopyRowsDown(fullRowsIndexes);
            DeleteConsecutiveRows(indexOfTopRowCopied, fullRowsIndexes.Count());
        }

        private int CopyRowsDown(List<int> fullRowsIndexes) {
            int indexOfLowestFullRow = fullRowsIndexes.Max();
            int rowDelta = 0;
            int rowIndex = 0;

            for (rowIndex = indexOfLowestFullRow; rowIndex >= 0; rowIndex--) {
                if (IsEmptyRow(Bricks[rowIndex]))
                    break;
                else if (fullRowsIndexes.Contains(rowIndex))
                    rowDelta++;
                else
                    CopyRow(rowIndex, rowIndex - rowDelta);
            }

            return rowIndex;
        }

        private void CopyRow(int sourceRowIndex, int destRowIndex) {
            for (int colIndex = 0; colIndex < Bricks[sourceRowIndex].Length; colIndex++) {
                Bricks[destRowIndex][colIndex] = Bricks[sourceRowIndex][colIndex];
            }
        }

        private void DeleteConsecutiveRows(int startRowIndex, int howMany) {
            for (int rowIndex = startRowIndex; rowIndex < startRowIndex + howMany; rowIndex++) {
                DeleteRow(rowIndex);
            }
        }

        private void DeleteRow(int rowIndex) {
            for (int columnIndex = 0; columnIndex < Bricks[rowIndex].Length; columnIndex++) {
                Bricks[rowIndex][columnIndex] = null;
            }
        }
    }
}
