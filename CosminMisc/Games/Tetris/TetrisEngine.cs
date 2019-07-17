using System;

namespace Games.Tetris
{
    public class TetrisEngine
    {
        public TetrisEngine() {
        }

        public int Rows { get; } = 20;
        public int Cols { get; } = 10;
        public int Speed { get; private set; } = 1;
        public int Score { get; private set; } = 0;

        TetrisPiece CurrentPiece = new TetrisPiece();

        //Methods
        //    MoveLeft
        //    MoveRight
        //    MoveDown
        //    Rotate

        //Events
        //    PieceChange
        //    PieceAppear
        //    ScoreChange
        //    LinesCollapse
        //    GameEnd
    }
}
