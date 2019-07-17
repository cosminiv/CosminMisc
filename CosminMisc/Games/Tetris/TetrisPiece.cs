using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.Tetris
{
    public class TetrisPiece {
        internal TetrisBrick[][] Bricks;

        public TetrisPiece(int maxWidth, int maxHeight) {
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
            Bricks = MakeBricksMatrix();
        }

        public int MaxWidth { get; private set; }
        public int MaxHeight { get; private set; }

        public int Width {
            get {
                int result = Bricks.Max(row => row.Count(brick => brick != null));
                return result;
            }
        }

        public int Height {
            get {
                int result = Bricks.Count(row => row.Any(brick => brick != null));
                return result;
            }
        }

        public int TopPadding {
            get {
                int result = 0;

                for (int i = 0; i < Bricks.Length; i++) {
                    if (Bricks[i].All(brick => brick == null))
                        result++;
                    else
                        break;
                }

                return result;
            }
        }

        public TetrisBrick this[int row, int col] => Bricks[row][col];

        public TetrisColor Color { get; set; }

        private TetrisBrick[][] MakeBricksMatrix() {
            TetrisBrick[][] result = new TetrisBrick[MaxHeight][];
            for (int i = 0; i < result.Length; i++) {
                result[i] = new TetrisBrick[MaxWidth];
            }
            return result;
        }

        internal void CopyFrom(TetrisPiece other) {
            for (int rowIndex = 0; rowIndex < Bricks.Length; rowIndex++) {
                for (int colIndex = 0; colIndex < Bricks[rowIndex].Length; colIndex++) {
                    TetrisBrick otherBrick = other.Bricks[rowIndex][colIndex];
                    if (otherBrick != null)
                        this.Bricks[rowIndex][colIndex] = new TetrisBrick { Color = otherBrick.Color };
                }
            }
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();

            for (int rowIndex = 0; rowIndex < Bricks.Length; rowIndex++) {
                for (int colIndex = 0; colIndex < Bricks[rowIndex].Length; colIndex++) {
                    TetrisBrick brick = Bricks[rowIndex][colIndex];
                    sb.Append(brick != null ? brick.ToString() : " ");
                }

                if (rowIndex < Bricks.Length - 1)
                    sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    class TetrisPieceWithPosition
    {
        public TetrisPiece Piece { get; set; }
        public TetrisPosition Position { get; set; }
    }
}
