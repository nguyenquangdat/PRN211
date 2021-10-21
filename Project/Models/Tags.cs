using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    [Table("Tags")]
    public class Tags
    {
        
        public Tags()
        {
            Product = new HashSet<Product>();
        }
        [Key]
        [Display(Name = "ID")]
        public int TagID { get; set; }

        [Required, MinLength(20, ErrorMessage = "Minimum length is 3")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Minimum length is 3")]
        [Display(Name = "Name")]
        public string TagName { get; set; }
        

        public virtual ICollection<Product> Product { get; set; }
    }
}