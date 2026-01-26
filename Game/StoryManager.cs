namespace PigeonCarrier.Game
{
    public static class StoryManager
    {
        public static int CurrentStoryLevel { get; set; } = 0;

        public static readonly List<StoryLevel> Levels =
        [
            new StoryLevel { LevelNumber = 0, Type = GameLevel.ChallengeMode },
            new StoryLevel { LevelNumber = 1, Type = GameLevel.LevelOne, ObstaclesToPass = 10 }
        ];

        public static StoryLevel GetCurrentLevel() => Levels[CurrentStoryLevel];

        public static void UnlockNextLevel()
        {
            if (CurrentStoryLevel < Levels.Count - 1)
                CurrentStoryLevel++;
        }
    }
}
