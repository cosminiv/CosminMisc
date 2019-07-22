using CosminIv.Games.Common;
using System;

namespace CosminIv.Games.Tetris
{
    public class PieceMovedArgs : EventArgs
    {
        public TetrisPiece Piece;
        public Coordinates OldCoordinates;
        public Coordinates NewCoordinates;
    }
}