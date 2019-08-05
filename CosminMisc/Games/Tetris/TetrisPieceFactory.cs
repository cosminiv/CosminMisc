using CosminIv.Games.Common;
using CosminIv.Games.Tetris.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CosminIv.Games.Tetris
{
    public class TetrisPieceFactory
    {
        TetrisPiece[] _prototypePieces = MakePrototypePieces();
        ConsoleColorFactory _colorFactory = new ConsoleColorFactory();
        Random _random = new Random();

        public TetrisPiece MakePiece() {
            TetrisPiece piece = MakeRandomPiece();
            return piece;
        }

        private TetrisPiece MakeRandomPiece() {
            int pieceIndex = _random.Next(_prototypePieces.Length);
            TetrisPiece prototypePiece = _prototypePieces[pieceIndex];
            TetrisPiece piece = (TetrisPiece)prototypePiece.Clone();

            piece.Color = _colorFactory.MakeRandomColor();
            RotatePieceRandomly(piece);

            return piece;
        }

        private void RotatePieceRandomly(TetrisPiece piece) {
            int rotationCount = _random.Next(4);
            for (int i = 0; i <= rotationCount; i++) {
                piece.Rotate90DegreesClockwise();
            }
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

        private static TetrisPiece MakePieceFromBrickCoordinates(int size, (int, int)[] brickCoords) {
            TetrisPiece result = new TetrisPiece(size);
            foreach ((int, int) position in brickCoords) {
                result[position.Item1, position.Item2] = new TetrisBrick();
            }
            return result;
        }

        private static TetrisPiece MakePrototypePiece1() {
            // ****
            (int, int)[] brickCoords = (new[] { (1, 0), (1, 1), (1, 2), (1, 3) });
            return MakePieceFromBrickCoordinates(4, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece2() {
            // ***
            // *
            (int, int)[] brickCoords = (new[] { (1, 0), (1, 1), (1, 2), (2, 0) });
            return MakePieceFromBrickCoordinates(3, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece3() {
            // ***
            //   *
            (int, int)[] brickCoords = (new[] { (1, 0), (1, 1), (1, 2), (2, 2) });
            return MakePieceFromBrickCoordinates(3, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece4() {
            // ***
            //  *
            (int, int)[] brickCoords = (new[] { (1, 0), (1, 1), (1, 2), (2, 1) });
            return MakePieceFromBrickCoordinates(3, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece5() {
            // **
            //  **
            (int, int)[] brickCoords = (new[] { (1, 0), (1, 1), (2, 1), (2, 2) });
            return MakePieceFromBrickCoordinates(3, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece6() {
            //  **
            // **
            (int, int)[] brickCoords = (new[] { (1, 1), (1, 2), (2, 0), (2, 1) });
            return MakePieceFromBrickCoordinates(3, brickCoords);
        }

        private static TetrisPiece MakePrototypePiece7() {
            // **
            // **
            (int, int)[] brickCoords = (new[] { (0, 0), (0, 1), (1, 0), (1, 1) });
            return MakePieceFromBrickCoordinates(2, brickCoords);
        }
    }
}
