using PigeonCarrier.Game;
using PigeonCarrier.Game.Enums;

namespace PigeonCarrier.UI
{
    public partial class LevelsMenuForm : Form
    {
        public LevelsMenuForm()
        {
            InitializeComponent();
            PopulateLevels();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            PopulateLevels();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            MainForm gameForm;

            switch (lstLevels.SelectedIndex)
            {
                case 0:
                    gameForm = new MainForm(GameLevel.Endless);
                    break;
                case 1:
                    int pipesToBeat = GetNumberOfPipes();
                    gameForm = new MainForm(GameLevel.ChallengeMode, pipesToBeat);
                    break;
                case 2:
                    gameForm = new MainForm(GameLevel.LevelOne);
                    break;
                case 3:
                    gameForm = new MainForm(GameLevel.LevelTwo);
                    break;
                default:
                    return;
            }

            Hide();
            gameForm.ShowDialog();
            Show();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static int GetNumberOfPipes()
        {
            string input = Prompt.ShowDialog(
                "Enter the number of pipes to beat:",
                "Setup"
            );

            if (int.TryParse(input, out int num) && num > 0)
                return num;

            return 10;
        }

        private void PopulateLevels()
        {
            lstLevels.Items.Clear();

            lstLevels.Items.Add("Endless");
            lstLevels.Items.Add("ChallengeMode");
            lstLevels.Items.Add("Level 1");

            if (StoryManager.CurrentStoryLevel >= 1)
                lstLevels.Items.Add("Level 2");

            lstLevels.SelectedIndex = 0;
        }
    }
}
