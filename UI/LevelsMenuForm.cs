using PidgeonCarrier.Game;

namespace PidgeonCarrier.UI
{
    public partial class LevelsMenuForm : Form
    {
        public LevelsMenuForm()
        {
            InitializeComponent();

            lstLevels.Items.Add("Endless");
            lstLevels.Items.Add("StoryMode");
            lstLevels.SelectedIndex = 0;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            GameLevel selectedLevel = lstLevels.SelectedIndex switch
            {
                0 => GameLevel.Endless,
                1 => GameLevel.StoryMode,
                _ => GameLevel.Endless
            };

            MainForm gameForm = new(selectedLevel);
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
    }
}
