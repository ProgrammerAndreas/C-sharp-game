namespace PigeonCarrier
{
    using PigeonCarrier.Game;
    using PigeonCarrier.UI;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        public GameExitResult ExitResult { get; private set; } = GameExitResult.Restart;
        
        private readonly Timer _gameTimer = new();
        private const int TargetFps = 60;

        private Pigeon _pigeon;

        private readonly List<Obstacle> _obstacles = [];
        private const float ObstacleSpeed = 4f;
        
        private const int ObstacleSpacing = 300;

        private int _score = 0;

        private readonly GameLevel _level;
        private int _obstaclesPassed;
        private int _obstaclesToPass;

        private bool _isGameOver = false;

        private readonly List<Envelope> _envelopes = [];
        private int _envelopesRequired;
        private int _envelopesCollected = 0;

        private bool _lastGapHadEnvelope = false;
        private Random _rand = new();

        public MainForm(GameLevel level, int obstaclesToPass = 0)
        {
            InitializeComponent();
            _level = level;
            _obstaclesToPass = obstaclesToPass;
            ApplyLevelSettings();

            DoubleBuffered = true;
            KeyPreview = true;

            switch (_level)
            {
                case GameLevel.ChallengeMode:
                    SetUpPipesLevel();      
                    break;
                case GameLevel.LevelOne:
                    SetUpLevelOne();
                    break;
                case GameLevel.Endless:
                    SetUpEndlessLevel();
                    break;
                default:
                    SetUpEndlessLevel();
                    break;
            }

            _gameTimer.Interval = 1000 / TargetFps;
            _gameTimer.Tick -= GameLoop;
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

            if (_level == GameLevel.LevelOne)
            {
                _envelopes.RemoveAll(e => e.Bounds.X + e.Bounds.Width < 0);

                foreach (var envelope in _envelopes)
                {
                    envelope.Update(ObstacleSpeed);

                    if (envelope.Collected) continue;

                    foreach (var hitbox in _pigeon.GetHitBoxes())
                    {
                        if (envelope.TryCollect(hitbox))
                        {
                            _envelopesCollected++;
                            break;
                        }
                    }
                }

                var lastTree = _obstacles.Last();
                if (lastTree.X + lastTree.Width < ClientSize.Width)
                {
                    var newTree = new Tree(lastTree.X + ObstacleSpacing, ClientSize.Height);
                    _obstacles.Add(newTree);

                    bool placeEnvelope = !_lastGapHadEnvelope && _envelopes.Count < _envelopesRequired && _rand.NextDouble() < 0.5;
                    if (placeEnvelope)
                    {
                        int envelopeX = lastTree.X + ObstacleSpacing / 2;
                        int envelopeY = ClientSize.Height - 80 - _rand.Next(0, 30);
                        _envelopes.Add(new Envelope(envelopeX, envelopeY));
                        _lastGapHadEnvelope = true;
                    }
                    else
                    {
                        _lastGapHadEnvelope = false;
                    }

                    if (_envelopesCollected >= _envelopesRequired)
                    {
                        newTree.IsFinish = true;
                    }
                }

                if (_envelopesCollected >= _envelopesRequired && _obstacles.Last().IsFinish)
                {
                    HandleLevelComplete();
                    return;
                }
            }

            foreach (var obstacle in _obstacles)
            {
                obstacle.Update(ObstacleSpeed);

                if (_level == GameLevel.Endless && obstacle.X + obstacle.Width < 0)
                {
                    int maxX = _obstacles.Max(t => t.X);
                    obstacle.Reset(maxX + ObstacleSpacing);
                    _score++;
                }

                if (obstacle.CollidesWith(_pigeon))
                {
                    HandleGameOver();
                    return;
                }

                if (_level != GameLevel.LevelOne && !obstacle.HasBeenPassed && obstacle.X + obstacle.Width < _pigeon.Position.X)
                {
                    obstacle.HasBeenPassed = true;
                    _obstaclesPassed++;

                    if (obstacle.IsFinish)
                    {
                        HandleLevelComplete();
                        return;
                    }
                }
            }

            if (_pigeon.IsOutOfBounds(ClientSize.Height))
            {
                HandleGameOver();
            }
        }

        private void DrawGame(Graphics graphics)
        {
            graphics.Clear(Color.SkyBlue);

            _pigeon.Draw(graphics);

            foreach (var obstacle in _obstacles)
            {
                obstacle.Draw(graphics, ClientSize.Height);
            }

            if (_level == GameLevel.Endless)
            {
                DrawScore(graphics);
            }
            else if (_level == GameLevel.ChallengeMode)
            {
                DrawCountDown(graphics);
            }
            else if (_level == GameLevel.LevelOne)
            {
                foreach (var letter in _envelopes)
                    letter.Draw(graphics);

                DrawLetterProgress(graphics);
            }
        }

        private void ResetGame()
        {
            switch (_level)
            {
                case GameLevel.ChallengeMode:
                    SetUpPipesLevel();
                    break;
                case GameLevel.LevelOne:
                    SetUpLevelOne();
                    break;
                case GameLevel.Endless:
                default:
                    SetUpEndlessLevel();
                    break;
            }

            if (!_gameTimer.Enabled)
                _gameTimer.Start();

            Invalidate();
        }

        private void ApplyLevelSettings()
        {
            switch (_level)
            {
                case GameLevel.Endless:
                    _obstaclesToPass = int.MaxValue;
                    break;

                case GameLevel.ChallengeMode:
                    _obstaclesToPass = 10;
                    break;

                case GameLevel.LevelOne:
                    _obstaclesToPass = 20;
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
            int remaining = Math.Max(0, _obstaclesToPass - _obstaclesPassed);

            Font font = new("Arial", 24, FontStyle.Bold);
            graphics.DrawString(
                $"Remaining: {remaining}",
                font,
                Brushes.Black,
                10,
                10
            );
        }

        private void SetUpPipesLevel()
        {
            _isGameOver = false;
            _obstacles.Clear();
            _obstaclesPassed = 0;

            _pigeon = new Pigeon(100, ClientSize.Height / 2);

            for (int i = 0; i < _obstaclesToPass; i++)
            {
                var pipe = new Pipe(ClientSize.Width + i * ObstacleSpacing, ClientSize.Height);

                if (i == _obstaclesToPass - 1)
                    pipe.IsFinish = true;

                pipe.HasBeenPassed = false;
                _obstacles.Add(pipe);
            }
        }

        private void SetUpLevelOne()
        {
            _isGameOver = false;
            
            _obstacles.Clear();
            _envelopes.Clear();

            _envelopesRequired = 5;
            _envelopesCollected = 0;

            _pigeon = new Pigeon(100, ClientSize.Height / 2);

            int initialTreeCount = 5;
            for (int i = 0; i < initialTreeCount; i++)
            {
                var tree = new Tree(ClientSize.Width + i * ObstacleSpacing, ClientSize.Height);
                _obstacles.Add(tree);
            }

            _obstacles[^1].IsFinish = true;

            _lastGapHadEnvelope = false;
        }

        private void SetUpEndlessLevel()
        {
            _isGameOver = false;
            _obstacles.Clear();
            _score = 0;

            _pigeon = new Pigeon(100, ClientSize.Height / 2);

            for (int i = 0; i < 3; i++)
            {
                _obstacles.Add(new Pipe(ClientSize.Width + i * ObstacleSpacing, ClientSize.Height));
            }
        }

        private void DrawLetterProgress(Graphics g)
        {
            Font font = new("Arial", 18, FontStyle.Bold);
            g.DrawString(
                $"Envelopes: {_envelopesCollected}/{_envelopesRequired}",
                font,
                Brushes.Black,
                10,
                10
            );
        }
    }
}
