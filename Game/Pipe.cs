namespace PigeonCarrier.Game
{
    using System.Drawing;

    public class Pipe
    {
        public float X { get; private set; }
        public float Width { get; } = 60;
        public float GapHeight { get; } = 150;
        public float TopHeight { get; private set; }
        public bool IsFinishPipe { get; set; } = false;
        public bool HasBeenPassed { get; set; } = false;

        private readonly int formHeight;

        public Pipe(float startX, int formHeight)
        {
            X = startX;
            this.formHeight = formHeight;
            RandomizeHeight();
        }

        public void Update(float speed)
        {
            X -= speed;
        }

        public void RandomizeHeight()
        {
            Random rand = new();
            TopHeight = rand.Next(50, formHeight - 50 - (int)GapHeight);
        }

        public void Reset(float newX)
        {
            X = newX;
            RandomizeHeight();
        }

        public void Draw(Graphics g, int formHeight)
        {
            Brush brush = IsFinishPipe ? Brushes.Gold : Brushes.Green;
            // Top pipes
            g.FillRectangle(brush, X, 0, Width, TopHeight);
            // Bottom pipes
            g.FillRectangle(brush, X, TopHeight + GapHeight, Width, formHeight - (TopHeight + GapHeight));
        }

        public RectangleF GetTopBounds()
        {
            return new RectangleF(X, 0, Width, TopHeight);
        }

        public RectangleF GetBottomBounds()
        {
            return new RectangleF(X, TopHeight + GapHeight, Width, formHeight - (TopHeight + GapHeight));
        }
    }
}
