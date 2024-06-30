using Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm() => InitializeComponent();

        private void btnRegisterAndLogin_Click(object sender, EventArgs e)
        {
            var loginCheck = Regex.IsMatch(txtUserName.Text, @"^[a-zA-Z][a-zA-Z0-9]{2,16}$", RegexOptions.None);
            var pwCheck = Regex.IsMatch(txtPassword.Text, @"(?=.*\d)(?=.*[A-Z]).{8,16}", RegexOptions.None);
            var pwReCheck = string.Equals(txtPassword.Text, txtRepeatPassword.Text, StringComparison.Ordinal);
            var errorMessage = "";
            if (!loginCheck)
                errorMessage += "The username must contain only letters and digits and be at least 3 characters long.\r\n";
            if (!pwCheck)
                errorMessage += "The password must contain at least one capital letter and one digit, and be at least 8 characters long.\r\n";
            if (!pwReCheck)
                errorMessage += "The password and the confirmation password must match.";
            if (loginCheck && pwCheck && pwReCheck) 
            {
                if (LibraryContext.Db.Users.FirstOrDefault(v => v.Name.Equals(txtUserName.Text, StringComparison.OrdinalIgnoreCase)) != null)
                    MessageBox.Show($"User with name {txtUserName.Text} already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    var newUser = new User { Name = txtUserName.Text, Password = txtPassword.Text };
                    LibraryContext.Db.Users.Add(newUser);
                    LibraryContext.Db.SaveChanges();
                    MessageBox.Show($"User with name {txtUserName.Text} registered successfully!", "Succeess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.StartAndSavePosition(new LoginForm());
                }
            }
            else
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnBack_Click(object sender, EventArgs e) => this.StartAndSavePosition(new LoginForm());

        private void txtRepeatPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)13))
                btnRegisterAndLogin_Click(sender, e);
        }
    }
}
