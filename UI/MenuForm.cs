namespace PidgeonCarrier.UI
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            MainForm gameForm = new MainForm();
            gameForm.Show();
            Hide();
            gameForm.FormClosed += (s, args) => Show();
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            HighScoresForm highScoresForm = new HighScoresForm();
            highScoresForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
