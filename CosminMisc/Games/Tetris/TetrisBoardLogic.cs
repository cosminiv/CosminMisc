using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using CosminIv.Games.Common.Logging;
using CosminIv.Games.Common;
using CosminIv.Games.Tetris.DTO.EventArg;
using CosminIv.Games.Tetris.DTO;
using CosminIv.Games.Common.Color;

namespace CosminIv.Games.Tetris
{
    class TetrisBoard
    {
        int Rows { get; }
        int Columns { get; }
        TetrisPieceWithPosition CurrentPiece;
        TetrisPieceWithPosition GhostPiece;
        bool ShowGhost;
        readonly int MinGhostDistance = 7;
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
            ShowGhost = settings.ShowGhost;
            FixedBricks = new TetrisFixedBricksLogic(Rows, Columns, settings.RowsWithFixedBricks);
            CollisionDetector = new TetrisCollisionDetector(FixedBricks, Rows, Columns);
            MakeNewPiece();
        }

        internal TryMovePieceResult TryMovePiece(int rowDelta, int columnDelta) {
            lock (PieceMoveLock) {
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

                result.State = GetState();
                return result;
            }
        }

        internal TryMovePieceResult MovePieceAllTheWayDownAndStick() {
            lock (PieceMoveLock) {
                int rowDeltaMoved = MovePieceAllTheWayDown(CurrentPiece);
                int deletedRows = StickPiece();
                MakeNewPiece();

                TryMovePieceResult result = new TryMovePieceResult {
                    Moved = rowDeltaMoved > 0,
                    DeletedRows = deletedRows,
                    State = GetState(),
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

        internal TryRotatePieceResult TryRotatePiece() {
            TryRotatePieceResult result = new TryRotatePieceResult();

            lock (PieceMoveLock) {
                if (CanRotatePiece()) {
                    RotatePiece();
                    result.Rotated = true;
                    result.State = GetState();
                }
            }

            return result;
        }

        private void RotatePiece() {
            CurrentPiece.Piece.Rotate90DegreesClockwise();
            ComputeGhost();
        }

        internal int StickPiece() {
            FixedBricks.AddPiece(CurrentPiece);
            TetrisFixedBricksState fixedBricks = FixedBricks.DeleteFullRows();
            CurrentPiece = null;
            return fixedBricks.DeletedRows;
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

        private void MakeNewPiece() {
            TetrisPieceWithPosition pieceWithPos = MakePieceWithPosition();

            if (CollisionDetector.IsCollision(pieceWithPos))
                CurrentPiece = null;
            else {
                CurrentPiece = pieceWithPos;
                ComputeGhost();
            }
        }

        internal void MovePiece(TetrisPieceWithPosition piece, int rowDelta, int columnDelta) {
            piece.Position.Row += rowDelta;
            piece.Position.Column += columnDelta;

            if (piece != GhostPiece)
                ComputeGhost();
        }

        private void ComputeGhost() {
            if (!ShowGhost)
                return;
            GhostPiece = (TetrisPieceWithPosition)CurrentPiece.Clone();
            // TODO: remove console color (not general enough)
            GhostPiece.Piece.Color = new ConsoleColor2(ConsoleColor.DarkGray);   
            int rowDelta = MovePieceAllTheWayDown(GhostPiece);
            if (rowDelta < MinGhostDistance)
                GhostPiece = null;
        }

        public TetrisState GetState() {
            TetrisState state = new TetrisState(Rows, Columns);
            CopyFixedBricksInState(state);
            CopyPieceInState(CurrentPiece, state);
            CopyPieceInState(GhostPiece, state);
            state.NextPiece = NextPiece;
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

        private void CopyPieceInState(TetrisPieceWithPosition pieceWithPos, TetrisState state) {
            if (pieceWithPos == null)
                return;

            for (int row = 0; row < pieceWithPos.Piece.MaxSize; row++) {
                for (int col = 0; col < pieceWithPos.Piece.MaxSize; col++) {
                    int boardRow = row + pieceWithPos.Position.Row;
                    int boardCol = col + pieceWithPos.Position.Column;
                    TetrisBrick brick = pieceWithPos.Piece[row, col];

                    if (brick != null)
                        state.Bricks[boardRow][boardCol] = (TetrisBrick)brick.Clone();
                }
            }
        }
    }
}
