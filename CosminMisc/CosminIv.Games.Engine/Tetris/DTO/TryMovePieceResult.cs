﻿namespace CosminIv.Games.Engine.Tetris.DTO
{
    internal class TryMovePieceResult {
        public bool Moved { get; set; }
        public int DeletedRows { get; set; }
        public bool IsGameEnd { get; set; }
    }
}
