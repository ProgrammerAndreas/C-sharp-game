namespace PidgeonCarrier
{
    using PidgeonCarrier.Game;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private readonly Timer _gameTimer = new();
        private const int TargetFps = 60;

        private Pidgeon _pidgeon;

        private readonly List<Tree> _trees = [];
        private const float TreeSpeed = 4f;
        private const int TreeSpacing = 300;

        private int _score = 0;

        public MainForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
            KeyPreview = true;

            _pidgeon = new Pidgeon(100, ClientSize.Height / 2);

            for (int i = 0; i < 3; i++)
            {
                _trees.Add(new Tree(ClientSize.Width + i * TreeSpacing, ClientSize.Height));
            }

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

            if (_pidgeon.IsOutOfBounds(ClientSize.Height))
            {
                _gameTimer.Stop();
                MessageBox.Show("Game Over!");
                _pidgeon = new Pidgeon(100, this.ClientSize.Height / 2);
                _gameTimer.Start();
            }

            foreach (var tree in _trees)
            {
                tree.Update(TreeSpeed);

                if (tree.X + tree.Width < 0)
                {
                    tree.Reset(ClientSize.Width);
                    _score++;
                }

                if (_pidgeon.GetBounds().IntersectsWith(tree.GetTopBounds()) ||
                    _pidgeon.GetBounds().IntersectsWith(tree.GetBottomBounds()))
                {
                    _gameTimer.Stop();
                    MessageBox.Show($"Game Over! Your score: {_score}");
                    ResetGame();
                    return;
                }
            }
        }

        private void DrawGame(Graphics graphics)
        {
            graphics.Clear(Color.SkyBlue);

            _pidgeon.Draw(graphics);

            foreach (var tree in _trees)
            {
                tree.Draw(graphics, ClientSize.Height);
            }

            Font scoreFont = new("Arial", 24, FontStyle.Bold);
            graphics.DrawString($"Score: {_score}", scoreFont, Brushes.Black, 10, 10);
        }

        private void ResetGame()
        {
            _pidgeon = new Pidgeon(100, ClientSize.Height / 2);

            for (int i = 0; i < _trees.Count; i++)
            {
                _trees[i].Reset(ClientSize.Width + i * TreeSpacing);
            }

            _score = 0;
            _gameTimer.Start();
        }
    }
}
