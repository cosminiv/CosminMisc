using System.Collections.Generic;

namespace CosminIv.Games.Engine.Tetris.DTO
{
    public class TetrisMoves
    {
        public List<TetrisMove> Moves { get; set; } = new List<TetrisMove>();

        public override string ToString() {
            return string.Join(" ", Moves);
        }
    }
}
