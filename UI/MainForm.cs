namespace PidgeonCarrier
{
    using PidgeonCarrier.Game;
    using PidgeonCarrier.UI;
    using PigeonCarrier.Game;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        public GameExitResult ExitResult { get; private set; } = GameExitResult.Restart;
        
        private readonly Timer _gameTimer = new();
        private const int TargetFps = 60;

        private Pigeon _pigeon;

        private readonly List<Tree> _trees = [];
        private const float TreeSpeed = 4f;
        private const int TreeSpacing = 300;

        private int _score = 0;

        private readonly GameLevel _level;
        private int _treesPassed;
        private int _treesToWin;

        private bool _isGameOver = false;

        public MainForm(GameLevel level)
        {
            InitializeComponent();
            _level = level;
            ApplyLevelSettings();

            DoubleBuffered = true;
            KeyPreview = true;

            SetUpLevel();

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
                _pigeon.Flap();
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
            
            _pigeon.Update();

            foreach (var tree in _trees)
            {
                tree.Update(TreeSpeed);

                if (_level == GameLevel.Endless)
                {
                    if (tree.X + tree.Width < 0)
                    {
                        tree.Reset(ClientSize.Width);
                        _score++;
                    }
                }
                else if (_level == GameLevel.StoryMode)
                {
                    if (!tree.IsFinishTree && !tree.HasBeenPassed && tree.X + tree.Width == _pigeon.Position.X)
                    {
                        _treesPassed++;
                        tree.HasBeenPassed = true;
                    }

                    if (tree.IsFinishTree && tree.X + tree.Width == _pigeon.Position.X)
                    {
                        HandleLevelComplete();
                        return;
                    }
                }

                foreach (var hitbox in _pigeon.GetHitBoxes())
                {
                    if (hitbox.IntersectsWith(tree.GetTopBounds()) ||
                        hitbox.IntersectsWith(tree.GetBottomBounds()))
                    {
                        HandleGameOver();
                        return;
                    }
                }
            }

            if (_pigeon.IsOutOfBounds(ClientSize.Height))
            {
                HandleGameOver();
                return;
            }
        }

        private void DrawGame(Graphics graphics)
        {
            graphics.Clear(Color.SkyBlue);

            _pigeon.Draw(graphics);

            foreach (var tree in _trees)
            {
                tree.Draw(graphics, ClientSize.Height);
            }

            if (_level == GameLevel.Endless)
            {
                DrawScore(graphics);
            }
            else if (_level == GameLevel.StoryMode)
            {
                DrawCountDown(graphics);
            }
        }

        private void ResetGame()
        {
            SetUpLevel();

            if (!_gameTimer.Enabled)
                _gameTimer.Start();

            Invalidate();
        }

        private void ApplyLevelSettings()
        {
            switch (_level)
            {
                case GameLevel.Endless:
                    _treesToWin = int.MaxValue;
                    break;

                case GameLevel.StoryMode:
                    _treesToWin = 20;
                    break;
            }
        }

        private void HandleGameOver()
        {
            if (_isGameOver) return;

            _isGameOver = true;
            _gameTimer.Stop();

            if (_level == GameLevel.Endless && _score > 0 && HighScoreManager.IsHighScore(_score))
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

            string message = _level == GameLevel.Endless
                ? $"Game Over! Your Score: {_score}\nDo you want to restart the game?"
                : "Game Over!\nDo you want to restart the game?";

            var result = MessageBox.Show(
                message,
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

        private void HandleLevelComplete()
        {
            _isGameOver = true;
            _gameTimer.Stop();

            MessageBox.Show(
                "Level Complete!\nYou reached the end!",
                "Victory",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            ExitResult = GameExitResult.ReturnToLevels;
            Close();
        }

        private void DrawScore(Graphics graphics)
        {
            Font scoreFont = new("Arial", 24, FontStyle.Bold);
            graphics.DrawString($"Score: {_score}", scoreFont, Brushes.Black, 10, 10);
        }

        private void DrawCountDown(Graphics graphics)
        {
            int remaining = Math.Max(0, _treesToWin - _treesPassed);

            Font font = new("Arial", 24, FontStyle.Bold);
            graphics.DrawString(
                $"Remaining: {remaining}",
                font,
                Brushes.Black,
                10,
                10
            );
        }

        private void SetUpLevel()
        {
            _isGameOver = false;
            _score = 0;
            _treesPassed = 0;

            _pigeon = new Pigeon(100, ClientSize.Height / 2);

            _trees.Clear();

            if (_level == GameLevel.Endless)
            {
                for (int i = 0; i < 3; i++)
                {
                    _trees.Add(new Tree(ClientSize.Width + i * TreeSpacing, ClientSize.Height));
                }
            }
            else if (_level == GameLevel.StoryMode)
            {
                for (int i = 0; i < _treesToWin; i++)
                {
                    var tree = new Tree(ClientSize.Width + i * TreeSpacing, ClientSize.Height);

                    if (i == _treesToWin - 1)
                        tree.IsFinishTree = true;

                    tree.HasBeenPassed = false;
                    _trees.Add(tree);
                }
            }
        }
    }
}
