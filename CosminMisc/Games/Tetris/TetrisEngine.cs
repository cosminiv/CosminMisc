using CosminIv.Games.Common;
using CosminIv.Games.Common.Logging;
using CosminIv.Games.Tetris.DTO;
using CosminIv.Games.Tetris.DTO.EventArg;
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

        readonly int FullLinesForLevelUp = 7;
        readonly int MaxSpeed = 10;

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
                Timer.Enabled = true;
                State = GameState.Running;
                GameInitialized?.Invoke(new GameInitializedArgs { FixedBricks = Board.GetFixedBricks() });
                MakeNewPiece();
            }
        }

        public void MovePieceDown() {
            TryMovePieceResult result = TryMovePiece(1, 0);
            if (!result.Moved)
                AfterStickPiece(result.DeletedRows);
        }

        public void MovePieceAllTheWayDown() {
            if (State != GameState.Running)
                return;

            TryMovePieceResult result = Board.MovePieceAllTheWayDownAndStick();
            AfterStickPiece(result.DeletedRows);
            StateChanged?.Invoke(result.State);
        }

        private void AfterStickPiece(int deletedRows) {
            if (deletedRows > 0) {
                UpdateScoreAfterFullRows(deletedRows);
                UpdateSpeedAfterFullRows(deletedRows);
            }

            MakeNewPiece();
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

        public delegate void StateChangedHandler(TetrisState state);
        public event StateChangedHandler StateChanged;

        public delegate void PieceRotatedHandler(PieceRotatedArgs args);
        public event PieceRotatedHandler PieceRotated;

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
            Speed = Settings.Speed;
            Score = 0;
            State = GameState.Paused;
            MakeTimer();
        }

        private TryMovePieceResult TryMovePiece(int rowDelta, int columnDelta) {
            if (State != GameState.Running)
                return new TryMovePieceResult { Moved = false };

            TryMovePieceResult result = Board.TryMovePiece(rowDelta, columnDelta);
            if (result.Moved)
                StateChanged?.Invoke(result.State);

            return result;
        }

        private void TryRotatePiece() {
            if (State != GameState.Running)
                return;

            TryRotatePieceResult result = Board.TryRotatePiece();
            if (result.Rotated) {
                Debug.Assert(result.PieceRotatedArgs != null);
                PieceRotated?.Invoke(result.PieceRotatedArgs);
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
            TetrisPieceWithPosition piece = Board.MakeNewPiece();

            if (piece != null)
                StateChanged?.Invoke(Board.GetState());
            else
                End();
        }

        private void UpdateScoreAfterFullRows(int newFullRowCount) {
            Debug.Assert(newFullRowCount >= 0 && newFullRowCount <= 4);
            FullLineCount += newFullRowCount;
            double speedMultiplier = 1 + (Speed - 1) * 0.1;
            int increment = (int)(10 * newFullRowCount * speedMultiplier);
            Score += increment;

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

        #endregion
    }
}
