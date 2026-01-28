namespace PigeonCarrier.Game.Obstacles
{
    using PigeonCarrier.Game.Items;
    using System.Drawing;

    public class Pipe : Obstacle
    {
        public int GapHeight { get; } = 150;
        public int TopHeight { get; private set; }

        private readonly int _clientHeight;
        private static readonly Random _rand = new();

        public Pipe(int x, int clientHeight)
            : base(x, 60)
        {
            _clientHeight = clientHeight;
            RandomizeHeight();
        }

        public void RandomizeHeight()
        {
            TopHeight = _rand.Next(50, _clientHeight - 50 - GapHeight);
        }

        public override void Reset(int startX)
        {
            base.Reset(startX);
            HasBeenPassed = false;
            RandomizeHeight();
        }

        public override void Draw(Graphics g, int clientHeight)
        {
            Brush brush = IsFinish ? Brushes.Gold : Brushes.Green;
            // Top pipes
            g.FillRectangle(brush, X, 0, Width, TopHeight);
            // Bottom pipes
            g.FillRectangle(brush, X, TopHeight + GapHeight, Width, _clientHeight - (TopHeight + GapHeight));
        }

        public override bool CollidesWith(Pigeon pigeon)
        {
            foreach (var hitbox in pigeon.GetHitBoxes())
            {
                if (hitbox.IntersectsWith(GetTopBounds()) ||
                    hitbox.IntersectsWith(GetBottomBounds()))
                {
                    return true;
                }
            }

            return false;
        }

        private RectangleF GetTopBounds()
        {
            return new RectangleF(X, 0, Width, TopHeight);
        }

        private RectangleF GetBottomBounds()
        {
            return new RectangleF(
                X,
                TopHeight + GapHeight,
                Width,
                _clientHeight - (TopHeight + GapHeight)
            );
        }
    }
}
