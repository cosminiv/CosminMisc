using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using CosminIv.Games.Common.Logging;
using CosminIv.Games.Common;
using CosminIv.Games.Tetris.EventArguments;

namespace CosminIv.Games.Tetris
{
    class TetrisBoard
    {
        int Rows { get; }
        int Columns { get; }
        TetrisPieceWithPosition CurrentPiece;
        TetrisCollisionDetector CollisionDetector;
        TetrisPieceFactory PieceFactory = new TetrisPieceFactory();
        TetrisFixedBricks FixedBricks;
        Random Random = new Random();
        ILogger Logger;

        public TetrisBoard(int rows, int columns, ILogger logger) {
            Logger = logger;
            Rows = rows;
            Columns = columns;
            FixedBricks = new TetrisFixedBricks(rows, columns);
            CollisionDetector = new TetrisCollisionDetector(FixedBricks, Rows, Columns);
        }

        public int StickPiece() {
            FixedBricks.AddPiece(CurrentPiece);
            TetrisModifiedRows modifiedRows = FixedBricks.DeleteFullRows();
            int fullRowCount = modifiedRows.DeletedRowsIndexes.Count;

            if (fullRowCount > 0)
                RowsDeleted?.Invoke(modifiedRows);

            return fullRowCount;
        }

        public delegate void RowsDeletedHandler(TetrisModifiedRows rowsDeletedResult);
        public event RowsDeletedHandler RowsDeleted;

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

        internal bool CanRotatePiece() {
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
