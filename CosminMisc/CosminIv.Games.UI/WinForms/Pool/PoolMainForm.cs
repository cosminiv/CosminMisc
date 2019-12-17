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
        private double _ballAngle = -1;

        private readonly int _ballWidth = 25;
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
            UpdateBallPosition();

            panel1.Invalidate();
        }

        private void UpdateBallPosition()
        {
            _ballOldX = _ballX;
            _ballOldY = _ballY;

            _ballX += _ballSpeed * Math.Cos(_ballAngle);
            _ballY += _ballSpeed * Math.Sin(_ballAngle);

            if (_ballX < 0)
            {
                _ballX = 0;
                //_ballAngle = ...;
            }

        }

        private void DrawBall()
        {
            using (Graphics graphics = panel1.CreateGraphics())
            {
                graphics.FillEllipse(_ballDeleteBrush, (int)_ballOldX, (int)_ballOldY, _ballWidth, _ballWidth);
                graphics.FillEllipse(_ballBrush, (int)_ballX, (int)_ballY, _ballWidth, _ballWidth);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawBall();
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
