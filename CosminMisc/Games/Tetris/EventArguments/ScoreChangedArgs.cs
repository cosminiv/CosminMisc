using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.EventArguments
{
    public class ScoreChangedArgs : EventArgs
    {
        public int Score;
        public int LineCount;
    }
}
