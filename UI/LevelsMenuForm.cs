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
            //GameLevel selectedLevel = lstLevels.SelectedIndex switch
            //{
            //    0 => GameLevel.Endless,
            //    1 => GameLevel.ChallengeMode,
            //    _ => GameLevel.Endless
            //};

            //MainForm gameForm = new(selectedLevel);
            //Hide();
            //gameForm.ShowDialog();
            //Show();

            //if (gameForm.ExitResult == GameExitResult.ReturnToLevels)
            //{
            //    return;
            //}

            MainForm gameForm;

            if (lstLevels.SelectedIndex == 0)
            {
                gameForm = new MainForm(GameLevel.Endless);
            }
            else if (lstLevels.SelectedIndex >= 1)
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

            //if (gameForm.ExitResult == GameExitResult.LevelComplete)
            //{
            //    StoryManager.UnlockNextLevel();
            //}
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
