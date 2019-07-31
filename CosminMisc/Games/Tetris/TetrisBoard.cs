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
                result.Moved = false;

                bool canMove = CanMovePiece(rowDelta, columnDelta);

                if (canMove) {
                    PieceMovedArgs pieceMovedArgs = MovePiece(rowDelta, columnDelta);
                    result.Moved = true;
                    result.PieceMovedArgs = pieceMovedArgs;
                }
                else {
                    bool tryingToMoveDown = rowDelta > 0;
                    if (tryingToMoveDown)
                        result.FixedBricks = StickPiece();
                }

                return result;
            }
        }

        internal TryMovePieceResult MovePieceAllTheWayDownAndStick() {
            lock (PieceMoveLock) {
                TryMovePieceResult result = new TryMovePieceResult();
                int rowDelta = 1;

                while (CollisionDetector.CanMovePiece(CurrentPiece, rowDelta, 0))
                    rowDelta++;

                rowDelta--;

                PieceMovedArgs pieceMovedArgs = MovePiece(rowDelta, 0);
                result.Moved = true;
                result.PieceMovedArgs = pieceMovedArgs;
                result.FixedBricks = StickPiece();

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

        internal TetrisFixedBricksState StickPiece() {
            FixedBricks.AddPiece(CurrentPiece);
            TetrisFixedBricksState modifiedRows = FixedBricks.DeleteFullRows();

            return modifiedRows;
        }

        public TetrisFixedBricksState GetFixedBricks() {
            return FixedBricks.GetState();
        }

        private TetrisPieceWithPosition MakePiece() {
            TetrisPiece piece = PieceFactory.MakePiece();
            RotatePieceRandomly(piece);

            TetrisPieceWithPosition pieceWithPosition = new TetrisPieceWithPosition {
                Piece = piece,
                Position = new Coordinates(0, Columns / 2)
            };

            return pieceWithPosition;
        }

        private void RotatePieceRandomly(TetrisPiece piece) {
            int rotationCount = Random.Next(4);
            for (int i = 0; i <= rotationCount; i++) {
                piece.Rotate90DegreesClockwise();
            }
        }

        internal bool CanMovePiece(int rowDelta, int columnDelta) {
            return CollisionDetector.CanMovePiece(CurrentPiece, rowDelta, columnDelta);
        }

        private bool CanRotatePiece() {
            return CollisionDetector.CanRotatePiece(CurrentPiece);
        }

        internal bool MakeNewPiece(out TetrisPieceWithPosition piece) {
            piece = MakePiece();

            if (CollisionDetector.IsCollision(piece))
                return false;
            else {
                CurrentPiece = piece;
                return true;
            }
        }

        internal PieceMovedArgs MovePiece(int rowDelta, int columnDelta) {
            CurrentPiece.Position.Row += rowDelta;
            CurrentPiece.Position.Column += columnDelta;
            Coordinates pos = CurrentPiece.Position;

            PieceMovedArgs result = new PieceMovedArgs {
                Piece = CurrentPiece.Piece,
                OldCoordinates = new Coordinates(pos.Row - rowDelta, pos.Column - columnDelta),
                NewCoordinates = new Coordinates(pos.Row, pos.Column)
            };

            return result;
        }

        internal PieceRotatedArgs RotatePiece() {
            TetrisPiece pieceBeforeRotation = new TetrisPiece(CurrentPiece.Piece.MaxSize);
            pieceBeforeRotation.CopyFrom(CurrentPiece.Piece);

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
