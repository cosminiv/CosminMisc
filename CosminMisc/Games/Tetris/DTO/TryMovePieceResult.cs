using CosminIv.Games.Tetris.DTO.EventArg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.DTO
{
    internal class TryMovePieceResult
    {
        //public bool IsGameNotRunning { get; set; }
        public bool Moved { get; set; }
        public PieceMovedArgs PieceMovedArgs { get; set; }
        //public int FullRowCount { get; set; }
        public TetrisFixedBricksState FixedBricks;
    }
}
