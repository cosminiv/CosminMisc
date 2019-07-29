using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.EventArguments
{
    public class GameInitializedArgs : EventArgs
    {
        public TetrisFixedBricksState FixedBricks;
    }
}
