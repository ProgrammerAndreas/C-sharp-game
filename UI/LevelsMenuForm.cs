using PidgeonCarrier.Game;

namespace PidgeonCarrier.UI
{
    public partial class LevelsMenuForm : Form
    {
        public LevelsMenuForm()
        {
            InitializeComponent();

            lstLevels.Items.Add("Classic (Normal)");
            lstLevels.SelectedIndex = 0;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            GameLevel selectedLevel = GameLevel.Classic;

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
