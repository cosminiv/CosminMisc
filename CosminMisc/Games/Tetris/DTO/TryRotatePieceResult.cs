using CosminIv.Games.Tetris.DTO.EventArg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.DTO
{
    class TryRotatePieceResult
    {
        public bool Rotated { get; set; }
        public PieceRotatedArgs PieceRotatedArgs { get; set; }
    }
}
