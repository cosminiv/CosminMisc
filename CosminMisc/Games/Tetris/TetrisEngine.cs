using CosminIv.Games.Common;
using CosminIv.Games.Common.Logging;
using CosminIv.Games.Tetris.DTO;
using System;
using System.Diagnostics;
using System.Timers;

namespace CosminIv.Games.Tetris
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

        public void Start() {
            if (State == GameState.Paused) {
                Timer.Enabled = Settings.EnableTimer;
                State = GameState.Running;
            }
        }

        public TetrisState MovePieceDown() {
            TetrisState state = TryMovePiece(1, 0);
            return state;
        }

        public TetrisState MovePieceAllTheWayDown() {
            if (State != GameState.Running)
                return GetState();

            TryMovePieceResult result = BoardLogic.MovePieceAllTheWayDownAndStick();
            AfterStickPiece(result.DeletedRows, result.IsGameEnd);
            TetrisState state = GetState();

            StateChanged?.Invoke(state);

            return state;
        }

        public TetrisState MovePieceLeft() {
            return TryMovePiece(0, -1);
        }

        public TetrisState MovePieceRight() {
            return TryMovePiece(0, 1);
        }

        public TetrisState RotatePiece() {
            return TryRotatePiece();
        }

        public void TogglePause() {
            ToggleTimerState();
            ToggleGameState();
        }

        public void Restart() {
            Initialize();
            Start();
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
            State = GameState.Paused;
            MakeTimer();
        }

        private TetrisState TryMovePiece(int rowDelta, int columnDelta) {
            if (State != GameState.Running)
                return GetState();

            TryMovePieceResult result = BoardLogic.TryMovePiece(rowDelta, columnDelta);
            AfterStickPiece(result.DeletedRows, result.IsGameEnd);
            TetrisState state = GetState();

            if (result.Moved || result.DeletedRows > 0)
                StateChanged?.Invoke(state);

            return state;
        }

        TetrisState GetState() {
            TetrisState state = BoardLogic.GetState();
            AddEngineSpecificDataToState(state);
            return state;
        }

        private TetrisState TryRotatePiece() {
            if (State != GameState.Running)
                return GetState();

            TryRotatePieceResult result = BoardLogic.TryRotatePiece();
            TetrisState state = GetState();

            if (result.Rotated)
                StateChanged?.Invoke(state);

            return state;
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
