using CosminIv.Games.Common;
using CosminIv.Games.Common.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CosminIv.Games.Tetris.DTO
{
    public class TetrisPiece : ICloneable {
        TetrisBrick[][] Bricks;
        IColor _color = new ConsoleColor2(ConsoleColor.White);

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

        public object Clone() {
            TetrisPiece newPiece = new TetrisPiece(this.MaxSize);

            for (int rowIndex = 0; rowIndex < this.Bricks.Length; rowIndex++) {
                for (int colIndex = 0; colIndex < this.Bricks[rowIndex].Length; colIndex++) {
                    TetrisBrick brick = Bricks[rowIndex][colIndex];
                    newPiece.Bricks[rowIndex][colIndex] = (brick != null ? (TetrisBrick)brick.Clone() : null);
                }
            }

            newPiece.Color = (IColor)Color.Clone();

            return newPiece;
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

        public override bool Equals(object other) {

            if (!(other is TetrisPiece otherPiece))
                return false;

            if (otherPiece.MaxSize != this.MaxSize)
                return false;

            for (int row = 0; row < MaxSize; row++) {
                for (int col = 0; col < MaxSize; col++) {
                    TetrisBrick brick = Bricks[row][col];
                    TetrisBrick otherBrick = otherPiece.Bricks[row][col];

                    if (brick == null && otherBrick != null)
                        return false;

                    if (brick == null && otherBrick == null)
                        continue;

                    if (!brick.Equals(otherBrick))
                        return false;
                }
            }

            return true;
        }

        public override int GetHashCode() {
            unchecked {
                int hash = 19;
                for (int row = 0; row < MaxSize; row++) {
                    for (int col = 0; col < MaxSize; col++) {
                        TetrisBrick brick = Bricks[row][col];
                        int brickHash = brick != null ? brick.GetHashCode() : 0;
                        hash = hash * 31 + brickHash;
                    }
                }
                return hash;
            }
        }
    }
}
