using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using CosminIv.Games.Common.Logging;
using CosminIv.Games.Common;
using CosminIv.Games.Tetris.DTO.EventArg;
using CosminIv.Games.Tetris.DTO;

namespace CosminIv.Games.Tetris
{
    class TetrisBoard
    {
        int Rows { get; }
        int Columns { get; }
        TetrisPieceWithPosition CurrentPiece;
        TetrisPiece NextPiece;
        TetrisCollisionDetector CollisionDetector;
        TetrisPieceFactory PieceFactory = new TetrisPieceFactory();
        TetrisFixedBricksLogic FixedBricks;
        Random Random = new Random();
        readonly ILogger Logger;
        readonly object PieceMoveLock = new object();

        public TetrisBoard(TetrisEngineSettings settings) {
            Logger = settings.Logger;
            Rows = settings.Rows;
            Columns = settings.Columns;
            FixedBricks = new TetrisFixedBricksLogic(Rows, Columns, settings.RowsWithFixedBricks);
            CollisionDetector = new TetrisCollisionDetector(FixedBricks, Rows, Columns);
        }

        internal TryMovePieceResult TryMovePiece(int rowDelta, int columnDelta) {
            lock (PieceMoveLock) {
                TryMovePieceResult result = new TryMovePieceResult();
                bool canMove = CanMovePiece(rowDelta, columnDelta);

                if (canMove) {
                    MovePiece(rowDelta, columnDelta);
                    result.Moved = true;
                }
                else {
                    bool tryingToMoveDown = rowDelta > 0;
                    if (tryingToMoveDown)
                        result.DeletedRows = StickPiece();
                }

                result.State = GetState();
                return result;
            }
        }

        internal TryMovePieceResult MovePieceAllTheWayDownAndStick() {
            lock (PieceMoveLock) {
                int rowDelta = 1;

                while (CollisionDetector.CanMovePiece(CurrentPiece, rowDelta, 0))
                    rowDelta++;

                rowDelta--;

                MovePiece(rowDelta, 0);

                int deletedRows = StickPiece();

                TryMovePieceResult result = new TryMovePieceResult {
                    DeletedRows = deletedRows,
                    State = GetState()
                };

                return result;
            }
        }

        internal TryRotatePieceResult TryRotatePiece() {
            TryRotatePieceResult result = new TryRotatePieceResult();

            lock (PieceMoveLock) {
                if (CanRotatePiece()) {
                    PieceRotatedArgs pieceRotatedArgs = RotatePiece();
                    result.Rotated = true;
                    result.PieceRotatedArgs = pieceRotatedArgs;
                }
            }

            return result;
        }

        internal int StickPiece() {
            FixedBricks.AddPiece(CurrentPiece);
            TetrisFixedBricksState modifiedRows = FixedBricks.DeleteFullRows();

            return modifiedRows.DeletedRowsIndexes.Count();
        }

        public TetrisFixedBricksState GetFixedBricks() {
            return FixedBricks.GetState();
        }

        private TetrisPieceWithPosition MakePieceWithPosition() {
            TetrisPieceWithPosition result = new TetrisPieceWithPosition();

            if (NextPiece != null)
                result.Piece = NextPiece;
            else
                result.Piece = PieceFactory.MakePiece();

            NextPiece = PieceFactory.MakePiece();
            result.NextPiece = NextPiece;
            result.Position = new Coordinates(0, Columns / 2);

            return result;
        }

        internal bool CanMovePiece(int rowDelta, int columnDelta) {
            return CollisionDetector.CanMovePiece(CurrentPiece, rowDelta, columnDelta);
        }

        private bool CanRotatePiece() {
            return CollisionDetector.CanRotatePiece(CurrentPiece);
        }

        internal TetrisPieceWithPosition MakeNewPiece() {
            TetrisPieceWithPosition pieceWithPos = MakePieceWithPosition();

            if (CollisionDetector.IsCollision(pieceWithPos))
                return null;
            else {
                CurrentPiece = pieceWithPos;
                return pieceWithPos;
            }
        }

        internal void MovePiece(int rowDelta, int columnDelta) {
            CurrentPiece.Position.Row += rowDelta;
            CurrentPiece.Position.Column += columnDelta;
        }

        public TetrisState GetState() {
            TetrisState state = new TetrisState(Rows, Columns);
            CopyFixedBricksInState(state);
            CopyCurrentPieceInState(state);
            return state;
        }

        private void CopyFixedBricksInState(TetrisState state) {
            TetrisFixedBricksState fixedBricks = FixedBricks.GetState();

            for (int row = 0; row < fixedBricks.Rows.Count; row++) {
                for (int col = 0; col < Columns; col++) {
                    TetrisBrick fixedBrick = fixedBricks.Rows[row][col];
                    if (fixedBrick != null)
                        state.Bricks[row + fixedBricks.RowsStartIndex][col] = (TetrisBrick)fixedBrick.Clone();
                }
            }
        }

        private void CopyCurrentPieceInState(TetrisState state) {
            for (int row = 0; row < CurrentPiece.Piece.MaxSize; row++) {
                for (int col = 0; col < CurrentPiece.Piece.MaxSize; col++) {
                    int boardRow = row + CurrentPiece.Position.Row;
                    int boardCol = col + CurrentPiece.Position.Column;
                    TetrisBrick brick = CurrentPiece.Piece[row, col];

                    if (brick != null)
                        state.Bricks[boardRow][boardCol] = (TetrisBrick)brick.Clone();
                }
            }
        }

        internal PieceRotatedArgs RotatePiece() {
            TetrisPiece pieceBeforeRotation = (TetrisPiece)CurrentPiece.Piece.Clone();

            CurrentPiece.Piece.Rotate90DegreesClockwise();
            Coordinates pos = CurrentPiece.Position;

            PieceRotatedArgs result = new PieceRotatedArgs {
                Coordinates = CurrentPiece.Position,
                PieceBeforeRotation = pieceBeforeRotation,
                PieceAfterRotation = CurrentPiece.Piece
            };

            return result;
        }
    }
}
