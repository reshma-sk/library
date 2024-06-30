using Library.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Forms
{
    public class LibraryContext : DbContext
    {
        public static LibraryContext Db { get; set; }
        static LibraryContext() => Database.SetInitializer<LibraryContext>(new LibraryContextInitializer());
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasRequired(v => v.User);
            base.OnModelCreating(modelBuilder);
        }
    }
}
