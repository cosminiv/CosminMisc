using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.DTO
{
    class TryRotatePieceResult
    {
        public bool Rotated { get; set; }
        public TetrisState State { get; set; }
    }
}
