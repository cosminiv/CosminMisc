using CosminIv.Games.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.DTO
{
    public class TetrisBrick : ICloneable
    {
        public IColor Color { get; set; }

        public TetrisBrick() {
            Color = new ConsoleColor2(ConsoleColor.White);
        }

        public TetrisBrick(TetrisBrick other) {
            Color = (IColor)other.Color.Clone();
        }

        public object Clone() {
            return new TetrisBrick(this);
        }

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
