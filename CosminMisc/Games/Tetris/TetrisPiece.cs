using CosminIv.Games.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CosminIv.Games.Tetris
{
    public class TetrisPiece {
        TetrisBrick[][] Bricks;
        IColor _color;

        public TetrisPiece(int maxSize) {
            MaxSize = maxSize;
            Bricks = MakeBricksMatrix();
        }

        public int MaxSize { get; private set; }

        public TetrisBrick this[int row, int col] {
            get {
                return Bricks[row][col];
            }
            set {
                Bricks[row][col] = value;
            }
        }

        public IColor Color {
            get {
                return _color;
            }

            set {
                _color = value;

                for (int rowIndex = 0; rowIndex < Bricks.Length; rowIndex++) {
                    for (int colIndex = 0; colIndex < Bricks[rowIndex].Length; colIndex++) {
                        TetrisBrick brick = Bricks[rowIndex][colIndex];
                        if (brick != null)
                            brick.Color = value;
                    }
                }
            }
        }

        public void Rotate90DegreesClockwise() {
            if (Bricks.Length == 1)
                return;

            int n = Bricks.Length;

            for (int shell = 0; shell <= n / 2; shell++) {
                for (int i = 0; i < n - 2 * shell - 1; i++) {
                    var (row1, col1) = (shell, shell + i);
                    var (row2, col2) = (n - shell - 1 - i, shell);
                    var (row3, col3) = (n - shell - 1, n - shell - 1 - i);
                    var (row4, col4) = (shell + i, n - shell - 1);

                    TetrisBrick temp = Bricks[row1][col1];
                    Bricks[row1][col1] = Bricks[row2][col2];
                    Bricks[row2][col2] = Bricks[row3][col3];
                    Bricks[row3][col3] = Bricks[row4][col4];
                    Bricks[row4][col4] = temp;
                }
            }
        }

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
                    this.Bricks[rowIndex][colIndex] = other.Bricks[rowIndex][colIndex];
                }
            }

            Color = other.Color;
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
