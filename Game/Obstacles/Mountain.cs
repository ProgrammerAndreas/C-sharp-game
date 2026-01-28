
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
            _height = Random.Shared.Next(160, 330);
        }

        public override void Draw(Graphics g, int clientHeight)
        {
            Point[] triangle =
            [
                new(X + Width / 2, _groundY - _height),
                new(X, _groundY),
                new(X + Width, _groundY)
            ];

#if DEBUG
            //using Pen pen = new(Color.Red, 1);
            //foreach (var layerRect in GetCollisionLayers())
            //{
            //    g.DrawRectangle(pen, Rectangle.Round(layerRect));
            //}
#endif

            g.FillPolygon(Brushes.DarkSlateGray, triangle);
        }


        public override bool CollidesWith(Pigeon pigeon)
        {
            foreach (var layerRect in GetCollisionLayers())
            {
                foreach (var hitbox in pigeon.GetHitBoxes())
                {
                    if (hitbox.IntersectsWith(layerRect))
                        return true;
                }
            }

            return false;
        }


        private IEnumerable<RectangleF> GetCollisionLayers()
        {
            const int layers = 30;
            float layerHeight = _height / (float)layers;

            for (int i = 0; i < layers; i++)
            {
                float t = i / (float)layers;
                float layerWidth = Width * (1 - t);
                float offsetX = (Width - layerWidth) / 2;

                yield return new RectangleF(
                    X + offsetX,
                    _groundY - (i + 1) * layerHeight,
                    layerWidth,
                    layerHeight
                );
            }
        }

    }
}
