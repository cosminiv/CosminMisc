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

        TetrisEngineSettings Settings;
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        int _speed;
        public int Speed {
            get { return _speed; }
            private set { _speed = Math.Min(value, MaxSpeed); }
        }

        int Score { get; set; }
        int FullLineCount { get; set; }

        TetrisBoard Board;
        Timer Timer;
        ILogger Logger;
        GameState State;

        readonly int FullLinesForLevelUp = 5;
        readonly int MaxSpeed = 10;
        readonly object PieceMoveLock = new object();

        #endregion


        #region Constructors

        public TetrisEngine(TetrisEngineSettings settings) {
            Settings = settings;
            Initialize();
        }

        #endregion


        #region Public Methods

        public void Start() {
            if (State == GameState.Paused) {
                MakeNewPiece();
                Timer.Enabled = true;
                State = GameState.Running;
                GameInitialized?.Invoke(new GameInitializedArgs { FixedBricks = Board.GetFixedBricks() });
            }
        }

        public void MovePieceDown() {
            TryMovePieceResult result = TryMovePiece(1, 0);

            if (result == TryMovePieceResult.CollisionDetected) {
                int fullRowCount = Board.StickPiece();
                if (fullRowCount > 0) {
                    UpdateScoreAfterFullRows(fullRowCount);
                    UpdateSpeedAfterFullRows(fullRowCount);
                }
                MakeNewPiece();
            }
        }

        public void MovePieceLeft() {
            TryMovePiece(0, -1);
        }

        public void MovePieceRight() {
            TryMovePiece(0, 1);
        }

        public void RotatePiece() {
            TryRotatePiece();
        }

        public void TogglePause() {
            Timer.Enabled = !Timer.Enabled;

            if (State == GameState.Running)
                State = GameState.Paused;
            else if (State == GameState.Paused)
                State = GameState.Running;
        }

        public void Restart() {
            Initialize();
            Start();
        }

        #endregion


        #region Public events

        public delegate void PieceAppearedHandler(TetrisPieceWithPosition pieceWithPos);
        public event PieceAppearedHandler PieceAppeared;

        public delegate void PieceMovedHandler(PieceMovedArgs args);
        public event PieceMovedHandler PieceMoved;

        public delegate void PieceRotatedHandler(PieceRotatedArgs args);
        public event PieceRotatedHandler PieceRotated;

        public delegate void RowsDeletedHandler(TetrisFixedBricksState modifiedRows);
        public event RowsDeletedHandler RowsDeleted;

        public delegate void ScoreChangedHandler(ScoreChangedArgs args);
        public event ScoreChangedHandler ScoreChanged;

        public delegate void SpeedChangedHandler(SpeedChangedArgs args);
        public event SpeedChangedHandler SpeedChanged;

        public delegate void GameEndedHandler();
        public event GameEndedHandler GameEnded;

        public delegate void GameInitializedHandler(GameInitializedArgs args);
        public event GameInitializedHandler GameInitialized;

        #endregion


        #region Private methods

        private void Initialize() {
            Logger = Settings.Logger;
            Rows = Settings.Rows;
            Columns = Settings.Columns;
            Board = new TetrisBoard(Settings);
            Board.RowsDeleted += Board_RowsDeleted;
            Speed = Settings.Speed;
            Score = 0;
            State = GameState.Paused;
            MakeTimer();
        }

        private TryMovePieceResult TryMovePiece(int rowDelta, int columnDelta) {
            if (State != GameState.Running)
                return TryMovePieceResult.GamePaused;

            bool canMove = false;

            lock (PieceMoveLock) {
                canMove = Board.CanMovePiece(rowDelta, columnDelta);
                if (canMove) {
                    PieceMovedArgs pieceMovedArgs = Board.MovePiece(rowDelta, columnDelta);
                    PieceMoved?.Invoke(pieceMovedArgs);
                }
            }

            TryMovePieceResult result = canMove ? TryMovePieceResult.Moved : TryMovePieceResult.CollisionDetected;

            return result;
        }

        private void TryRotatePiece() {
            if (State != GameState.Running)
                return;

            lock (PieceMoveLock) {
                if (Board.CanRotatePiece()) {
                    PieceRotatedArgs pieceRotatedArgs = Board.RotatePiece();
                    PieceRotated?.Invoke(pieceRotatedArgs);
                }
            }
        }

        private void MakeTimer() {
            if (Timer == null) {
                Timer = new Timer();
                Timer.Elapsed += Timer_Elapsed;
            }

            Timer.Interval = ComputeTimerInterval();
            Timer.Enabled = false;
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
            int diff = FullLineCount / FullLinesForLevelUp - fullLinesBefore / FullLinesForLevelUp;

            if (diff > 0) {
                Speed += diff;
                Timer.Interval = ComputeTimerInterval();
                SpeedChanged?.Invoke(new SpeedChangedArgs { Speed = this.Speed });
            }
        }

        private void End() {
            Timer.Stop();
            State = GameState.Ended;
            GameEnded?.Invoke();
        }

        int ComputeTimerInterval() {
            double interval = 1000 * (1 - (Speed - 1) * 0.1);
            return (int)interval;
        }

        private void Board_RowsDeleted(TetrisFixedBricksState rowsDeletedResult) {
            RowsDeleted?.Invoke(rowsDeletedResult);
        }

        #endregion

        enum TryMovePieceResult
        {
            Moved,
            CollisionDetected,
            GamePaused
        }
    }
}
