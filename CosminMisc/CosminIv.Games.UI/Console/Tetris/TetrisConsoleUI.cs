using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.UI.Console.Tetris
{
    public class TetrisConsoleUI
    {
        readonly TetrisEngine Engine;
        readonly Coordinates BoardWindowOrigin = new Coordinates(2, 20);
        readonly TetrisBoardDisplayer BoardDisplayer;
        readonly TetrisPieceDisplayer PieceDisplayer;

        readonly int WindowHeight = 28;
        readonly int WindowWidth = 60;
        readonly int BorderWidth = 1;

        public TetrisConsoleUI(TetrisEngine engine) {
            SetWindowSize();
            Engine = engine;
            BoardDisplayer = new TetrisBoardDisplayer(engine, BoardWindowOrigin, BorderWidth);
            PieceDisplayer = new TetrisPieceDisplayer(BoardWindowOrigin, BorderWidth);
            WireEventHandlers();
        }

        public void Start() {
            BoardDisplayer.Display();
            Engine.Start();
            System.Console.CursorVisible = false;
            MonitorKeyboard();
        }


        private void SetWindowSize() {
            System.Console.SetBufferSize(WindowWidth, WindowHeight);
            System.Console.SetWindowSize(WindowWidth, WindowHeight);
        }

        private void WireEventHandlers() {
            Engine.PieceAppeared += Engine_PieceAppeared;
            Engine.PieceMoved += Engine_PieceMoved;
            Engine.RowsDeleted += Engine_RowsDeleted;
        }

        private void Engine_PieceAppeared(TetrisPieceWithPosition arg) {
            PieceDisplayer.Display(arg.Piece, arg.Position);
        }

        private void Engine_PieceMoved(PieceMovedArgs args) {
            PieceDisplayer.Delete(args.Piece, args.OldCoordinates);
            PieceDisplayer.Display(args.Piece, args.NewCoordinates);
        }

        private void Engine_RowsDeleted(TetrisFullRowsDeletedResult rowsDeleted) {
            PieceDisplayer.DeleteRows(rowsDeleted);
        }

        private void MonitorKeyboard() {
            while (true) {
                ConsoleKeyInfo keyInfo = System.Console.ReadKey(true);

                switch (keyInfo.Key) {
                    case ConsoleKey.LeftArrow:
                        Engine.MovePieceLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        Engine.MovePieceRight();
                        break;
                    //case ConsoleKey.UpArrow:
                    //    Engine.RotatePieceClockwise();
                    //    break;
                    case ConsoleKey.DownArrow:
                        Engine.MovePieceDown();
                        break;

                    case ConsoleKey.P:
                        Engine.TogglePause();
                        break;
                }
            }
        }
    }
}
