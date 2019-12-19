using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace CosminIv.Games.UI.WinForms.Pool
{
    public partial class PoolMainForm : Form
    {
        private double _ballX;
        private double _ballY = 100;
        private double _ballOldX;
        private double _ballOldY;

        private double _ballSpeed = 7;
        private double _ballAngle = 0.2;
        private readonly int _ballRadius = 12;

        private readonly double _frictionCoefficient = 0.005;

        private readonly SolidBrush _ballBrush = new SolidBrush(Color.DarkBlue);
        private readonly SolidBrush _ballDeleteBrush = new SolidBrush(DefaultBackColor);

        private readonly System.Timers.Timer _timer = new System.Timers.Timer(15);

        public PoolMainForm()
        {
            InitializeComponent();

            _timer.Elapsed += TimerOnElapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (UpdateBallPosition())
                panel1.Invalidate();
        }

        private bool UpdateBallPosition()
        {
            if (_ballSpeed < 0.06)
            {
                _ballSpeed = 0;
                return false;
            }

            _ballOldX = _ballX;
            _ballOldY = _ballY;

            _ballX += _ballSpeed * Math.Cos(_ballAngle);
            _ballY += _ballSpeed * Math.Sin(_ballAngle);

            _ballSpeed *= 1 - _frictionCoefficient;

            if (_ballX + _ballRadius >= panel1.Width)
            {
                _ballX = panel1.Width - _ballRadius;
                _ballAngle = Math.PI - _ballAngle;
            }

            return true;
        }

        private void DrawBall()
        {
            int ballDiameter = 2 * _ballRadius;
            double ballOldYWindow = ToWindowCoordinates(_ballOldY);
            double ballYWindow = ToWindowCoordinates(_ballY);

            using (Graphics graphics = panel1.CreateGraphics())
            {
                graphics.FillEllipse(_ballDeleteBrush, (int)_ballOldX - _ballRadius, (int)ballOldYWindow - _ballRadius, ballDiameter, ballDiameter);
                graphics.FillEllipse(_ballBrush, (int)_ballX - _ballRadius, (int)ballYWindow - _ballRadius, ballDiameter, ballDiameter);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawBall();
        }

        double ToWindowCoordinates(double y)
        {
            return panel1.Height - y;
        }

        // This stops flickering
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000; // WS_EX_COMPOSITED       
                return handleParam;
            }
        }
    }
}
