using System.Drawing;

namespace PidgeonCarrier.Game
{
    public class Pidgeon
    {
        public PointF Position { get; private set; }
        public float Velocity { get; private set; }

        private readonly float gravity = 0.5F;
        private readonly float flapStrength = -8f;

        private readonly int width = 30;
        private readonly int height = 30;

        public Pidgeon(float startX, float startY)
        {
            Position = new PointF(startX, startY);
            Velocity = 0;
        }

        public void Update()
        {
            Velocity += gravity;
            Position = new PointF(Position.X, Position.Y + Velocity);
        }

        public void Flap()
        {
            Velocity = flapStrength;
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Yellow, Position.X, Position.Y, width, height);
        }

        public bool IsOutOfBounds(int formHeight)
        {
            return Position.Y < 0 || Position.Y + height > formHeight;
        }

        public RectangleF GetBounds()
        {
            return new RectangleF(Position.X, Position.Y, width, height);
        }
    }
}
