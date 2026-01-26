namespace PigeonCarrier.Game
{
    public class StoryLevel
    {
        public int LevelNumber { get; init; }
        public GameLevel Type { get; init; }
        public int ObstaclesToPass { get; init; } = 0;
    }
}
