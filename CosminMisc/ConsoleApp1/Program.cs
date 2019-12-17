using ConsoleApp1.Algorithms;
using ConsoleApp1.Arrays;
using ConsoleApp1.HackerRank;
using ConsoleApp1.Leet;
using ConsoleApp1.Misc;
using ConsoleApp1.Trees;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosminIv.Games.Common;
using CosminIv.Games.Common.Logging;
using CosminIv.Games.Tetris;
using CosminIv.Games.Tetris.DTO;
using CosminIv.Games.UI.Console.Tetris;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTetris();

            Stopwatch sw = Stopwatch.StartNew();

            new Leet_060().Solve();

            long duration = sw.ElapsedMilliseconds;
            Console.WriteLine();
            //Console.WriteLine($"Result: {a}");
            Console.WriteLine($"Took {duration}ms");
            Console.ReadLine();
            Debug.Print($"Took {duration}ms");
        }

        private static void RunTetris()
        {
            TetrisEngineSettings settings = new TetrisEngineSettings
            {
                Columns = 16,
                Logger = new DebugLogger(),
                Rows = 15,
                Speed = 4,
                RowsWithFixedBricks = 0,
                EnableTimer = true
            };

            TetrisEngine tetrisEngine = new TetrisEngine(settings);
            TetrisConsoleUI tetrisUI = new TetrisConsoleUI(tetrisEngine, GameMode.Interactive);

            tetrisUI.Start();
        }
    }
}
