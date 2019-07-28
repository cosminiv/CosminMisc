using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.EventArguments
{
    public class ScoreChangedArgs : EventArgs
    {
        public int NewScore;
        public int NewLines;
    }
}
