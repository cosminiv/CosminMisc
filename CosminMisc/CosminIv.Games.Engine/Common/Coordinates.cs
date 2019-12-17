using System;

namespace CosminIv.Games.Engine.Common
{
    public class Coordinates : ICloneable
    {
        public Coordinates(int row, int column) {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }

        public object Clone() {
            return new Coordinates(Row, Column);
        }

        public void CopyFrom(Coordinates other) {
            this.Row = other.Row;
            this.Column = other.Column;
        }
    }
}
