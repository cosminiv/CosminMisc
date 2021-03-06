﻿using System;
using System.Threading;
using CosminIv.Games.Engine.Common;
using CosminIv.Games.Engine.Tetris;
using CosminIv.Games.Engine.Tetris.DTO;

namespace CosminIv.Games.UI.Console.Tetris
{
    public class TetrisConsoleUI
    {
        readonly TetrisEngine Engine;
        private readonly GameMode Mode;
        readonly Coordinates BoardWindowOrigin = new Coordinates(2, 20);
        readonly TetrisBoardRenderer BoardRenderer;
        readonly TetrisTextRenderer TextRenderer;
        readonly TetrisSolver Solver;
        readonly object DrawLock = new object();
        TetrisState GameState;

        public TetrisConsoleUI(TetrisEngine engine, GameMode mode) {
            Engine = engine;
            Mode = mode;
            BoardRenderer = new TetrisBoardRenderer(engine, BoardWindowOrigin);
            TextRenderer = new TetrisTextRenderer();
            Solver = new TetrisSolver(Engine.Settings.Rows, Engine.Settings.Columns);
            WireEventHandlers();
        }

        public void Start() {
            GameState = Engine.Start();
            SafeDraw(() => DrawInitial());

            if (Mode == GameMode.Interactive)
                MonitorKeyboard();
            else
                ExecuteSolverCommands();
        }

        private void DrawInitial() {
            System.Console.Clear();
            System.Console.CursorVisible = false;
            BoardRenderer.DisplayBoardBorder();
            Redraw(null, GameState);
        }

        private void WireEventHandlers() {
            Engine.StateChanged += Engine_StateChanged;
            Engine.GameEnded += Engine_GameEnded;
        }

        private void Engine_StateChanged(TetrisState newState) {
            TetrisState oldState = GameState;
            GameState = newState;
            Redraw(oldState, newState);
        }

        private void Redraw(TetrisState oldState, TetrisState newState) {
            SafeDraw(() => {
                BoardRenderer.DisplayBricks(oldState, newState);
                TextRenderer.Update(oldState, newState);
            });
        }

        private void SafeDraw(Action action) {
            lock (DrawLock) {
                action();
            }
        }

        private void Engine_GameEnded() {
            SafeDraw(() => {
                TextRenderer.DisplayText(TetrisMessage.GameEnded);
            });
        }

        private void MonitorKeyboard() {
            while (true) {
                ConsoleKeyInfo keyInfo = System.Console.ReadKey(true);

                switch (keyInfo.Key) {
                    case ConsoleKey.LeftArrow:
                        ExecuteCommand((state) => Engine.MovePieceLeft(GameState));
                        break;
                    case ConsoleKey.RightArrow:
                        ExecuteCommand((state) => Engine.MovePieceRight(GameState));
                        break;
                    case ConsoleKey.UpArrow:
                        ExecuteCommand((state) => Engine.RotatePiece(GameState));
                        break;
                    case ConsoleKey.DownArrow:
                        ExecuteCommand((state) => Engine.MovePieceDown(GameState));
                        break;
                    case ConsoleKey.Spacebar:
                        ExecuteCommand((state) => Engine.MovePieceAllTheWayDown(GameState));
                        break;
                    case ConsoleKey.P:
                        Engine.TogglePause();
                        break;
                    case ConsoleKey.R:
                        GameState = Engine.Restart();
                        SafeDraw(() => DrawInitial());
                        break;
                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void ExecuteCommand(Func<TetrisState, TetrisState> command) {
            TetrisState oldState = GameState;
            GameState = command(GameState);
            Redraw(oldState, GameState);
        }

        private void ExecuteSolverCommands() {
            while (true) {
                Thread.Sleep(600 - 100 * (GameState.Speed - 4));
                TetrisMoves moves = Solver.Solve(GameState);

                foreach (TetrisMove move in moves.Moves) {
                    switch (move) {
                        case TetrisMove.MoveLeft:
                            ExecuteCommand((state) => Engine.MovePieceLeft(GameState));
                            break;
                        case TetrisMove.MoveRight:
                            ExecuteCommand((state) => Engine.MovePieceRight(GameState));
                            break;
                        case TetrisMove.MoveDown:
                            ExecuteCommand((state) => Engine.MovePieceDown(GameState));
                            break;
                        case TetrisMove.MoveAllTheWayDown:
                            ExecuteCommand((state) => Engine.MovePieceAllTheWayDown(GameState));
                            break;
                        case TetrisMove.Rotate:
                            ExecuteCommand((state) => Engine.RotatePiece(GameState));
                            break;
                        default:
                            throw new Exception($"Unknown move: {move}");
                    }

                    Thread.Sleep(Math.Max(0, 150 - 30 * (GameState.Speed - 4)));
                }
            }
        }
    }
}
