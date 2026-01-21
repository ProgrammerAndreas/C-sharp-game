using PidgeonCarrier.Game;

namespace PidgeonCarrier.UI
{
    public partial class HighScoresForm : Form
    {
        public HighScoresForm()
        {
            InitializeComponent();
            LoadHighScores();
        }

        private void LoadHighScores()
        {
            var scores = HighScoreManager.LoadScores();
            lstHighScores.Items.Clear();

            foreach (var entry in scores)
            {
                lstHighScores.Items.Add($"{entry.Name} - {entry.Score}");
            }
        }
    }
}
