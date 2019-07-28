using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CosminIv.Games.Tetris
{
    class TetrisFixedBricks
    {
        TetrisBrick[][] Bricks;
        TetrisFullRowsDeleter FullRowsDeleter;

        public TetrisFixedBricks(int rows, int columns) {
            InitalizeBrickMatrix(rows, columns);
            FullRowsDeleter = new TetrisFullRowsDeleter(Bricks);
        }

        private void InitalizeBrickMatrix(int rows, int columns) {
            Bricks = new TetrisBrick[rows][];
            for (int i = 0; i < rows; i++) {
                Bricks[i] = new TetrisBrick[columns];
            }
        }

        public bool IsBrick(int row, int column) {
            int columnCount = 0;
            if (Bricks.Length > 0)
                columnCount = Bricks[0].Length;

            bool areIndexesOutOfBounds = row < 0 || row >= Bricks.Length || column < 0 || column >= columnCount;
            if (areIndexesOutOfBounds)
                return false;

            bool result = Bricks[row][column] != null;

            return result;
        }

        public void AddPiece(TetrisPieceWithPosition pieceWithPos) {
            TetrisPiece piece = pieceWithPos.Piece;

            for (int row = 0; row < piece.MaxSize; row++) {
                for (int column = 0; column < piece.MaxSize; column++) {
                    TetrisBrick brick = piece[row, column];

                    if (brick != null) {
                        int rowRelativeToBoard = row + pieceWithPos.Position.Row;
                        int columnRelativeToBoard = column + pieceWithPos.Position.Column;
                        Bricks[rowRelativeToBoard][columnRelativeToBoard] = brick;
                    }
                }
            }
        }

        public TetrisModifiedRows DeleteFullRows() {
            TetrisModifiedRows result = FullRowsDeleter.DeleteFullRows();
            return result;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder(500);

            sb.Append('-', Bricks[0].Length + 2);
            sb.AppendLine();

            for (int row = 0; row < Bricks.Length; row++) {
                sb.Append("|");
                for (int column = 0; column < Bricks[row].Length; column++) {
                    TetrisBrick brick = Bricks[row][column];
                    sb.Append(brick != null ? brick.ToString() : " ");
                }
                sb.Append("|");
                sb.AppendLine();
            }

            sb.Append('-', Bricks[0].Length + 2);
            sb.AppendLine();

            return sb.ToString();
        }

    }
}
