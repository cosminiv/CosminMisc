using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using CosminIv.Games.Tetris.EventArguments;
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
        readonly TetrisTextRenderer TextRenderer;

        readonly int WindowHeight = 28;
        readonly int WindowWidth = 60;
        readonly int BorderWidth = 1;

        public TetrisConsoleUI(TetrisEngine engine) {
            SetWindowSize();
            Engine = engine;
            BoardDisplayer = new TetrisBoardDisplayer(engine, BoardWindowOrigin, BorderWidth);
            PieceDisplayer = new TetrisPieceDisplayer(BoardWindowOrigin, BorderWidth);
            TextRenderer = new TetrisTextRenderer();
            WireEventHandlers();
        }

        public void Start() {
            BoardDisplayer.Display();
            TextRenderer.DisplayInitial();
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
            Engine.PieceRotated += Engine_PieceRotated;
            Engine.GameEnded += Engine_GameEnded;
            Engine.ScoreChanged += Engine_ScoreChanged;
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

        private void Engine_PieceRotated(PieceRotatedArgs args) {
            PieceDisplayer.Delete(args.PieceBeforeRotation, args.Coordinates);
            PieceDisplayer.Display(args.PieceAfterRotation, args.Coordinates);
        }

        private void Engine_ScoreChanged(ScoreChangedArgs args) {
            TextRenderer.DisplayScore(args.NewScore);
        }

        private void Engine_GameEnded() {
            TextRenderer.DisplayMessage(TetrisMessage.GameEnded);
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
                    case ConsoleKey.UpArrow:
                        Engine.RotatePiece();
                        break;
                    case ConsoleKey.DownArrow:
                        Engine.MovePieceDown();
                        break;
                    case ConsoleKey.P:
                        Engine.TogglePause();
                        break;
                    case ConsoleKey.R:
                        Engine.Restart();
                        break;
                }
            }
        }
    }
}
