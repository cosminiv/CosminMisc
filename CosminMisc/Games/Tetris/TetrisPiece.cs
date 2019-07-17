using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Tetris
{
    public class TetrisPiece {
        internal TetrisBrick[][] Bricks;
        public static readonly int MaxSize = 4;

        public TetrisPiece(int width, int height) {
            Width = width;
            Height = height;
            Bricks = MakeBricksMatrix();
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public TetrisBrick this[int row, int col] => Bricks[row][col];

        public TetrisColor Color { get; set; }

        private TetrisBrick[][] MakeBricksMatrix() {
            TetrisBrick[][] result = new TetrisBrick[Height][];
            for (int i = 0; i < result.Length; i++) {
                result[i] = new TetrisBrick[Width];
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
                    sb.Append(Bricks[rowIndex][colIndex] != null ? "#" : " ");
                }

                if (rowIndex < Bricks.Length - 1)
                    sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
