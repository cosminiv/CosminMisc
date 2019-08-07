using CosminIv.Games.Common;
using System;

namespace CosminIv.Games.Tetris.DTO
{
    public class TetrisPieceWithPosition : ICloneable
    {
        public TetrisPiece Piece { get; set; }
        public TetrisPiece NextPiece { get; set; }
        public Coordinates Position { get; set; } = new Coordinates(0, 0);

        public object Clone() {
            TetrisPieceWithPosition clone = new TetrisPieceWithPosition {
                NextPiece = (TetrisPiece)NextPiece.Clone(),
                Piece = (TetrisPiece)Piece.Clone(),
                Position = (Coordinates)Position.Clone()
            };

            return clone;
        }

        public void CopyFrom(TetrisPieceWithPosition other) {
            this.Piece = (TetrisPiece)other.Piece.Clone();
            this.Position.CopyFrom(other.Position);
        }
    }
}
