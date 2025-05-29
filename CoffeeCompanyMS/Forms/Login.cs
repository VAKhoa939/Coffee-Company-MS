using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CoffeeCompanyMS.Forms.Authentication;

namespace CoffeeCompanyMS.UI.Authentication
{
    public partial class Login : Form
    {
        private const string SERVER_NAME = "DESKTOP-2S48EVN";

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            handleLogin();
        }

        private void handleLogin()
        {
            string email = textBoxGmail.Text, password = textBoxPassword.Text;
            if (email == "" || password == "")
            {
                MessageBox.Show("Please fill all text boxes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool result = UserSession.Instance.Start(SERVER_NAME, email, password);
            if (!result) return;

            textBoxGmail.Text = string.Empty;
            textBoxPassword.Text = string.Empty;
            Hide();

            Program.mainForm = new Main();
            Program.mainForm.Show();
        }
        
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                handleLogin();
            }
        }
    }
}
