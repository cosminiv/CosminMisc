namespace CosminIv.Games.Common
{
    public class Coordinates
    {
        public Coordinates(int row, int column) {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }

        public void CopyFrom(Coordinates other) {
            this.Row = other.Row;
            this.Column = other.Column;
        }
    }
}
