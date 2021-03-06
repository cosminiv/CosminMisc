﻿using ConsoleApp1.Leet;
using System;
using System.Diagnostics;
using ConsoleApp1.Eu._051_100;
using ConsoleApp1.Leet._051_100;
using CosminIv.Games.Engine.Common;
using CosminIv.Games.Engine.Common.Logging;
using CosminIv.Games.Engine.Tetris;
using CosminIv.Games.Engine.Tetris.DTO;
using CosminIv.Games.UI.Console.Tetris;

namespace ConsoleApp1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
             RunPracticeSolver();

             // RunTetris();
        }

        private static void RunPracticeSolver()
        {
            Stopwatch sw = Stopwatch.StartNew();

            new Leet_080().Solve();

            long duration = sw.ElapsedMilliseconds;
            Console.WriteLine();
            //Console.WriteLine($"Result: {a}");
            Console.WriteLine($"Took {duration}ms");
            //Console.ReadLine();
            Debug.Print($"\nTook {duration}ms");
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
