using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.Tetris
{
    class TetrisFixedBricks
    {
        TetrisBrick[][] Bricks;

        public TetrisFixedBricks(int rows, int columns) {
            Bricks = new TetrisBrick[rows][];
            for (int i = 0; i < rows; i++) {
                Bricks[i] = new TetrisBrick[columns];
            }
        }

        public bool IsBrick(int row, int column) {
            bool result = Bricks[row][column] != null;
            return result;
        }

        public void AddPiece(TetrisPieceWithPosition pieceWithPos) {
            for (int row = 0; row < pieceWithPos.Piece.MaxHeight; row++) {
                for (int column = 0; column < pieceWithPos.Piece.MaxWidth; column++) {
                    TetrisBrick brick = pieceWithPos.Piece.Bricks[row][column];

                    if (brick != null) {
                        int rowRelativeToBoard = row + pieceWithPos.Position.Row;
                        int columnRelativeToBoard = column + pieceWithPos.Position.Column;
                        Bricks[rowRelativeToBoard][columnRelativeToBoard] = brick;
                    }
                }
            }
        }

    }
}
