using PigeonCarrier.Game;

namespace PigeonCarrier.UI
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            Hide();

            LevelsMenuForm levelsMenu = new();
            levelsMenu.ShowDialog();

            Show();
        }

        private void BtnHighScores_Click(object sender, EventArgs e)
        {
            HighScoresForm highScoresForm = new HighScoresForm();
            highScoresForm.ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnResetStory_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Are you sure you want to reset level progression?",
                "Confirm Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
                StoryManager.ResetProgress();
        }

        private void BtnResetHighScores_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Are you sure you want to reset the high score?",
                "Confirm Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
                HighScoreManager.ResetHighScores();
        }

        private void BtnResetAll_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Are you sure you want to reset all game data",
                "Confirm Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
                ResetAll();
        }

        private static void ResetAll()
        {
            StoryManager.ResetProgress();
            HighScoreManager.ResetHighScores();
        }
    }
}
