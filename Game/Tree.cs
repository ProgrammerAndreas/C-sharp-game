
namespace PigeonCarrier.Game
{
    public class Tree : Obstacle
    {
        private readonly int _height;
        private readonly int _groundY;

        public Tree(int x, int groundY)
            : base(x, 50)
        {
            _groundY = groundY;
            _height = 120;
        }

        public override void Draw(Graphics g, int clientHeight)
        {
            g.FillRectangle(Brushes.Brown, X + Width / 4, _groundY - _height, Width / 2, _height);
            g.FillEllipse(Brushes.Green, X - Width / 2, _groundY - _height - 30, Width * 2, 50);
        }

        public override bool CollidesWith(Pigeon pigeon)
        {
            Rectangle treeBounds = new(
            X,
            _groundY - _height,
            Width,
            _height
        );

            foreach (var hitbox in pigeon.GetHitBoxes())
            {
                if (hitbox.IntersectsWith(treeBounds))
                    return true;
            }

            return false;
        }
    }
}
