using CosminIv.Games.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris
{
    public class TetrisBrick
    {
        public IColor Color { get; set; }

        public override string ToString() {
            return "#";
        }
    }

    class TetrisBrickWithPosition
    {
        public TetrisBrick Brick { get; set; }
        public Coordinates Position { get; set; }
    }
}
