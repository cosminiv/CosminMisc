using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using CosminIv.Games.Common.Logging;
using CosminIv.Games.Common;

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

        public void StickPiece() {
            FixedBricks.AddPiece(CurrentPiece);
            //Logger.WriteLine(FixedBricks.ToString());
        }

        private TetrisPieceWithPosition MakePiece() {
            TetrisPiece piece = PieceFactory.MakePiece();
            int column = Random.Next(Columns - piece.Width);

            TetrisPieceWithPosition pieceWithPosition = new TetrisPieceWithPosition {
                Piece = piece,
                Position = new Coordinates(0, column)
            };

            return pieceWithPosition;
        }

        internal bool CanMovePiece(int rowDelta, int columnDelta) {
            return CollisionDetector.CanMovePiece(CurrentPiece, rowDelta, columnDelta);
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
    }
}
