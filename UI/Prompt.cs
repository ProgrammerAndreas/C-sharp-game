namespace PigeonCarrier.UI
{
    public static class Prompt
    {
        public static string ShowDialog(string message, string title)
        {
            Form promt = new()
            {
                Width = 320,
                Height = 170,
                Text = title,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label textLabel = new() 
            { 
                Text = message, 
                Left = 10, 
                Top = 15, 
                Width = 280,
                Height = 30
            };
            TextBox textBox = new() 
            { 
                Left = 10, 
                Top = 50, 
                Width = 280 
            };
            Button confirmation = new() { 
                Text = "OK", 
                Left = 140, 
                Top = 90, 
                Width = 70, 
                Height = 30,
                DialogResult = DialogResult.OK,
                TextAlign = ContentAlignment.MiddleCenter,
                FlatStyle = FlatStyle.Standard
            };
            Button cancel = new() { 
                Text = "Cancel", 
                Left = 220, 
                Top = 90, 
                Width = 70, 
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
