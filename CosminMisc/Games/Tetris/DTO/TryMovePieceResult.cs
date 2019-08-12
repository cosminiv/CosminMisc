using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.DTO
{
    internal class TryMovePieceResult {
        public bool Moved { get; set; }
        public int DeletedRows { get; set; }
        public bool IsGameEnd { get; set; }
        //public TetrisState State { get; set; }
    }
}
