namespace PigeonCarrier.Game
{
    public abstract class Obstacle
    {
        public int X { get; protected set; }
        public int Width { get; protected set; }
        public bool IsFinish { get; set; } = false;
        public bool HasBeenPassed { get; set; } = false;

        public Obstacle(int x, int width)
        {
            X = x;
            Width = width;
        }

        public virtual void Update(float speed)
        {
            X -= (int)speed;
        }

        public virtual void Reset(int startX)
        {
            X = startX;
            HasBeenPassed = false;
        }

        public abstract void Draw(Graphics g, int clientHeight);

        public abstract bool CollidesWith(Pigeon pigeon);  
    }
}
