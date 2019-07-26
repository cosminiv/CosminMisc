using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Tetris
{
    public class TetrisFullRowsDeletedResult
    {
        public List<int> DeletedRowsIndexes { get; set; }
        public int ModifiedRowsStartIndex { get; set; }
        public TetrisBrick[][] ModifiedRows { get; set; } = new TetrisBrick[0][];
    }
}
