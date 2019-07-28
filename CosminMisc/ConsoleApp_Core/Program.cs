using CosminIv.Games.Common.Logging;
using CosminIv.Games.Tetris;
using CosminIv.Games.UI.Console.Tetris;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_Core
{
    class Program
    {
        static void Main(string[] args) {
            RunTetris();

            //Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        private static void RunTetris() {
            TetrisEngineSettings settings = new TetrisEngineSettings {
                Columns = 16,
                Logger = new DebugLogger(),
                Rows = 15,
                Speed = 4
            };
            TetrisEngine tetrisEngine = new TetrisEngine(settings);
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
