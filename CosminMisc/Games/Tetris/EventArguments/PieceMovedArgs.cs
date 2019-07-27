using CosminIv.Games.Common;
using System;

namespace CosminIv.Games.Tetris.EventArguments
{
    public class PieceMovedArgs : EventArgs
    {
        public TetrisPiece Piece;
        public Coordinates OldCoordinates;
        public Coordinates NewCoordinates;
    }
}