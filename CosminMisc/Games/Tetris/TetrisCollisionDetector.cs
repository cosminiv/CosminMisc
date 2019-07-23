using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris
{
    class TetrisCollisionDetector
    {
        readonly TetrisFixedBricks FixedBricks;
        readonly int BoardRowCount;
        readonly int BoardColumnCount;

        public TetrisCollisionDetector(TetrisFixedBricks fixedBricks, int boardRowCount, int boardColumnCount) {
            FixedBricks = fixedBricks;
            BoardRowCount = boardRowCount;
            BoardColumnCount = boardColumnCount;
        }

        internal bool CanMovePiece(TetrisPieceWithPosition piece) {
            if (ReachedBoardBottom(piece))
                return false;

            if (ReachedFixedBrick(piece, 1))
                return false;

            return true;
        }

        bool ReachedBoardBottom(TetrisPieceWithPosition piece) {
            int pieceBottomRow = piece.Position.Row + piece.Piece.TopPadding + piece.Piece.Height - 1;
            bool result = pieceBottomRow >= BoardRowCount - 1;
            return result;
        }

        public bool ReachedFixedBrick(TetrisPieceWithPosition piece, int verticalDistanceToFixedBrick) {
            for (int column = 0; column < piece.Piece.MaxWidth; column++) {
                for (int row = piece.Piece.MaxHeight - 1; row >= 0; row--) {
                    bool isPieceBrick = piece.Piece[row, column] != null;
                    if (isPieceBrick) {
                        int rowRelativeToBoard = row + piece.Position.Row + verticalDistanceToFixedBrick;
                        int columnRelativeToBoard = column + piece.Position.Column;
                        bool isFixedBrickUnderneath = FixedBricks.IsBrick(rowRelativeToBoard, columnRelativeToBoard);

                        if (isFixedBrickUnderneath)
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
