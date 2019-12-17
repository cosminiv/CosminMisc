using CosminIv.Games.Engine.Common.Logging;

namespace CosminIv.Games.Engine.Tetris.DTO
{
    public class TetrisEngineSettings
    {
        public int Rows;
        public int Columns;
        public int Speed;
        public int RowsWithFixedBricks;
        public bool EnableTimer = true;
        public int FullLinesForLevelUp = 7;
        public ILogger Logger;
    }
}
