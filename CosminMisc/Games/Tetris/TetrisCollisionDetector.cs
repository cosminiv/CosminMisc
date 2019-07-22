using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Tetris
{
    class TetrisCollisionDetector
    {
        internal bool CanMovePieceDown(TetrisPieceWithPosition piece, int boardRowCount, TetrisFixedBricks fixedBricks) {
            if (ReachedBoardBottom(piece, boardRowCount))
                return false;

            if (ReachedFixedBrick(piece, fixedBricks, 1))
                return false;

            return true;
        }

        bool ReachedBoardBottom(TetrisPieceWithPosition piece, int boardRowCount) {
            int pieceBottomRow = piece.Position.Row + piece.Piece.TopPadding + piece.Piece.Height - 1;
            bool result = pieceBottomRow >= boardRowCount - 1;
            return result;
        }

        public bool ReachedFixedBrick(TetrisPieceWithPosition piece, TetrisFixedBricks fixedBricks,
            int fixedBrickRowOffset) {
            for (int column = 0; column < piece.Piece.MaxWidth; column++) {
                for (int row = piece.Piece.MaxHeight - 1; row >= 0; row--) {
                    bool isPieceBrick = piece.Piece[row, column] != null;
                    if (isPieceBrick) {
                        int rowRelativeToBoard = row + piece.Position.Row + fixedBrickRowOffset;
                        int columnRelativeToBoard = column + piece.Position.Column;
                        bool isFixedBrickUnderneath = fixedBricks.IsBrick(rowRelativeToBoard, columnRelativeToBoard);

                        if (isFixedBrickUnderneath)
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
