using System.Collections.Generic;
using System.Linq;
using CosminIv.Games.Engine.Common;
using CosminIv.Games.Engine.Tetris.DTO;

namespace CosminIv.Games.UI.Console.Tetris
{
    internal class TetrisStateDifferences
    {
        readonly int Rows;
        readonly int Columns;

        public TetrisStateDifferences(int rows, int columns) {
            Rows = rows;
            Columns = columns;
        }

        public List<TetrisBrickWithPosition> ComputeDifferences(TetrisState oldState, TetrisState newState) {
            List<TetrisBrickWithPosition> result = new List<TetrisBrickWithPosition>();
            ComputeCurrentPieceDifferences(oldState, newState, result);
            ComputeFixedBricksDifferences(oldState, newState, result);
            result = result.Where(b => IsInsideBoard(b.Position)).ToList();
            return result;
        }

        private bool IsInsideBoard(Coordinates pos) {
            bool result = pos.Row >= 0 && pos.Row < Rows && pos.Column >= 0 && pos.Column < Columns;
            return result;
        }

        private static void ComputeFixedBricksDifferences(TetrisState oldState, TetrisState newState, List<TetrisBrickWithPosition> diffList) {

            for (int row = 0; row < newState.Rows; row++) {
                for (int column = 0; column < newState.Columns; column++) {
                    TetrisBrick oldBrick = oldState?.FixedBricks[row][column];
                    TetrisBrick newBrick = newState.FixedBricks[row][column];

                    if (AreBricksDifferent(oldBrick, newBrick)) {
                        diffList.Add(new TetrisBrickWithPosition {
                            Brick = (newBrick != null) ? (TetrisBrick)newBrick.Clone() : null,
                            Position = new Coordinates(row, column)
                        });
                    }
                }
            }
        }

        private void ComputeCurrentPieceDifferences(TetrisState oldState, TetrisState newState, List<TetrisBrickWithPosition> diffList) {
            TetrisPieceWithPosition oldPieceWithPos = oldState?.CurrentPiece;
            TetrisPieceWithPosition newPieceWithPos = newState.CurrentPiece;

            AddNullsForOldPiece(diffList, oldPieceWithPos);

            if (newPieceWithPos == null)
                return;

            for (int row = 0; row < newPieceWithPos.Piece.MaxSize; row++) {
                for (int column = 0; column < newPieceWithPos.Piece.MaxSize; column++) {
                    int rowRelativeToBoard = row + newPieceWithPos.Position.Row;
                    int columnRelativeToBoard = column + newPieceWithPos.Position.Column;
                    TetrisBrick newBrick = newPieceWithPos.Piece[row, column];

                    if (newBrick != null)
                        diffList.Add(new TetrisBrickWithPosition {
                            Brick = (TetrisBrick)newBrick.Clone(),
                            Position = new Coordinates(rowRelativeToBoard, columnRelativeToBoard)
                        });
                }
            }
        }

        private static void AddNullsForOldPiece(List<TetrisBrickWithPosition> diffList, TetrisPieceWithPosition oldPieceWithPos) {
            if (oldPieceWithPos == null)
                return;

            for (int row = 0; row < oldPieceWithPos.Piece.MaxSize; row++) {
                for (int column = 0; column < oldPieceWithPos.Piece.MaxSize; column++) {
                    TetrisBrick oldBrick = oldPieceWithPos.Piece[row, column];

                    if (oldBrick != null) {
                        int rowRelativeToBoard = row + oldPieceWithPos.Position.Row;
                        int columnRelativeToBoard = column + oldPieceWithPos.Position.Column;

                        diffList.Add(new TetrisBrickWithPosition {
                            Brick = null,
                            Position = new Coordinates(rowRelativeToBoard, columnRelativeToBoard)
                        });
                    }
                }
            }
        }

        private static bool AreBricksDifferent(TetrisBrick oldBrick, TetrisBrick newBrick) {
            bool result =
                (oldBrick != null && newBrick == null) ||
                (oldBrick == null && newBrick != null) ||
                (oldBrick != null && newBrick != null && !oldBrick.Color.Equals(newBrick.Color));
            return result;
        }
    }
}
