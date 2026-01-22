namespace PidgeonCarrier.UI
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
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnExit);
            Controls.Add(btnHighScores);
            Controls.Add(btnStartGame);
            Name = "MenuForm";
            Text = "MenuForm";
            ResumeLayout(false);
        }

        #endregion

        private Button btnStartGame;
        private Button btnHighScores;
        private Button btnExit;
    }
}