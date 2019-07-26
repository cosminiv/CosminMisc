using CosminIv.Games.Common;
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
        readonly TetrisPieceWithPositionCopy PieceCopy;

        public TetrisCollisionDetector(TetrisFixedBricks fixedBricks, int boardRowCount, int boardColumnCount) {
            FixedBricks = fixedBricks;
            BoardRowCount = boardRowCount;
            BoardColumnCount = boardColumnCount;
            PieceCopy = new TetrisPieceWithPositionCopy();
        }

        internal bool CanMovePiece(TetrisPieceWithPosition piece, int rowDelta, int columnDelta) {
            TetrisPieceWithPosition pieceCopy = PreparePieceCopy(piece, rowDelta, columnDelta);

            if (IsCollision(pieceCopy))
                return false;

            return true;
        }

        private TetrisPieceWithPosition PreparePieceCopy(TetrisPieceWithPosition piece, int rowDelta, int columnDelta) {
            PieceCopy.CopyFrom(piece);
            PieceCopy.Value.Position.Row += rowDelta;
            PieceCopy.Value.Position.Column += columnDelta;
            return PieceCopy.Value;
        }

        public bool IsCollision(TetrisPieceWithPosition piece) {
            foreach (Coordinates brickCoord in GetBrickCoordinatesRelativeToBoard(piece)) {
                bool isLeftWallCollision = brickCoord.Column < 0;
                bool isRightWallCollision = brickCoord.Column >= BoardColumnCount;
                bool isBottomWallCollision = brickCoord.Row >= BoardRowCount;
                bool isFixedBrickCollision = FixedBricks.IsBrick(brickCoord.Row, brickCoord.Column);

                if (isLeftWallCollision || isRightWallCollision || isBottomWallCollision || isFixedBrickCollision)
                    return true;
            }

            return false;
        }

        IEnumerable<Coordinates> GetBrickCoordinatesRelativeToBoard(TetrisPieceWithPosition piece) {
            Coordinates coord = new Coordinates(0, 0);

            for (int column = 0; column < piece.Piece.MaxSize; column++) {
                for (int row = piece.Piece.MaxSize - 1; row >= 0; row--) {
                    bool isPieceBrick = piece.Piece[row, column] != null;

                    if (isPieceBrick) {
                        coord.Row = row + piece.Position.Row;
                        coord.Column = column + piece.Position.Column;

                        yield return coord;
                    }
                }
            }
        }
    }
}
