using System;
using CosminIv.Games.Engine.Common.Color;

namespace CosminIv.Games.Engine.Tetris.DTO
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

        public override bool Equals(object other) {
            if (!(other is TetrisBrick otherBrick))
                return false;

            if (!Color.Equals(otherBrick.Color))
                return false;

            return true;
        }

        public override int GetHashCode() {
            return Color.GetHashCode();
        }
    }
}
