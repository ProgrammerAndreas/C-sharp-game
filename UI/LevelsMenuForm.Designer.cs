namespace PidgeonCarrier.UI
{
    partial class LevelsMenuForm
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
            lstLevels = new ListBox();
            btnPlay = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // lstLevels
            // 
            lstLevels.FormattingEnabled = true;
            lstLevels.Location = new Point(12, 12);
            lstLevels.Name = "lstLevels";
            lstLevels.Size = new Size(776, 364);
            lstLevels.TabIndex = 0;
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(348, 380);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(94, 29);
            btnPlay.TabIndex = 1;
            btnPlay.Text = "Play";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += BtnPlay_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(348, 415);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(94, 29);
            btnBack.TabIndex = 2;
            btnBack.Text = "Go back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += BtnBack_Click;
            // 
            // LevelsMenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBack);
            Controls.Add(btnPlay);
            Controls.Add(lstLevels);
            Name = "LevelsMenuForm";
            Text = "LevelsMenuForm";
            ResumeLayout(false);
        }

        #endregion

        private ListBox lstLevels;
        private Button btnPlay;
        private Button btnBack;
    }
}