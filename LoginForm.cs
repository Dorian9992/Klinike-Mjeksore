
using System;
using System.Linq;
using System.Windows.Forms;

namespace KlinikeMjekesore
{
    public class LoginForm : Form
    {
        private TextBox txtEmail = new TextBox { PlaceholderText = "Email", Left = 20, Top = 20, Width = 200 };
        private TextBox txtPassword = new TextBox { PlaceholderText = "Fjalëkalimi", Left = 20, Top = 60, Width = 200, UseSystemPasswordChar = true };
        private Button btnLogin = new Button { Text = "Hyr", Left = 20, Top = 100, Width = 200 };

        public LoginForm()
        {
            this.Text = "Hyrje në Sistem";
            this.Controls.Add(txtEmail);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
            this.ClientSize = new System.Drawing.Size(250, 160);
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            using var db = new KlinikaDbContext();
            var user = db.Perdoruesit.FirstOrDefault(u => u.Email == txtEmail.Text);
            if (user != null && user.FjalekalimiHash == txtPassword.Text)
            {
                MessageBox.Show($"Mirësevjen, {user.Emri}! Roli: {user.Roli}");
            }
            else
            {
                MessageBox.Show("Email ose fjalëkalim i pasaktë.");
            }
        }
    }
}
