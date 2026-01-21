namespace PidgeonCarrier.UI
{
    partial class HighScoresForm
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
            lstHighScores = new ListBox();
            SuspendLayout();
            // 
            // lstHighScores
            // 
            lstHighScores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstHighScores.FormattingEnabled = true;
            lstHighScores.Location = new Point(12, 12);
            lstHighScores.Name = "lstHighScores";
            lstHighScores.Size = new Size(776, 424);
            lstHighScores.TabIndex = 0;
            // 
            // HighScoresForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lstHighScores);
            Name = "HighScoresForm";
            Text = "HighScoresForm";
            ResumeLayout(false);
        }

        #endregion

        private ListBox lstHighScores;
    }
}