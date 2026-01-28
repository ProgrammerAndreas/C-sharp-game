using PigeonCarrier.Game.Enums;
using System.Text.Json;

namespace PigeonCarrier.Game
{
    public static class StoryManager
    {
        public static int CurrentStoryLevel { get; set; } = 0;

        private static readonly string SavePath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "PigeonCarrier",
                "story_progress.json"
            );

        public static readonly List<StoryLevel> Levels =
        [
            new StoryLevel { LevelNumber = 0, Type = GameLevel.ChallengeMode },
            new StoryLevel { LevelNumber = 1, Type = GameLevel.LevelOne },
            new StoryLevel { LevelNumber = 2, Type = GameLevel.LevelTwo }
        ];

        public static StoryLevel GetCurrentLevel() => Levels[CurrentStoryLevel];

        public static void UnlockNextLevel()
        {
            if (CurrentStoryLevel < Levels.Count - 1)
            {
                CurrentStoryLevel++;
                SaveProgress();
            }
        }

        public static void LoadProgress()
        {
            try
            {
                if (!File.Exists(SavePath))
                    return;

                string json = File.ReadAllText(SavePath);
                var data = JsonSerializer.Deserialize<StoryProgress>(json);

                if (data != null)
                    CurrentStoryLevel = Math.Clamp(
                        data.CurrentStoryLevel,
                        0,
                        Levels.Count - 1
                    );
            }
            catch
            {
                CurrentStoryLevel = 0;
            }
        }

        public static void ResetProgress()
        {
            CurrentStoryLevel = 0;

            try
            {
                if (File.Exists(SavePath))
                    File.Delete(SavePath);
            }
            catch
            { }
        }

        private static void SaveProgress()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SavePath)!);

            var data = new StoryProgress
            {
                CurrentStoryLevel = CurrentStoryLevel
            };

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(SavePath, json);
        }
    }
}
