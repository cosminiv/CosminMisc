using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.Tetris
{
    public class TetrisPieceFactory
    {
        static TetrisPiece[] _prototypePieces = MakePrototypePieces();
        static Random _random = new Random();

        public TetrisPiece MakePiece() {
            TetrisPiece piece = new TetrisPiece();
            int PrototypePieceIndex = _random.Next(_prototypePieces.Length);
            TetrisPiece prototypePiece = _prototypePieces[PrototypePieceIndex];
            piece.CopyFrom(prototypePiece);
            return piece;
        }

        private static TetrisPiece[] MakePrototypePieces() {
            TetrisPiece[] result = new[] {
                MakePrototypePiece1(),
                MakePrototypePiece2(),
                MakePrototypePiece3(),
                MakePrototypePiece4(),
                MakePrototypePiece5(),
                MakePrototypePiece6(),
                MakePrototypePiece7(),
            };
            return result;
        }

        private static TetrisPiece MakePieceFromBrickCoordinates((int, int)[] brickCoords) {
            TetrisPiece result = new TetrisPiece();
            foreach ((int, int) position in brickCoords) {
                result.Bricks[position.Item1][position.Item2] = new TetrisBrick();
            }
            return result;
        }

        private static TetrisPiece MakePrototypePiece1() {
            // ****
            (int, int)[] brickCoords = (new[] { (0, 0), (0, 1), (0, 2), (0, 3) });
            return MakePieceFromBrickCoordinates(brickCoords);
        }

        private static TetrisPiece MakePrototypePiece2() {
            // ***
            // *
            (int, int)[] brickCoords = (new[] { (0, 0), (0, 1), (0, 2), (1, 0) });
            return MakePieceFromBrickCoordinates(brickCoords);
        }

        private static TetrisPiece MakePrototypePiece3() {
            // ***
            //   *
            (int, int)[] brickCoords = (new[] { (0, 0), (0, 1), (0, 2), (1, 2) });
            return MakePieceFromBrickCoordinates(brickCoords);
        }

        private static TetrisPiece MakePrototypePiece4() {
            // ***
            //  *
            (int, int)[] brickCoords = (new[] { (0, 0), (0, 1), (0, 2), (1, 1) });
            return MakePieceFromBrickCoordinates(brickCoords);
        }

        private static TetrisPiece MakePrototypePiece5() {
            // **
            //  **
            (int, int)[] brickCoords = (new[] { (0, 0), (0, 1), (1, 1), (1, 2) });
            return MakePieceFromBrickCoordinates(brickCoords);
        }

        private static TetrisPiece MakePrototypePiece6() {
            //  **
            // **
            (int, int)[] brickCoords = (new[] { (0, 1), (0, 2), (1, 0), (1, 1) });
            return MakePieceFromBrickCoordinates(brickCoords);
        }

        private static TetrisPiece MakePrototypePiece7() {
            // **
            // **
            (int, int)[] brickCoords = (new[] { (0, 0), (0, 1), (1, 0), (1, 1) });
            return MakePieceFromBrickCoordinates(brickCoords);
        }
    }
}
