using CosminIv.Games.Common;
using CosminIv.Games.Common.Color;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.DTO
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
