using System;
using System.Diagnostics;
using System.Timers;
using CosminIv.Games.Engine.Common;
using CosminIv.Games.Engine.Common.Logging;
using CosminIv.Games.Engine.Tetris.DTO;

namespace CosminIv.Games.Engine.Tetris
{
    public class TetrisEngine : IDisposable
    {
        #region Data members and properties

        public TetrisEngineSettings Settings;

        int _speed;
        public int Speed {
            get { return _speed; }
            private set { _speed = Math.Min(value, MaxSpeed); }
        }

        int Score { get; set; }
        int Lines { get; set; }

        TetrisBoardLogic BoardLogic;
        Timer Timer;
        ILogger Logger;
        GameState State;

        readonly int MaxSpeed = 10;

        #endregion


        #region Constructors

        public TetrisEngine(TetrisEngineSettings settings) {
            Settings = settings;
            Initialize();
        }

        #endregion


        #region Public Methods

        public TetrisState Start() {
            if (State == GameState.Paused) {
                Timer.Enabled = Settings.EnableTimer;
                State = GameState.Running;
            }

            return GetState();
        }

        public TetrisState MovePieceDown(TetrisState state) {
            return TryMovePiece(state, 1, 0, false);
        }

        public TetrisState MovePieceAllTheWayDown(TetrisState state) {
            if (State != GameState.Running)
                return state;

            TryMovePieceResult result = BoardLogic.MovePieceAllTheWayDownAndStick(state);
            AfterStickPiece(result.DeletedRows, result.IsGameEnd);
            TetrisState newState = GetState();

            return newState;
        }

        public TetrisState MovePieceLeft(TetrisState state) {
            return TryMovePiece(state, 0, -1, false);
        }

        public TetrisState MovePieceRight(TetrisState state) {
            return TryMovePiece(state, 0, 1, false);
        }

        public TetrisState RotatePiece(TetrisState state) {
            return TryRotatePiece(state);
        }

        public void TogglePause() {
            ToggleTimerState();
            ToggleGameState();
        }

        public TetrisState Restart() {
            Initialize();
            TetrisState state = Start();
            return state;
        }

        public void Dispose() {
            if (Timer != null)
                Timer.Dispose();
        }

        #endregion


        #region Public events

        public delegate void StateChangedHandler(TetrisState state);
        public event StateChangedHandler StateChanged;

        public delegate void GameEndedHandler();
        public event GameEndedHandler GameEnded;

        #endregion


        #region Private methods

        private void Initialize() {
            Logger = Settings.Logger;
            BoardLogic = new TetrisBoardLogic(Settings);
            Speed = Settings.Speed;
            Score = 0;
            Lines = 0;
            State = GameState.Paused;
            MakeTimer();
        }

        private TetrisState TryMovePiece(TetrisState state, int rowDelta, int columnDelta, bool fireEvent) {
            if (State != GameState.Running)
                return state;

            TryMovePieceResult result = BoardLogic.TryMovePiece(state, rowDelta, columnDelta);
            AfterStickPiece(result.DeletedRows, result.IsGameEnd);
            TetrisState newState = GetState();

            if (fireEvent && (result.Moved || result.DeletedRows > 0))
                StateChanged?.Invoke(newState);

            return newState;
        }

        TetrisState GetState() {
            TetrisState state = BoardLogic.GetState();
            AddEngineSpecificDataToState(state);
            return state;
        }

        private TetrisState TryRotatePiece(TetrisState state) {
            if (State != GameState.Running)
                return state;

            TryRotatePieceResult result = BoardLogic.TryRotatePiece(state);
            TetrisState newState = GetState();

            return newState;
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
            TryMovePiece(GetState(), 1, 0, true);
        }

        private void UpdateScoreAfterFullRows(int newFullRowCount) {
            Debug.Assert(newFullRowCount >= 0 && newFullRowCount <= 4);
            Lines += newFullRowCount;
            double speedMultiplier = 1 + (Speed - 1) * 0.1;
            int increment = (int)(10 * newFullRowCount * speedMultiplier);
            Score += increment;
        }

        private void UpdateSpeedAfterFullRows(int newFullRowCount) {
            int fullLinesBefore = Lines - newFullRowCount;
            int diff = Lines / Settings.FullLinesForLevelUp - fullLinesBefore / Settings.FullLinesForLevelUp;

            if (diff > 0) {
                Speed += diff;
                Timer.Interval = ComputeTimerInterval();
            }
        }

        private void EndGame() {
            Timer.Stop();
            State = GameState.Ended;
            GameEnded?.Invoke();
        }

        int ComputeTimerInterval() {
            double interval = 1000 * (1 - (Speed - 1) * 0.1);
            return (int)interval;
        }

        private void ToggleGameState() {
            if (State == GameState.Running)
                State = GameState.Paused;
            else if (State == GameState.Paused)
                State = GameState.Running;
        }

        private void ToggleTimerState() {
            if (Settings.EnableTimer)
                Timer.Enabled = !Timer.Enabled;
        }

        private void AddEngineSpecificDataToState(TetrisState state) {
            state.Score = Score;
            state.Lines = Lines;
            state.Speed = Speed;
        }

        private void AfterStickPiece(int deletedRows, bool isGameEnd) {
            if (deletedRows > 0) {
                UpdateScoreAfterFullRows(deletedRows);
                UpdateSpeedAfterFullRows(deletedRows);
            }

            if (isGameEnd)
                EndGame();
        }

        #endregion
    }
}
