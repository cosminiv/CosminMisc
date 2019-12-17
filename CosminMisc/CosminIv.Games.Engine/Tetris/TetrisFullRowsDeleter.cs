using CosminIv.Games.Tetris.DTO;
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

        public TetrisFixedBricksState DeleteFullRows() {
            List<int> fullRowsIndexes = GetIndexesOfFullRows();
            TetrisFixedBricksState result = DeleteFullRows(fullRowsIndexes);
            return result;
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

        private TetrisFixedBricksState DeleteFullRows(List<int> fullRowsIndexes) {
            if (fullRowsIndexes.Count == 0) {
                return new TetrisFixedBricksState {
                    DeletedRows = fullRowsIndexes.Count,
                    Rows = new List<TetrisBrick[]>(),
                    RowsStartIndex = 0
                };
            }

            CopyRowsResult copyResult = CopyRowsDown(fullRowsIndexes);

            DeleteRows(copyResult.StartIndex, fullRowsIndexes.Count());

            return new TetrisFixedBricksState {
                DeletedRows = fullRowsIndexes.Count,
                RowsStartIndex = copyResult.StartIndex,
                Rows = Bricks.Where((rows, i) => i >= copyResult.StartIndex && i <= fullRowsIndexes.Max()).ToList()
            };
        }

        private CopyRowsResult CopyRowsDown(List<int> fullRowsIndexes) {
            if (fullRowsIndexes.Count == 0)
                return new CopyRowsResult { HowMany = 0, StartIndex = 0 };

            int indexOfLowestFullRow = fullRowsIndexes.Max();
            int rowDelta = 0;
            int rowIndex = 0;
            int rowCountCopied = 0;

            for (rowIndex = indexOfLowestFullRow; rowIndex >= 0; rowIndex--) {
                if (IsEmptyRow(Bricks[rowIndex]))
                    break;
                else if (fullRowsIndexes.Contains(rowIndex))
                    rowDelta++;
                else {
                    CopyRow(rowIndex, rowIndex + rowDelta);
                    rowCountCopied++;
                }
            }

            return new CopyRowsResult {
                StartIndex = rowIndex + 1,
                HowMany = rowCountCopied
            };
        }

        private void CopyRow(int sourceRowIndex, int destRowIndex) {
            for (int colIndex = 0; colIndex < Bricks[sourceRowIndex].Length; colIndex++) {
                Bricks[destRowIndex][colIndex] = Bricks[sourceRowIndex][colIndex];
            }
        }

        private void DeleteRows(int startRowIndex, int howMany) {
            for (int rowIndex = startRowIndex; rowIndex < startRowIndex + howMany; rowIndex++) {
                DeleteRow(rowIndex);
            }
        }

        private void DeleteRow(int rowIndex) {
            for (int columnIndex = 0; columnIndex < Bricks[rowIndex].Length; columnIndex++) {
                Bricks[rowIndex][columnIndex] = null;
            }
        }

        class CopyRowsResult
        {
            public int StartIndex;
            public int HowMany;
        }
    }
}
