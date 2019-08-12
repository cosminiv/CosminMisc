using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using CosminIv.Games.Tetris.DTO;
using System;
using System.Diagnostics;

namespace CosminIv.Games.UI.Console.Tetris
{
    public class TetrisConsoleUI
    {
        readonly TetrisEngine Engine;
        readonly Coordinates BoardWindowOrigin = new Coordinates(2, 20);
        readonly TetrisBoardRenderer BoardRenderer;
        readonly TetrisTextRenderer TextRenderer;
        readonly object DrawLock = new object();
        TetrisState GameState;

        public TetrisConsoleUI(TetrisEngine engine) {
            Engine = engine;
            BoardRenderer = new TetrisBoardRenderer(engine, BoardWindowOrigin);
            TextRenderer = new TetrisTextRenderer();
            WireEventHandlers();
        }

        public void Start() {
            GameState = Engine.Start();

            SafeDraw(() => {
                System.Console.Clear();
                System.Console.CursorVisible = false;
                BoardRenderer.DisplayBoardBorder();
            });

            MonitorKeyboard();
        }

        private void WireEventHandlers() {
            Engine.StateChanged += Engine_StateChanged;
            Engine.GameEnded += Engine_GameEnded;
        }

        private void Engine_StateChanged(TetrisState state) {
            Redraw(state);
        }

        private void Redraw(TetrisState state) {
            SafeDraw(() => {
                BoardRenderer.DisplayBoard(state);
                TextRenderer.Update(state);
            });
        }

        private void SafeDraw(Action action) {
            lock (DrawLock) {
                action();
            }
        }

        private void Engine_GameEnded() {
            SafeDraw(() => {
                TextRenderer.DisplayMessage(TetrisMessage.GameEnded);
            });
        }

        private void MonitorKeyboard() {
            while (true) {
                ConsoleKeyInfo keyInfo = System.Console.ReadKey(true);

                switch (keyInfo.Key) {
                    case ConsoleKey.LeftArrow:
                        GameState = Engine.MovePieceLeft(GameState);
                        Redraw(GameState);
                        break;
                    case ConsoleKey.RightArrow:
                        GameState = Engine.MovePieceRight(GameState);
                        Redraw(GameState);
                        break;
                    case ConsoleKey.UpArrow:
                        GameState = Engine.RotatePiece(GameState);
                        Redraw(GameState);
                        break;
                    case ConsoleKey.DownArrow:
                        GameState = Engine.MovePieceDown(GameState);
                        Redraw(GameState);
                        break;
                    case ConsoleKey.P:
                        Engine.TogglePause();
                        break;
                    case ConsoleKey.R:
                        Engine.Restart();
                        break;
                    case ConsoleKey.Spacebar:
                        GameState = Engine.MovePieceAllTheWayDown(GameState);
                        Engine_StateChanged(GameState);
                        break;
                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
