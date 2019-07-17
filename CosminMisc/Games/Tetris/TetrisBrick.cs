using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Tetris
{
    public class TetrisBrick
    {
        public TetrisColor Color { get; set; }
    }

    class TetrisBrickWithPosition
    {
        public TetrisBrick Brick { get; set; }
        public TetrisPosition Position { get; set; }
    }

    public class TetrisPosition
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
