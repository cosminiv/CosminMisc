using CosminIv.Games.Engine.Common;

namespace CosminIv.Games.Engine.Tetris.DTO
{
    public class TetrisBrickWithPosition
    {
        public TetrisBrick Brick { get; set; }
        public Coordinates Position { get; set; }

        public override string ToString() {
            string brickStr = Brick != null ? "#" : " ";
            return $"{brickStr} ({Position.Row}, {Position.Column})";
        }
    }
}
