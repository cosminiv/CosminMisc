using System.Collections.Generic;

namespace CosminIv.Games.Engine.Tetris.DTO
{
    public class TetrisFixedBricksState
    {
        public int DeletedRows { get; set; }
        public int RowsStartIndex { get; set; }
        public List<TetrisBrick[]> Rows { get; set; } = new List<TetrisBrick[]>();
    }
}
