namespace PidgeonCarrier.UI
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form promt = new()
            {
                Width = 300,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new() { Left = 10, Top = 20, Text = text, Width = 260 };
            TextBox textBox = new() { Left = 10, Top = 50, Width = 260 };
            Button confirmation = new() { 
                Text = "OK", 
                Left = 200, 
                Width = 70, 
                Top = 90, 
                Height = 30,
                DialogResult = DialogResult.OK,
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Standard
            };
            Button cancel = new() { 
                Text = "Cancel", 
                Left = 120, 
                Width = 70, 
                Top = 90, 
                Height = 30, 
                DialogResult = DialogResult.Cancel,
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Standard
            };

            confirmation.Click += (sender, e) => { promt.Close(); };
            cancel.Click += (sender, e) => { textBox.Text = ""; promt.Close(); };

            promt.Controls.Add(textLabel);
            promt.Controls.Add(textBox);
            promt.Controls.Add(confirmation);
            promt.Controls.Add(cancel);
            
            promt.AcceptButton = confirmation;
            promt.CancelButton = cancel;

            return promt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
