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
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
            lblWelcome.Text = $"{User.CurrentUser.Name}\r\n{lblWelcome.Text}";
        }

        private void btnLogout_Click(object sender, EventArgs e) => this.StartAndSavePosition(new LoginForm());
        private void bookListToolStripMenuItem_Click(object sender, EventArgs e) => this.StartAndSavePosition(new BookListForm());
        private void addBookToolStripMenuItem_Click(object sender, EventArgs e) => this.StartAndSavePosition(new AddForm());
    }
}
