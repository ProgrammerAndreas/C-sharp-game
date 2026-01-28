
using PigeonCarrier.Game.Items;

namespace PigeonCarrier.Game.Obstacles
{
    public class Mountain : Obstacle
    {
        private readonly int _groundY;
        private readonly int _height;

        public Mountain(int x, int groundY)
            : base(x, 120)
        {
            _groundY = groundY;
            _height = Random.Shared.Next(80, 220);
        }

        public override void Draw(Graphics g, int clientHeight)
        {
            Point[] triangle =
            {
                new(X, _groundY),
                new(X + Width / 2, _groundY - _height),
                new(X + Width, _groundY)
            };

            g.FillPolygon(Brushes.DarkSlateGray, triangle);
        }

        public override bool CollidesWith(Pigeon pigeon)
        {
            Rectangle mountainBounds = new(
                X,
                _groundY - _height,
                Width,
                _height
            );

            foreach (var hitbox in pigeon.GetHitBoxes())
            {
                if (hitbox.IntersectsWith(mountainBounds))
                    return true;
            }

            return false;
        }
    }
}
