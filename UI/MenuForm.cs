namespace PidgeonCarrier.UI
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
    }
}
