using CosminIv.Games.Common.Logging;
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

        TetrisBoard Board;
        Timer Timer;
        ILogger Logger;
        bool IsGameStarted = false;

        readonly int MaxSpeed = 10;

        #endregion


        #region Constructors

        public TetrisEngine(ILogger logger) {
            Logger = logger;
            Rows = 10;
            Columns = 10;
            Board = new TetrisBoard(Rows, Columns, logger);
            Speed = 1;
            Score = 0;
            Timer = MakeTimer();
        }

        #endregion


        #region Public Methods

        public void Start() {
            if (!IsGameStarted) {
                MakeNewPiece();
                Unpause();
                IsGameStarted = true;
            }
        }

        public void Pause() {
            Timer.Enabled = false;
        }

        public void Unpause() {
            Timer.Enabled = true;
        }

        #endregion


        #region Public events

        public delegate void PieceAppearedHandler(TetrisPieceWithPosition arg);
        public event PieceAppearedHandler PieceAppeared;

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
            //Debug.WriteLine($"Timer elapsed; time = {DateTime.Now.Second}.{DateTime.Now.Millisecond}");
            if (Board.CanMovePieceDown()) {
                Board.MovePieceDown();
            }
            else {
                Board.StickPiece();
                UpdateScoreAfterStickingPiece();
                MakeNewPiece();
            }
        }

        private void MakeNewPiece() {
            bool couldPlacePiece = Board.MakeNewPiece(out var piece);
            if (couldPlacePiece)
                PieceAppeared?.Invoke(piece);
            else
                End();
        }

        private void UpdateScoreAfterStickingPiece() {
            int increment = (int)(10 * (1 + (Speed - 1) * 0.1));
            Score += increment;
        }

        private void End() {
            Timer.Stop();
            Logger.WriteLine($"Game ended; score = {Score}");
            GameEnded?.Invoke();
        }

        double ComputeTimerInterval(int speed) {
            return 1000 * (1 - (Speed - 1) * 0.1);
        }

        #endregion

        //Methods
        //    MoveLeft
        //    MoveRight
        //    MoveDown
        //    Rotate


        //Events
        //    PieceChange
        //    PieceAppear
        //    ScoreChange
        //    LinesCollapse
        //    GameEnd
    }
}
