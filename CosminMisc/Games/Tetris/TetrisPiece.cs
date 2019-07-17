using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Tetris
{
    internal class TetrisPiece {
        public static readonly int MaxSize = 4;

        internal bool[][] Bricks = MakeBricksMatrix();

        public bool this[int row, int col] => Bricks[row][col];

        private static bool[][] MakeBricksMatrix() {
            bool[][] result = new bool[MaxSize][];
            for (int i = 0; i < result.Length; i++) {
                result[i] = new bool[MaxSize];
            }
            return result;
        }

        internal void CopyFrom(TetrisPiece other) {
            for (int rowIndex = 0; rowIndex < Bricks.Length; rowIndex++) {
                Array.Copy(other.Bricks[rowIndex], this.Bricks[rowIndex], this.Bricks[rowIndex].Length);
            }
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            for (int rowIndex = 0; rowIndex < Bricks.Length; rowIndex++) {
                for (int colIndex = 0; colIndex < Bricks[rowIndex].Length; colIndex++) {
                    sb.Append(Bricks[rowIndex][colIndex] ? "#" : " ");
                }

                if (rowIndex < Bricks.Length - 1)
                    sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
