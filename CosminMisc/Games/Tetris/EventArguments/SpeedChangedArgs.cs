using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.EventArguments
{
    public class SpeedChangedArgs : EventArgs
    {
        public int Speed { get; set; }
    }
}
