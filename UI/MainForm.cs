namespace PidgeonCarrier
{
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private readonly Timer _gameTimer = new();
        private const int TargetFps = 60;

        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
            KeyPreview = true;

            _gameTimer.Interval = 1000 / TargetFps;
            _gameTimer.Tick += GameLoop;
            _gameTimer.Start();
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            UpdateGame();
            Invalidate();
        }

        private void UpdateGame()
        {
            // Game logic will go here
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGame(e.Graphics);
        }

        private void DrawGame(Graphics graphics)
        {
            graphics.Clear(Color.SkyBlue);
        }
    }
}
