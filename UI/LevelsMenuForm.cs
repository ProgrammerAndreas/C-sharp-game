using PigeonCarrier.Game;

namespace PigeonCarrier.UI
{
    public partial class LevelsMenuForm : Form
    {
        public LevelsMenuForm()
        {
            InitializeComponent();

            lstLevels.Items.Add("Endless");
            lstLevels.Items.Add("ChallengeMode");
            lstLevels.Items.Add("Level 1");
            lstLevels.SelectedIndex = 0;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            MainForm gameForm;

            if (lstLevels.SelectedIndex == 0)
            {
                gameForm = new MainForm(GameLevel.Endless);
            }
            else if (lstLevels.SelectedIndex == 1)
            {
                int pipesToBeat = GetNumberOfPipes();

                gameForm = new(GameLevel.ChallengeMode, pipesToBeat);
            }
            else if (lstLevels.SelectedIndex >= 2)
            {
                int levelIndex = lstLevels.SelectedIndex - 1;
                StoryLevel levelData = StoryManager.Levels[levelIndex];

                gameForm = new MainForm(levelData.Type, levelData.ObstaclesToPass);
            }
            else
            {
                gameForm = new MainForm(GameLevel.Endless);
            }

            Hide();
            gameForm.ShowDialog();
            Show();

            if (gameForm.ExitResult == GameExitResult.ReturnToLevels)
            {
                return;
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int GetNumberOfPipes()
        {
            string input = Prompt.ShowDialog(
                "Enter the number of pipes to beat:",
                "Setup"
            );

            if (int.TryParse(input, out int num) && num > 0)
                return num;

            return 10;
        }
    }
}
