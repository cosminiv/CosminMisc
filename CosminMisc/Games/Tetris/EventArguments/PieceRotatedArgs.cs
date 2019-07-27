using CosminIv.Games.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.EventArguments
{
    public class PieceRotatedArgs : EventArgs
    {
        public Coordinates Coordinates;
        public TetrisPiece PieceBeforeRotation;
        public TetrisPiece PieceAfterRotation;
    }
}
