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
        TetrisCollisionDetector CollisionDetector = new TetrisCollisionDetector();
        TetrisPieceFactory PieceFactory = new TetrisPieceFactory();
        TetrisFixedBricks FixedBricks;
        Random Random = new Random();
        ILogger Logger;

        public TetrisBoard(int rows, int columns, ILogger logger) {
            Logger = logger;
            Rows = rows;
            Columns = columns;
            FixedBricks = new TetrisFixedBricks(rows, columns);
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

        internal bool CanMovePieceDown() {
            return CollisionDetector.CanMovePieceDown(CurrentPiece, Rows, FixedBricks);
        }

        internal bool MakeNewPiece(out TetrisPieceWithPosition piece) {
            piece = MakePiece();

            if (CollisionDetector.ReachedFixedBrick(piece, FixedBricks, 0))
                return false;
            else {
                CurrentPiece = piece;
                return true;
            }
        }

        internal PieceMovedArgs MovePieceDown() {
            CurrentPiece.Position.Row++;
            Coordinates pos = CurrentPiece.Position;

            PieceMovedArgs result = new PieceMovedArgs {
                Piece = CurrentPiece.Piece,
                OldCoordinates = new Coordinates(pos.Row - 1, pos.Column),
                NewCoordinates = new Coordinates(pos.Row, pos.Column)
            };

            return result;
        }
    }
}
