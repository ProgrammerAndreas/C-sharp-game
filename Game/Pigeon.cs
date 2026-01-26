namespace PigeonCarrier.Game
{
    public class Pigeon
    {
        public PointF Position { get; private set; }
        public float Velocity { get; private set; }

        private readonly float gravity = 0.5F;
        private readonly float flapStrength = -8f;

        private readonly int width = 70;
        private readonly int height = 32;

        private readonly Image _sprite;

        private const int HitboxPaddingX = 8;
        private const int HitboxPaddingY = 6;

        public Pigeon(float startX, float startY)
        {
            Position = new PointF(startX, startY);
            Velocity = 0;

            _sprite = Properties.Resources.pigeon;
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
            g.DrawImage(_sprite, Position.X, Position.Y, width, height);

#if DEBUG
            //using Pen pen = new(Color.Red, 1);
            //g.DrawRectangle(pen, GetBounds());
#endif
        }

        public bool IsOutOfBounds(int formHeight)
        {
            return Position.Y < 0 || Position.Y + height > formHeight;
        }

        public RectangleF GetBounds()
        {
            return new RectangleF(
                Position.X + HitboxPaddingX,
                Position.Y + HitboxPaddingY,
                width - (HitboxPaddingX * 2),
                height - (HitboxPaddingY * 2));
        }
    }
}
