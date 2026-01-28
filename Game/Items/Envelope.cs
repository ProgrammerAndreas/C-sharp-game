namespace PigeonCarrier.Game.Items
{
    public class Envelope
    {
        public RectangleF Bounds { get; private set; }
        public bool Collected { get; private set; }

        public Envelope(int x, int y)
        {
            Bounds = new RectangleF(x, y, 24, 24);
            Collected = false;
        }

        public void Draw(Graphics g)
        {
            if (Collected) return;

            g.DrawImage(Properties.Resources.envelope, Bounds);
        }

        public bool TryCollect(RectangleF hitbox)
        {
            if (Collected)
                return false;

            if (hitbox.IntersectsWith(GetBounds()))
            {
                Collected = true;
                return true;
            }

            return false;
        }

        public void Update(float speed)
        {
            Bounds = new RectangleF(
                Bounds.X - speed,
                Bounds.Y,
                Bounds.Width,
                Bounds.Height
            );
        }

        public RectangleF GetBounds() => Bounds;
    }
}
