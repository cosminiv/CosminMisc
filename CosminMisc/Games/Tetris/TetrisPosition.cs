namespace Games.Tetris
{
    public class TetrisPosition
    {
        public TetrisPosition(int row, int column) {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}
