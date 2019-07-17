using Games.Tetris;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_Core
{
    class Program
    {
        async static Task Main(string[] args) {
            //new TetrisEngine();
            //await Task.Delay(10000);

            TetrisPieceFactory factory = new TetrisPieceFactory();
            for (int i = 0; i < 20; i++) {
                Console.WriteLine(factory.MakePiece().ToString());
            }
            Console.ReadLine();

            //Console.WriteLine("Press the Enter key to exit the program.");
            //Console.ReadLine();
        }
    }
}
