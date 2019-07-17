using System;
using System.Collections.Generic;
using System.Text;

namespace Games.Tetris
{
    public class TetrisTest
    {
        public void Test() {
            TetrisEngine tetrisEngine = new TetrisEngine();
            TetrisPieceFactory factory = new TetrisPieceFactory();

            for (int i = 0; i < 20; i++) {
                TetrisPiece piece = factory.MakePiece();
                Console.WriteLine(piece);
                //Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
