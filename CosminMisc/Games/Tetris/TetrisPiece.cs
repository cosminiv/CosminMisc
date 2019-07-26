using CosminIv.Games.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CosminIv.Games.Tetris
{
    public class TetrisPiece {
        TetrisBrick[][] Bricks;

        public TetrisPiece(int maxSize) {
            MaxSize = maxSize;
            Bricks = MakeBricksMatrix();
        }

        public int MaxSize { get; private set; }

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

        public TetrisBrick this[int row, int col] {
            get {
                return Bricks[row][col];
            }
            set {
                Bricks[row][col] = value;
            }
        }

        public Color Color { get; set; }

        private TetrisBrick[][] MakeBricksMatrix() {
            TetrisBrick[][] result = new TetrisBrick[MaxSize][];
            for (int i = 0; i < result.Length; i++) {
                result[i] = new TetrisBrick[MaxSize];
            }
            return result;
        }

        internal void CopyFrom(TetrisPiece other) {
            for (int rowIndex = 0; rowIndex < this.Bricks.Length; rowIndex++) {
                for (int colIndex = 0; colIndex < this.Bricks[rowIndex].Length; colIndex++) {

                    TetrisBrick otherBrick = other.Bricks[rowIndex][colIndex];
                    if (otherBrick != null)
                        this.Bricks[rowIndex][colIndex] = otherBrick;
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
}
