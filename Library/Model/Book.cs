using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Book
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Title { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Author { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Description { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Genre { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Publisher { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string PublishedDate { get; set; }
        public User User { get; set; }
    }
}
