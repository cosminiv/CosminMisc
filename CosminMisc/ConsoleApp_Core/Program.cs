using CosminIv.Games.Common.Logging;
using CosminIv.Games.Tetris;
using CosminIv.Games.Tetris.DTO;
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
                Speed = 4,
                RowsWithFixedBricks = 0,
                EnableTimer = true
            };

            TetrisEngine tetrisEngine = new TetrisEngine(settings);
            TetrisConsoleUI tetrisUI = new TetrisConsoleUI(tetrisEngine);

            tetrisUI.Start();
        }
    }
}
