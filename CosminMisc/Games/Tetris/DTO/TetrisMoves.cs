using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.DTO
{
    public class TetrisMoves
    {
        public List<TetrisMove> Moves { get; set; } = new List<TetrisMove>();

        public override string ToString() {
            return string.Join(" ", Moves);
        }
    }
}
