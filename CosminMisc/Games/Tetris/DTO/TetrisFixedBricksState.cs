using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris.DTO
{
    public class TetrisFixedBricksState
    {
        public int DeletedRows { get; set; }
        public int RowsStartIndex { get; set; }
        public List<TetrisBrick[]> Rows { get; set; } = new List<TetrisBrick[]>();
    }
}
