namespace PigeonCarrier.Game.Items
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
            //foreach (var hitbox in GetHitBoxes())
            //{
            //    g.DrawRectangle(pen, hitbox);
            //}
#endif
        }

        public bool IsOutOfBounds(int formHeight)
        {
            return Position.Y < 0 || Position.Y + height > formHeight;
        }

        public IEnumerable<RectangleF> GetHitBoxes()
        {
            // Body hitbox
            yield return new RectangleF(
                Position.X + 15,
                Position.Y,
                width - 35,
                height - 3
            );

            // Head hitbox
            yield return new RectangleF(
                Position.X + width - 18,
                Position.Y + 17,
                14,
                height - 24
            );

            // Tail hitbox
            yield return new RectangleF(
                Position.X + width - 70,
                Position.Y + 25,
                14,
                height - 26
            );
        }
    }
}
