using CosminIv.Games.Common.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.DTO
{
    public class TetrisEngineSettings
    {
        public int Rows;
        public int Columns;
        public int Speed;
        public ILogger Logger;
        public int RowsWithFixedBricks;
        public bool ShowGhost;
    }
}
