using CosminIv.Games.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.DTO
{
    public class TetrisState : EventArgs
    {
        public TetrisBrick[][] Bricks;

        public TetrisState(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            Bricks = new MatrixUtil<TetrisBrick>().Init(rows, columns);
        }

        public int Rows { get; set; }
        public int Columns { get; set; }

        public int Score { get; set; }
        public int Lines { get; set; }
        public int Speed { get; set; }

        public TetrisPieceWithPosition CurrentPiece { get; set; }
        public TetrisPiece NextPiece { get; set; }

        //public bool HasStateChanged { get; set; }
        public bool IsGameEnd { get; set; }
    }
}
