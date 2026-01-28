namespace PigeonCarrier.UI
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStartGame = new Button();
            btnHighScores = new Button();
            btnExit = new Button();
            resetLevels = new Button();
            resetHighScores = new Button();
            resetData = new Button();
            SuspendLayout();
            // 
            // btnStartGame
            // 
            btnStartGame.Location = new Point(329, 112);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(127, 29);
            btnStartGame.TabIndex = 0;
            btnStartGame.Text = "Start Game";
            btnStartGame.UseVisualStyleBackColor = true;
            btnStartGame.Click += BtnStartGame_Click;
            // 
            // btnHighScores
            // 
            btnHighScores.Location = new Point(329, 149);
            btnHighScores.Name = "btnHighScores";
            btnHighScores.Size = new Size(127, 29);
            btnHighScores.TabIndex = 1;
            btnHighScores.Text = "High Scores";
            btnHighScores.UseVisualStyleBackColor = true;
            btnHighScores.Click += BtnHighScores_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(329, 184);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(127, 29);
            btnExit.TabIndex = 2;
            btnExit.Text = "Close Game";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += BtnExit_Click;
            // 
            // resetLevels
            // 
            resetLevels.BackColor = Color.FromArgb(255, 255, 128);
            resetLevels.Location = new Point(581, 386);
            resetLevels.Name = "resetLevels";
            resetLevels.Size = new Size(65, 52);
            resetLevels.TabIndex = 3;
            resetLevels.Text = "Reset Levels";
            resetLevels.UseVisualStyleBackColor = false;
            resetLevels.Click += BtnResetStory_Click;
            // 
            // resetHighScores
            // 
            resetHighScores.BackColor = Color.FromArgb(255, 255, 128);
            resetHighScores.Location = new Point(652, 386);
            resetHighScores.Name = "resetHighScores";
            resetHighScores.Size = new Size(65, 52);
            resetHighScores.TabIndex = 4;
            resetHighScores.Text = "Reset Scores";
            resetHighScores.UseVisualStyleBackColor = false;
            resetHighScores.Click += BtnResetHighScores_Click;
            // 
            // resetData
            // 
            resetData.BackColor = Color.FromArgb(255, 255, 128);
            resetData.Location = new Point(723, 386);
            resetData.Name = "resetData";
            resetData.Size = new Size(65, 52);
            resetData.TabIndex = 5;
            resetData.Text = "Reset All";
            resetData.UseVisualStyleBackColor = false;
            resetData.Click += BtnResetAll_Click;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(resetData);
            Controls.Add(resetHighScores);
            Controls.Add(resetLevels);
            Controls.Add(btnExit);
            Controls.Add(btnHighScores);
            Controls.Add(btnStartGame);
            Name = "MenuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MenuForm";
            ResumeLayout(false);
        }

        #endregion

        private Button btnStartGame;
        private Button btnHighScores;
        private Button btnExit;
        private Button resetLevels;
        private Button resetHighScores;
        private Button resetData;
    }
}