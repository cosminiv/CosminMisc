using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Tetris
{
    class TetrisFixedBricks
    {
        SortedDictionary<int, TetrisBrickWithPosition[]> _rowsOfBricks = new SortedDictionary<int, TetrisBrickWithPosition[]>();

        public IEnumerable<TetrisFixedLine> Lines {
            get {
                foreach (int row in _rowsOfBricks.Keys) {
                    TetrisFixedLine line = new TetrisFixedLine(_rowsOfBricks[row]);
                    yield return line;
                }
            }
        }

        public void AddPiece(TetrisPiece piece) {

        }
    }

    class TetrisFixedLine
    {
        TetrisBrickWithPosition[] _bricksWithPosition;

        public TetrisFixedLine(TetrisBrickWithPosition[] bricksWithPosition) {
            _bricksWithPosition = bricksWithPosition;
        }

        public IEnumerable<TetrisBrickWithPosition> BricksOnLine {
            get {
                foreach (TetrisBrickWithPosition brick in _bricksWithPosition) {
                    yield return brick;
                }
            }
        }
    }
}
