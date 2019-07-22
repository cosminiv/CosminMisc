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
            MakeNewPiece();
        }

        public void StickPiece() {
            Logger.WriteLine($"Can't move piece down; sticking at row {CurrentPiece.Position.Row}");
            FixedBricks.AddPiece(CurrentPiece);
            Logger.WriteLine(FixedBricks.ToString());
        }

        private TetrisPieceWithPosition MakePiece() {
            TetrisPiece piece = PieceFactory.MakePiece();
            int column = Random.Next(Columns - piece.Width);
            Logger.WriteLine($"{piece.ToString()}");
            Logger.WriteLine($"Column {column}");

            TetrisPieceWithPosition pieceWithPosition = new TetrisPieceWithPosition {
                Piece = piece,
                Position = new Coordinates(0, column)
            };

            return pieceWithPosition;
        }

        internal bool CanMovePieceDown() {
            return CollisionDetector.CanMovePieceDown(CurrentPiece, Rows, FixedBricks);
        }

        internal bool MakeNewPiece() {
            TetrisPieceWithPosition piece = MakePiece();

            if (CollisionDetector.ReachedFixedBrick(piece, FixedBricks, 0))
                return false;
            else {
                CurrentPiece = piece;
                return true;
            }
        }

        internal void MovePieceDown() {
            CurrentPiece.Position.Row++;
            Logger.WriteLine($"Moved piece to row {CurrentPiece.Position.Row}");
        }
    }
}
