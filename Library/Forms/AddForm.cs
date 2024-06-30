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
    public partial class AddForm : Form
    {
        public AddForm() => InitializeComponent();
        
        private void btnAddbook_Click(object sender, EventArgs e)
        {
            var errorMessage = "";
            var titleCheck = string.IsNullOrEmpty(txtTitle.Text);
            var authorCheck = string.IsNullOrEmpty(txtAuthor.Text);
            var genreCheck = string.IsNullOrEmpty(txtGenre.Text);
            if (titleCheck)
                errorMessage += "Enter Title.\r\n";
            if (authorCheck)
                errorMessage += "Enter Author.\r\n";
            if (genreCheck)
                errorMessage += "Enter Genre.\r\n";
            if (!titleCheck && !authorCheck && !genreCheck)
            {
                if (LibraryContext.Db.Books.FirstOrDefault(v => v.User.Id == User.CurrentUser.Id && v.Title.Equals(txtTitle.Text, StringComparison.OrdinalIgnoreCase)) != null)
                    MessageBox.Show($"Book with name {txtTitle.Text} already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    var newBook = new Book
                    {
                        User = User.CurrentUser,
                        Title = txtTitle.Text.Truncate(250),
                        Author = txtAuthor.Text.Truncate(250),
                        Genre = txtGenre.Text.Truncate(250),
                        Description = txtDescription.Text.Truncate(250),
                        Publisher = txtPublisher.Text.Truncate(250),
                        PublishedDate = txtPublishedDate.Text.Truncate(250)
                    };
                    LibraryContext.Db.Books.Add(newBook);
                    LibraryContext.Db.SaveChanges();
                    MessageBox.Show($"Book {txtTitle.Text} successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.StartAndSavePosition(new BookListForm());
                }
            }
            else
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnLogout_Click(object sender, EventArgs e) => this.StartAndSavePosition(new LoginForm());

        private void btnLibrary_Click(object sender, EventArgs e) => this.StartAndSavePosition(new BookListForm());

        private void btnDiscard_Click(object sender, EventArgs e) => this.StartAndSavePosition(new BookListForm());
    }
}
