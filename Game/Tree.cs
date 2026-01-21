namespace PidgeonCarrier.Game
{
    using System.Drawing;

    public class Tree
    {
        public float X { get; private set; }
        public float Width { get; } = 60;
        public float GapHeight { get; } = 150;
        public float TopHeight { get; private set; }

        private readonly int formHeight;

        public Tree(float startX, int formHeight)
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
            // Top trees (remove this later)
            g.FillRectangle(Brushes.Green, X, 0, Width, TopHeight);
            // Bottom trees
            g.FillRectangle(Brushes.Green, X, TopHeight + GapHeight, Width, formHeight - (TopHeight + GapHeight));
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
