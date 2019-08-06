using CosminIv.Games.Common;
using CosminIv.Games.Tetris;
using CosminIv.Games.Tetris.DTO;
using CosminIv.Games.Tetris.DTO.EventArg;
using System;

namespace CosminIv.Games.UI.Console.Tetris
{
    public class TetrisConsoleUI
    {
        readonly TetrisEngine Engine;
        readonly Coordinates BoardWindowOrigin = new Coordinates(2, 20);
        readonly TetrisBoardRenderer BoardRenderer;
        readonly TetrisTextRenderer TextRenderer;
        readonly object DrawLock = new object();

        public TetrisConsoleUI(TetrisEngine engine) {
            Engine = engine;
            BoardRenderer = new TetrisBoardRenderer(engine, BoardWindowOrigin);
            TextRenderer = new TetrisTextRenderer();
            WireEventHandlers();
        }

        public void Start() {
            Engine.Start();
            // Rendering is done as a response to Engine_GameInitialized event.
            System.Console.CursorVisible = false;
            MonitorKeyboard();
        }

        private void WireEventHandlers() {
            Engine.GameInitialized += Engine_GameInitialized;
            Engine.PieceAppeared += Engine_PieceAppeared;
            Engine.PieceMoved += Engine_PieceMoved;
            Engine.RowsDeleted += Engine_RowsDeleted;
            Engine.PieceRotated += Engine_PieceRotated;
            Engine.GameEnded += Engine_GameEnded;
            Engine.ScoreChanged += Engine_ScoreChanged;
            Engine.SpeedChanged += Engine_SpeedChanged;
        }

        private void Engine_GameInitialized(GameInitializedArgs args) {
            SafeDraw(() => {
                System.Console.Clear();
                BoardRenderer.DisplayBoard(args);
                TextRenderer.DisplayInitial(Engine.Speed);
            });
        }

        private void SafeDraw(Action action) {
            lock (DrawLock) {
                action();
            }
        }

        private void Engine_PieceAppeared(TetrisPieceWithPosition arg) {
            SafeDraw(() => {
                BoardRenderer.DisplayPiece(arg.Piece, arg.Position);
                TextRenderer.DeleteNextPiece(arg.Piece);
                TextRenderer.DisplayNextPiece(arg.NextPiece);
            });
        }

        private void Engine_PieceMoved(PieceMovedArgs args) {
            SafeDraw(() => {
                BoardRenderer.DeletePiece(args.Piece, args.OldCoordinates);
                BoardRenderer.DisplayPiece(args.Piece, args.NewCoordinates);
            });
        }

        private void Engine_RowsDeleted(TetrisFixedBricksState rowsDeleted) {
            SafeDraw(() => {
                BoardRenderer.UpdateRows(rowsDeleted);
            });
        }

        private void Engine_PieceRotated(PieceRotatedArgs args) {
            SafeDraw(() => {
                BoardRenderer.DeletePiece(args.PieceBeforeRotation, args.Coordinates);
                BoardRenderer.DisplayPiece(args.PieceAfterRotation, args.Coordinates);
            });
        }

        private void Engine_ScoreChanged(ScoreChangedArgs args) {
            SafeDraw(() => {
                TextRenderer.DisplayScore(args.Score);
                TextRenderer.DisplayLineCount(args.LineCount);
            });
        }

        private void Engine_GameEnded() {
            SafeDraw(() => {
                TextRenderer.DisplayMessage(TetrisMessage.GameEnded);
            });
        }

        private void Engine_SpeedChanged(SpeedChangedArgs args) {
            SafeDraw(() => {
                TextRenderer.DisplaySpeed(args.Speed);
            });
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
                    case ConsoleKey.Spacebar:
                        Engine.MovePieceAllTheWayDown();
                        break;
                }
            }
        }
    }
}
