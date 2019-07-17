using System;
using System.Timers;

namespace Games.Tetris
{
    public class TetrisEngine
    {
        public TetrisEngine() {
            Rows = 20;
            Cols = 10;
            Speed = 3;
            Score = 0;
            Timer = MakeTimer();
        }

        private Timer MakeTimer() {
            Timer timer = new Timer(ComputeTimerInterval(Speed));
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            return timer;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) {
            Console.WriteLine($"Timer elapsed; time = {DateTime.Now.Second}.{DateTime.Now.Millisecond}");
        }

        public int Rows { get; }
        public int Cols { get; }
        public int Speed { get; private set; }
        public int Score { get; private set; }

        TetrisPiece CurrentPiece = new TetrisPiece();
        Timer Timer;

        double ComputeTimerInterval(int speed) {
            return 1000 * (1 - (Speed - 1) * 0.1);
        }

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
