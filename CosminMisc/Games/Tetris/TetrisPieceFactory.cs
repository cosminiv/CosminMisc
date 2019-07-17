using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Games.Tetris
{
    public class TetrisPieceFactory
    {
        static TetrisPiece[] _prototypePieces = MakePrototypePieces();
        static Random _random = new Random();

        public TetrisPiece MakePiece() {
            int prototypePieceIndex = _random.Next(_prototypePieces.Length);
            TetrisPiece prototypePiece = _prototypePieces[prototypePieceIndex];
            TetrisPiece piece = new TetrisPiece(prototypePiece.MaxWidth, prototypePiece.MaxHeight);
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

        private static TetrisPiece MakePieceFromBrickCoordinates(int width, int height, (int, int)[] brickCoords) {
            TetrisPiece result = new TetrisPiece(width, height);
            foreach ((int, int) position in brickCoords) {
                result.Bricks[position.Item1][position.Item2] = new TetrisBrick();
            }
            return result;
        }

        private static TetrisPiece MakePrototypePiece1() {
            // ****
            (int, int)[] brickCoords = (new[] { (1, 0), (1, 1), (1, 2), (1, 3) });
            return MakePieceFromBrickCoordinates(4, 4, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece2() {
            // ***
            // *
            (int, int)[] brickCoords = (new[] { (1, 0), (1, 1), (1, 2), (2, 0) });
            return MakePieceFromBrickCoordinates(3, 3, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece3() {
            // ***
            //   *
            (int, int)[] brickCoords = (new[] { (1, 0), (1, 1), (1, 2), (2, 2) });
            return MakePieceFromBrickCoordinates(3, 3, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece4() {
            // ***
            //  *
            (int, int)[] brickCoords = (new[] { (1, 0), (1, 1), (1, 2), (2, 1) });
            return MakePieceFromBrickCoordinates(3, 3, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece5() {
            // **
            //  **
            (int, int)[] brickCoords = (new[] { (1, 0), (1, 1), (2, 1), (2, 2) });
            return MakePieceFromBrickCoordinates(3, 3, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece6() {
            //  **
            // **
            (int, int)[] brickCoords = (new[] { (1, 1), (1, 2), (2, 0), (2, 1) });
            return MakePieceFromBrickCoordinates(3, 3, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece7() {
            // **
            // **
            (int, int)[] brickCoords = (new[] { (0, 0), (0, 1), (1, 0), (1, 1) });
            return MakePieceFromBrickCoordinates(2, 2, brickCoords);
        }
    }
}
