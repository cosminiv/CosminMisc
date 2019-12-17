using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace CosminIv.Games.UI.WinForms.Pool
{
    public partial class PoolMainForm : Form
    {
        private int _ballX = 0;
        private int _ballY = 100;
        private int _ballOldX;
        private int _ballOldY;
        private readonly int _ballWidth = 25;
        private readonly SolidBrush _ballBrush = new SolidBrush(Color.DarkBlue);
        private readonly SolidBrush _ballDeleteBrush = new SolidBrush(DefaultBackColor);

        private readonly System.Timers.Timer _timer = new System.Timers.Timer(25);

        public PoolMainForm()
        {
            InitializeComponent();

            _timer.Elapsed += TimerOnElapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _ballOldX = _ballX;
            _ballOldY = _ballY;
            _ballX += 2;

            DrawBall();
        }

        private void DrawBall()
        {
            using (Graphics graphics = panel1.CreateGraphics())
            {
                graphics.FillEllipse(_ballDeleteBrush, _ballOldX, _ballOldY, _ballWidth, _ballWidth);
                graphics.FillEllipse(_ballBrush, _ballX, _ballY, _ballWidth, _ballWidth);
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
