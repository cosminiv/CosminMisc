using CosminIv.Games.Common;

namespace CosminIv.Games.Tetris.DTO
{
    public class TetrisPieceWithPosition
    {
        public TetrisPiece Piece { get; set; }
        public TetrisPiece NextPiece { get; set; }
        public Coordinates Position { get; set; } = new Coordinates(0, 0);

        public void CopyFrom(TetrisPieceWithPosition other) {
            this.Piece = (TetrisPiece)other.Piece.Clone();
            this.Position.CopyFrom(other.Position);
        }
    }
}
