using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    [Table("Category")]
    public class Categories
    {
        public Categories()
        {
            Product = new HashSet<Product>();
        }
        [Key]
        public int CategoryID { get; set; }
        [Required, MinLength(50, ErrorMessage = "Minimum length is 5")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Minimum length is 5")]
        public string CategoryName { get; set; }
        public virtual ICollection<Product> Product { get; set; }

    }
}