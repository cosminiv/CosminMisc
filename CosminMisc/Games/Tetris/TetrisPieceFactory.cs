using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Tetris
{
    internal class TetrisPieceFactory
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

        private static TetrisPiece MakePrototypePiece1() {
            TetrisPiece result = new TetrisPiece();
            // ****
            result.Bricks[0][0] = result.Bricks[0][1] = result.Bricks[0][2] = result.Bricks[0][3] = true;
            return result;
        }

        private static TetrisPiece MakePrototypePiece2() {
            TetrisPiece result = new TetrisPiece();
            // ***
            // *
            result.Bricks[0][0] = result.Bricks[0][1] = result.Bricks[0][2] = result.Bricks[1][0] = true;
            return result;
        }

        private static TetrisPiece MakePrototypePiece3() {
            TetrisPiece result = new TetrisPiece();
            // ***
            //   *
            result.Bricks[0][0] = result.Bricks[0][1] = result.Bricks[0][2] = result.Bricks[1][2] = true;
            return result;
        }

        private static TetrisPiece MakePrototypePiece4() {
            TetrisPiece result = new TetrisPiece();
            // ***
            //  *
            result.Bricks[0][0] = result.Bricks[0][1] = result.Bricks[0][2] = result.Bricks[1][1] = true;
            return result;
        }

        private static TetrisPiece MakePrototypePiece5() {
            TetrisPiece result = new TetrisPiece();
            // **
            //  **
            result.Bricks[0][0] = result.Bricks[0][1] = result.Bricks[1][1] = result.Bricks[1][2] = true;
            return result;
        }

        private static TetrisPiece MakePrototypePiece6() {
            TetrisPiece result = new TetrisPiece();
            //  **
            // **
            result.Bricks[0][1] = result.Bricks[0][2] = result.Bricks[1][0] = result.Bricks[1][1] = true;
            return result;
        }

        private static TetrisPiece MakePrototypePiece7() {
            TetrisPiece result = new TetrisPiece();
            // **
            // **
            result.Bricks[0][0] = result.Bricks[0][1] = result.Bricks[1][0] = result.Bricks[1][1] = true;
            return result;
        }
    }
}
