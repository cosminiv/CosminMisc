using Games.Tetris;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_Core
{
    class Program
    {
        async static Task Main(string[] args) {
            Console.WriteLine("Press the Enter key to exit the program.");

            new TetrisEngine();
            
            //TetrisPieceFactory factory = new TetrisPieceFactory();
            //for (int i = 0; i < 20; i++) {
            //    Console.WriteLine(factory.MakePiece().ToString());
            //    Console.WriteLine();
            //}
            //Console.ReadLine();

            Console.ReadLine();
        }
    }
}
