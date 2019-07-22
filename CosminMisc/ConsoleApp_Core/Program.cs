using CosminIv.Games.Tetris;
using CosminIv.Games.UI.Console.Tetris;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_Core
{
    class Program
    {
        async static Task Main(string[] args) {
            RunTetris();

            //Console.SetCursorPosition(20, 2);
            //Console.WriteLine("World");
            //Console.ReadLine();

            //Console.WriteLine("Press the Enter key to exit the program.");

            //TetrisEngine tetrisEngine = new TetrisEngine();

            //TetrisPieceFactory factory = new TetrisPieceFactory();
            //for (int i = 0; i < 20; i++) {
            //    Console.WriteLine(factory.MakePiece().ToString());
            //    Console.WriteLine();
            //}
            //Console.ReadLine();

            //Console.ReadLine();
            //tetrisEngine.Pause();

            Console.ReadLine();
        }

        private static void RunTetris() {
            TetrisEngine tetrisEngine = new TetrisEngine();
            TetrisConsoleUI tetrisUI = new TetrisConsoleUI(tetrisEngine);
            tetrisUI.Start();
        }

        private static void TestConsoleAnimation() {
            Console.WriteLine("Use arrows to move the text");

            int left = 20, top = 10;
            int oldLeft = 0, oldTop = 1;
            string text = "Hello";
            string blankText = "     ";

            while (left > 0 && top > 0) {
                Console.SetCursorPosition(oldLeft, oldTop);
                Console.Write(blankText);

                Console.SetCursorPosition(left, top);
                Console.Write(text);

                oldLeft = left;
                oldTop = top;

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.LeftArrow)
                    left--;
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                    left++;
                else if (keyInfo.Key == ConsoleKey.UpArrow)
                    top--;
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                    top++;
                else if (keyInfo.Key == ConsoleKey.Enter)
                    break;
            }
        }
    }
}
