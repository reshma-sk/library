using Library.Forms;
using Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class EditForm : Form
    {
        private Book currentBook;
        public EditForm(Book _currentBook)
        {
            InitializeComponent();
            this.currentBook = _currentBook;
            txtTitle.Text = currentBook.Title;
            txtAuthor.Text = currentBook.Author;
            txtDescription.Text = currentBook.Description;
            txtGenre.Text = currentBook.Genre;
            txtPublisher.Text = currentBook.Publisher;
            txtPublishedDate.Text = currentBook.PublishedDate;
        }

        private void btnApply_Click(object sender, EventArgs e)
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
                if (LibraryContext.Db.Books.FirstOrDefault(v => v.User.Id == User.CurrentUser.Id && currentBook.Id != v.Id && v.Title.Equals(txtTitle.Text, StringComparison.OrdinalIgnoreCase)) != null)
                    MessageBox.Show($"Book with name {txtTitle.Text} already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if(currentBook.Title.Equals(txtTitle.Text, StringComparison.OrdinalIgnoreCase) &&
                        currentBook.Author.Equals(txtAuthor.Text, StringComparison.OrdinalIgnoreCase) &&
                        currentBook.Genre.Equals(txtGenre.Text, StringComparison.OrdinalIgnoreCase) &&
                        currentBook.Description.Equals(txtDescription.Text, StringComparison.OrdinalIgnoreCase) &&
                        currentBook.Publisher.Equals(txtPublisher.Text, StringComparison.OrdinalIgnoreCase) &&
                        currentBook.PublishedDate.Equals(txtPublishedDate.Text, StringComparison.OrdinalIgnoreCase))
                    MessageBox.Show("No changes were made. Please press the \"Discard\" button if you do not wish to make any changes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    var newBook = new Book
                    {
                        Id = currentBook.Id,
                        User = currentBook.User,
                        Title = txtTitle.Text.Truncate(250),
                        Author = txtAuthor.Text.Truncate(250),
                        Genre = txtGenre.Text.Truncate(250),
                        Description = txtDescription.Text.Truncate(250),
                        Publisher = txtPublisher.Text.Truncate(250),
                        PublishedDate = txtPublishedDate.Text.Truncate(250)
                    };
                    var origBook = LibraryContext.Db.Books.First(v => v.Id == currentBook.Id);
                    LibraryContext.Db.Entry(origBook).CurrentValues.SetValues(newBook);
                    LibraryContext.Db.SaveChanges();
                    MessageBox.Show($"Book \"{txtTitle.Text}\" successfully edited!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.StartAndSavePosition(new BookListForm());
                }
            }
            else
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnDiscard_Click(object sender, EventArgs e) => this.StartAndSavePosition(new BookListForm());
        private void btnLogout_Click(object sender, EventArgs e) => this.StartAndSavePosition(new LoginForm());
    }
}
