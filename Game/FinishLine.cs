namespace PidgeonCarrier.Game
{
    public class FinishLine
    {
        public int X { get; private set; }
        public int Width { get; } = 20;

        public FinishLine(int startX)
        {
            X = startX;
        }

        public void Update(int speed)
        {
            X -= speed;
        }

        public Rectangle GetBounds(int formHeight)
        {
            return new Rectangle(X, 0, Width, formHeight);
        }

        public void Draw(Graphics graphics, int formHeight)
        {
            Brush brush = new SolidBrush(Color.Gold);
            graphics.FillRectangle(brush, X, 0, Width, formHeight);
        }
    }
}
