using Library.Forms;
using Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            lblTotalUsers.Text += LibraryContext.Db.Users.Count();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
                MessageBox.Show("Enter Login & Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                var foundUser = LibraryContext.Db.Users.FirstOrDefault(v => v.Name.Equals(txtUsername.Text, StringComparison.Ordinal));
                if (foundUser == null)
                    MessageBox.Show("Login is not exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (foundUser.Password.Equals(txtPassword.Text, StringComparison.Ordinal))
                    {
                        User.CurrentUser = foundUser;
                        this.StartAndSavePosition(new WelcomeForm());
                    }
                    else
                        MessageBox.Show("Wrong Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e) => this.StartAndSavePosition(new RegisterForm());

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)13))
                btnLogin_Click(sender, e);
        }
    }
}
