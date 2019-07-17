using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Tetris
{
    class TetrisCollisionDetector
    {
        internal bool CanMovePieceDown(TetrisPieceWithPosition piece, int boardRows) {
            // TODO verify fixed bricks
            int pieceBottomRow = piece.Position.Row + piece.Piece.TopPadding + piece.Piece.Height - 1;
            //Debug.Assert(pieceBottomRow <= Rows - 1);
            bool reachedBoardBottom = pieceBottomRow >= boardRows - 1;
            if (reachedBoardBottom)
                return false;

            return true;
        }
    }
}
