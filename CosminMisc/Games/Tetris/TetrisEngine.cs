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
            private set {
                _speed = Math.Min(value, MaxSpeed);
            }
        }
        int Score { get; set; }
        int FullLineCount { get; set; }

        TetrisBoard Board;
        Timer Timer;
        ILogger Logger;
        bool IsGameStarted = false;

        readonly int FullLinesForLevelUp = 10;
        readonly int MaxSpeed = 10;
        static object PieceMoveLock = new object();

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
            lock (PieceMoveLock) {
                if (Board.CanMovePiece(1, 0)) {
                    PieceMovedArgs pieceMovedArgs = Board.MovePiece(1, 0);
                    PieceMoved?.Invoke(pieceMovedArgs);
                }
                else {
                    int fullRowCount = Board.StickPiece();
                    if (fullRowCount > 0) {
                        UpdateScoreAfterFullRows(fullRowCount);
                        UpdateSpeedAfterFullRows(fullRowCount);
                    }
                    MakeNewPiece();
                }
            }
        }

        public void MovePieceLeft() {
            lock (PieceMoveLock) {
                if (Board.CanMovePiece(0, -1)) {
                    PieceMovedArgs pieceMovedArgs = Board.MovePiece(0, -1);
                    PieceMoved?.Invoke(pieceMovedArgs);
                }
            }
        }

        public void MovePieceRight() {
            lock (PieceMoveLock) {
                if (Board.CanMovePiece(0, 1)) {
                    PieceMovedArgs pieceMovedArgs = Board.MovePiece(0, 1);
                    PieceMoved?.Invoke(pieceMovedArgs);
                }
            }
        }

        public void RotatePiece() {
            lock (PieceMoveLock) {
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

        public delegate void RowsDeletedHandler(TetrisModifiedRows rowsDeletedResult);
        public event RowsDeletedHandler RowsDeleted;

        public delegate void ScoreChangedHandler(ScoreChangedArgs args);
        public event ScoreChangedHandler ScoreChanged;

        public delegate void SpeedChangedHandler(SpeedChangedArgs args);
        public event SpeedChangedHandler SpeedChanged;

        public delegate void GameEndedHandler();
        public event GameEndedHandler GameEnded;

        #endregion


        #region Private methods

        private Timer MakeTimer() {
            Timer timer = new Timer();
            timer.Interval = ComputeTimerInterval();
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
            FullLineCount += fullRowCount;

            if (increment != 0)
                ScoreChanged?.Invoke(new ScoreChangedArgs { Score = Score, LineCount = FullLineCount });
        }

        private void UpdateSpeedAfterFullRows(int newFullRowCount) {
            int fullLinesBefore = FullLineCount - newFullRowCount;
            
            if (fullLinesBefore / FullLinesForLevelUp != FullLineCount / FullLinesForLevelUp) {
                Speed++;
                Timer.Interval = ComputeTimerInterval();
                SpeedChanged?.Invoke(new SpeedChangedArgs { Speed = this.Speed });
            }
        }

        private void End() {
            Timer.Stop();
            GameEnded?.Invoke();
        }

        int ComputeTimerInterval() {
            double interval = 1000 * (1 - (Speed - 1) * 0.1);
            return (int)interval;
        }

        private void Board_RowsDeleted(TetrisModifiedRows rowsDeletedResult) {
            RowsDeleted?.Invoke(rowsDeletedResult);
        }

        #endregion
    }
}
