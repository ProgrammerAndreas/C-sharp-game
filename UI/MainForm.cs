namespace PidgeonCarrier
{
    using PidgeonCarrier.Game;
    using PidgeonCarrier.UI;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        public GameExitResult ExitResult { get; private set; } = GameExitResult.Restart;
        
        private readonly Timer _gameTimer = new();
        private const int TargetFps = 60;

        private Pidgeon _pidgeon;

        private readonly List<Tree> _trees = [];
        private const float TreeSpeed = 4f;
        private const int TreeSpacing = 300;

        private int _score = 0;

        private readonly GameLevel _level;

        private bool _isGameOver = false;

        public MainForm(GameLevel level)
        {
            InitializeComponent();
            _level = level;
            ApplyLevelSettings();

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
            if (_isGameOver) return;
            
            _pidgeon.Update();

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
                    HandleGameOver();
                    return;
                }
            }

            if (_pidgeon.IsOutOfBounds(ClientSize.Height))
            {
                HandleGameOver();
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
            _isGameOver = false;
            _score = 0;

            _pidgeon = new Pidgeon(100, ClientSize.Height / 2);

            for (int i = 0; i < _trees.Count; i++)
            {
                _trees[i].Reset(ClientSize.Width + i * TreeSpacing);
            }

            _gameTimer.Start();
        }

        private void ApplyLevelSettings()
        {
            switch (_level)
            {
                case GameLevel.Classic:
                    break;
            }
        }

        private void HandleGameOver()
        {
            if (_isGameOver) return;

            _isGameOver = true;
            _gameTimer.Stop();

            if (_score > 0 && HighScoreManager.IsHighScore(_score))
            {
                string playerName = Prompt.ShowDialog(
                    "New High Score! Enter your name:",
                    "High Score"
                );

                if (!string.IsNullOrWhiteSpace(playerName))
                {
                    HighScoreManager.AddScore(playerName, _score);
                }
            }

            var result = MessageBox.Show(
                $"Game Over! Your Score: {_score}\nDo you want to restart the game?",
                "Game Over",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                ExitResult = GameExitResult.Restart;
                ResetGame();
            }
            else
            {
                ExitResult = GameExitResult.ReturnToLevels;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
