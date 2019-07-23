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
        }


        private void SetWindowSize() {
            System.Console.SetBufferSize(WindowWidth, WindowHeight);
            System.Console.SetWindowSize(WindowWidth, WindowHeight);
        }

        private void WireEventHandlers() {
            Engine.PieceAppeared += Engine_PieceAppeared;
            Engine.PieceMoved += Engine_PieceMoved;
        }

        private void Engine_PieceAppeared(TetrisPieceWithPosition arg) {
            PieceDisplayer.Display(arg.Piece, arg.Position);
        }

        private void Engine_PieceMoved(PieceMovedArgs args) {
            PieceDisplayer.Delete(args.Piece, args.OldCoordinates);
            PieceDisplayer.Display(args.Piece, args.NewCoordinates);
        }
    }
}
