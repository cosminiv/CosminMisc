using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris
{
    public class TetrisFixedBricksState
    {
        public List<int> DeletedRowsIndexes { get; set; } = new List<int>();
        public int RowsStartIndex { get; set; }
        public List<TetrisBrick[]> Rows { get; set; } = new List<TetrisBrick[]>();
    }
}
