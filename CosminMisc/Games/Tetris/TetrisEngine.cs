using CosminIv.Games.Common;
using CosminIv.Games.Common.Logging;
using CosminIv.Games.Tetris.EventArguments;
using System;
using System.Diagnostics;
using System.Timers;

namespace CosminIv.Games.Tetris
{
    public class TetrisEngine
    {
        #region Data members and properties

        int _speed;

        public int Rows { get; }
        public int Columns { get; }
        public int Speed {
            get { return _speed; }
            private set { _speed = Math.Min(value, MaxSpeed); }
        }
        public int Score { get; private set; }
        public int Lines { get; private set; }

        TetrisBoard Board;
        Timer Timer;
        ILogger Logger;
        bool IsGameStarted = false;

        readonly int MaxSpeed = 10;
        static object ObjectMoveLock = new object();

        #endregion


        #region Constructors

        public TetrisEngine(TetrisEngineSettings settings) {
            Logger = settings.Logger;
            Rows = settings.Rows;
            Columns = settings.Columns;
            Board = new TetrisBoard(Rows, Columns, settings.Logger);
            Board.RowsDeleted += Board_RowsDeleted;
            Speed = settings.Speed;
            Score = 0;
            Timer = MakeTimer();
        }

        #endregion


        #region Public Methods

        public void Start() {
            if (!IsGameStarted) {
                MakeNewPiece();
                Timer.Enabled = true;
                IsGameStarted = true;
            }
        }

        public void MovePieceDown() {
            lock (ObjectMoveLock) {
                if (Board.CanMovePiece(1, 0)) {
                    PieceMovedArgs pieceMovedArgs = Board.MovePiece(1, 0);
                    PieceMoved?.Invoke(pieceMovedArgs);
                }
                else {
                    int fullRowCount = Board.StickPiece();
                    UpdateScoreAfterFullRows(fullRowCount);
                    MakeNewPiece();
                }
            }
        }

        public void MovePieceLeft() {
            lock (ObjectMoveLock) {
                if (Board.CanMovePiece(0, -1)) {
                    PieceMovedArgs pieceMovedArgs = Board.MovePiece(0, -1);
                    PieceMoved?.Invoke(pieceMovedArgs);
                }
            }
        }

        public void MovePieceRight() {
            lock (ObjectMoveLock) {
                if (Board.CanMovePiece(0, 1)) {
                    PieceMovedArgs pieceMovedArgs = Board.MovePiece(0, 1);
                    PieceMoved?.Invoke(pieceMovedArgs);
                }
            }
        }

        public void RotatePiece() {
            lock (ObjectMoveLock) {
                if (Board.CanRotatePiece()) {
                    PieceRotatedArgs pieceRotatedArgs = Board.RotatePiece();
                    PieceRotated?.Invoke(pieceRotatedArgs);
                }
            }
        }

        public void TogglePause() {
            Timer.Enabled = !Timer.Enabled;
        }

        public void Restart() {
            // todo
        }

        #endregion


        #region Public events

        public delegate void PieceAppearedHandler(TetrisPieceWithPosition pieceWithPos);
        public event PieceAppearedHandler PieceAppeared;

        public delegate void PieceMovedHandler(PieceMovedArgs args);
        public event PieceMovedHandler PieceMoved;

        public delegate void PieceRotatedHandler(PieceRotatedArgs args);
        public event PieceRotatedHandler PieceRotated;

        public delegate void RowsDeletedHandler(TetrisFullRowsDeletedResult rowsDeletedResult);
        public event RowsDeletedHandler RowsDeleted;

        public delegate void ScoreChangedHandler(ScoreChangedArgs args);
        public event ScoreChangedHandler ScoreChanged;

        public delegate void GameEndedHandler();
        public event GameEndedHandler GameEnded;

        #endregion


        #region Private methods

        private Timer MakeTimer() {
            Timer timer = new Timer(ComputeTimerInterval(Speed));
            timer.Enabled = false;
            timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            return timer;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            MovePieceDown();
        }

        private void MakeNewPiece() {
            bool couldPlacePiece = Board.MakeNewPiece(out TetrisPieceWithPosition piece);
            if (couldPlacePiece)
                PieceAppeared?.Invoke(piece);
            else
                End();
        }

        private void UpdateScoreAfterFullRows(int fullRowCount) {
            double speedMultiplier = 1 + (Speed - 1) * 0.1;
            int increment = (int)(10 * fullRowCount * speedMultiplier);
            Score += increment;
            Lines += fullRowCount;

            if (increment != 0)
                ScoreChanged?.Invoke(new ScoreChangedArgs { NewScore = Score, NewLines = Lines });
        }

        private void End() {
            Timer.Stop();
            Logger.WriteLine($"Game ended; score = {Score}");
            GameEnded?.Invoke();
        }

        double ComputeTimerInterval(int speed) {
            return 1000 * (1 - (Speed - 1) * 0.1);
        }

        private void Board_RowsDeleted(TetrisFullRowsDeletedResult rowsDeletedResult) {
            //TogglePause();
            RowsDeleted?.Invoke(rowsDeletedResult);
        }

        #endregion
    }
}
