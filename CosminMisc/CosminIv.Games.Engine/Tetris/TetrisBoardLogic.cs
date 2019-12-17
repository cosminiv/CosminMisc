using System;
using CosminIv.Games.Engine.Common;
using CosminIv.Games.Engine.Common.Logging;
using CosminIv.Games.Engine.Tetris.DTO;

namespace CosminIv.Games.Engine.Tetris
{
    class TetrisBoardLogic
    {
        int Rows { get; }
        int Columns { get; }
        TetrisPieceWithPosition CurrentPiece;
        TetrisPiece NextPiece;
        TetrisCollisionDetector CollisionDetector;
        TetrisPieceFactory PieceFactory = new TetrisPieceFactory();
        TetrisFixedBricksLogic FixedBricksLogic;
        Random Random = new Random();
        readonly ILogger Logger;
        readonly object PieceMoveLock = new object();

        public TetrisBoardLogic(TetrisEngineSettings settings) {
            Logger = settings.Logger;
            Rows = settings.Rows;
            Columns = settings.Columns;
            FixedBricksLogic = new TetrisFixedBricksLogic(Rows, Columns, settings.RowsWithFixedBricks);
            CollisionDetector = new TetrisCollisionDetector(FixedBricksLogic, Rows, Columns);
            MakeNewPiece();
        }

        internal TryMovePieceResult TryMovePiece(TetrisState state, int rowDelta, int columnDelta) {
            lock (PieceMoveLock) {
                SetState(state);

                TryMovePieceResult result = new TryMovePieceResult();
                bool canMove = CanMovePiece(rowDelta, columnDelta);

                if (canMove) {
                    MovePiece(CurrentPiece, rowDelta, columnDelta);
                    result.Moved = true;
                }
                else {
                    bool tryingToMoveDown = rowDelta > 0;
                    if (tryingToMoveDown) {
                        result.DeletedRows = StickPiece();
                        MakeNewPiece();
                        if (CurrentPiece == null)
                            result.IsGameEnd = true;
                    }
                }

                return result;
            }
        }

        internal TryMovePieceResult MovePieceAllTheWayDownAndStick(TetrisState state) {
            lock (PieceMoveLock) {
                SetState(state);

                int rowDeltaMoved = MovePieceAllTheWayDown(CurrentPiece);
                int deletedRows = StickPiece();
                MakeNewPiece();

                TryMovePieceResult result = new TryMovePieceResult {
                    Moved = rowDeltaMoved > 0,
                    DeletedRows = deletedRows,
                    IsGameEnd = CurrentPiece == null
                };

                return result;
            }
        }

        int MovePieceAllTheWayDown(TetrisPieceWithPosition piece) {
            int rowDelta = 1;

            while (CollisionDetector.CanMovePiece(piece, rowDelta, 0))
                rowDelta++;

            rowDelta--;
            MovePiece(piece, rowDelta, 0);
            return rowDelta;
        }

        internal TryRotatePieceResult TryRotatePiece(TetrisState state) {
            TryRotatePieceResult result = new TryRotatePieceResult();

            lock (PieceMoveLock) {
                SetState(state);

                if (CanRotatePiece()) {
                    RotatePiece();
                    result.Rotated = true;
                }
            }

            return result;
        }

        private void RotatePiece() {
            CurrentPiece.Piece.Rotate90DegreesClockwise();
        }

        internal int StickPiece() {
            FixedBricksLogic.AddPiece(CurrentPiece);
            TetrisFixedBricksState fixedBricks = FixedBricksLogic.DeleteFullRows();
            CurrentPiece = null;
            return fixedBricks.DeletedRows;
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

        private void MakeNewPiece() {
            TetrisPieceWithPosition pieceWithPos = MakePieceWithPosition();

            if (CollisionDetector.IsCollision(pieceWithPos))
                CurrentPiece = null;
            else {
                CurrentPiece = pieceWithPos;
            }
        }

        internal void MovePiece(TetrisPieceWithPosition piece, int rowDelta, int columnDelta) {
            piece.Position.Row += rowDelta;
            piece.Position.Column += columnDelta;
        }

        public TetrisState GetState() {
            TetrisState state = new TetrisState(Rows, Columns);
            FixedBricksLogic.CopyFixedBricksInState(state);
            if (CurrentPiece != null)
                state.CurrentPiece = (TetrisPieceWithPosition)CurrentPiece.Clone();
            state.NextPiece = (TetrisPiece)NextPiece.Clone();
            return state;
        }

        private void SetState(TetrisState state) {
            FixedBricksLogic.CopyFixedBricksFromState(state);
            if (state.CurrentPiece != null)
                CurrentPiece = (TetrisPieceWithPosition)state.CurrentPiece.Clone();
            else
                CurrentPiece = null;
        }

    }
}
