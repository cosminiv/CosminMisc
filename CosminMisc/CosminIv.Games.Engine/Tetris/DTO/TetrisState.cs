using System;
using CosminIv.Games.Engine.Common;

namespace CosminIv.Games.Engine.Tetris.DTO
{
    public class TetrisState : EventArgs
    {
        public TetrisBrick[][] FixedBricks;

        public TetrisState(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            FixedBricks = new MatrixUtil<TetrisBrick>().Init(rows, columns);
        }

        public int Rows { get; set; }
        public int Columns { get; set; }

        public int Score { get; set; }
        public int Lines { get; set; }
        public int Speed { get; set; }

        public TetrisPieceWithPosition CurrentPiece { get; set; }
        public TetrisPiece NextPiece { get; set; }

        public bool IsGameEnd { get; set; }
    }
}
