using System;
using System.Windows.Forms;

namespace LauncherETS2MP
{
    public partial class userLogin : Form
    {
        public userLogin()
        {
            InitializeComponent();
            txtEmail.Text       = Properties.Settings.Default.email;
            txtPassword.Text    = Properties.Settings.Default.password;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.email       = txtEmail.Text;
            Properties.Settings.Default.password    = txtPassword.Text;
            Properties.Settings.Default.Save();
            Close();
        }
    }
}
