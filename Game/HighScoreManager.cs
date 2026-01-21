using System.Text.Json;

namespace PidgeonCarrier.Game
{
    public static class HighScoreManager
    {
        private static readonly string FilePath = "highscore.json";
        private static readonly int MaxScores = 10;

        public static List<HighScoreEntry> LoadScores()
        {
            if (!File.Exists(FilePath)) return [];
            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<HighScoreEntry>>(json) ?? [];
        }

        public static void SaveScores(List<HighScoreEntry> scores)
        {
            string json = JsonSerializer.Serialize(scores);
            File.WriteAllText(FilePath, json);
        }

        public static void AddScore(string name, int score)
        {
            var scores = LoadScores();
            scores.Add(new HighScoreEntry { Name = name, Score = score });
            scores = [.. scores.OrderByDescending(s => s.Score).Take(MaxScores)];
            SaveScores(scores);
        }

        public static bool IsHighScore(int score)
        {
            var scores = LoadScores();
            if (scores.Count < MaxScores) return true;
            return score > scores.Min(s => s.Score);
        }
    }
}
