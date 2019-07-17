using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Games.Tetris
{
    class TetrisBoard
    {
        int Rows { get; }
        int Columns { get; }
        TetrisPieceWithPosition CurrentPiece;
        TetrisCollisionDetector CollisionDetector = new TetrisCollisionDetector();
        TetrisPieceFactory PieceFactory = new TetrisPieceFactory();
        TetrisFixedBricks FixedBricks;

        public TetrisBoard(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            FixedBricks = new TetrisFixedBricks(rows, columns);
            CurrentPiece = MakePiece();
        }

        public void FixPiece() {
            Console.WriteLine($"Can't move piece down; fixing at row {CurrentPiece.Position.Row}");
            FixedBricks.AddPiece(CurrentPiece);
        }

        private TetrisPieceWithPosition MakePiece() {
            TetrisPiece piece = PieceFactory.MakePiece();
            Console.WriteLine($"{piece.ToString()}");

            TetrisPieceWithPosition pieceWithPosition = new TetrisPieceWithPosition {
                Piece = piece,
                Position = new TetrisPosition(0, Columns / 2)
            };

            return pieceWithPosition;
        }

        internal bool CanMovePieceDown() {
            return CollisionDetector.CanMovePieceDown(CurrentPiece, Rows, FixedBricks);
        }

        internal void MakeNewPiece() {
            CurrentPiece = MakePiece();
        }

        internal void MovePieceDown() {
            CurrentPiece.Position.Row++;
            Console.WriteLine($"Moved piece to row {CurrentPiece.Position.Row}");
        }
    }
}
