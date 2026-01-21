namespace PidgeonCarrier
{
    using PidgeonCarrier.Game;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private readonly Timer _gameTimer = new();
        private const int TargetFps = 60;

        private Pidgeon _pidgeon;

        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
            KeyPreview = true;

            _pidgeon = new Pidgeon(100, this.ClientSize.Height / 2);

            _gameTimer.Interval = 1000 / TargetFps;
            _gameTimer.Tick += GameLoop;
            _gameTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGame(e.Graphics);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Space)
            {
                _pidgeon.Flap();
            }
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            UpdateGame();
            Invalidate();
        }

        private void UpdateGame()
        {
            _pidgeon.Update();

            if (_pidgeon.IsOutOfBounds(this.ClientSize.Height))
            {
                _gameTimer.Stop();
                MessageBox.Show("Game Over!");
                _pidgeon = new Pidgeon(100, this.ClientSize.Height / 2);
                _gameTimer.Start();
            }
        }

        private void DrawGame(Graphics graphics)
        {
            graphics.Clear(Color.SkyBlue);

            _pidgeon.Draw(graphics);
        }
    }
}
