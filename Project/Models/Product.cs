using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    [Table("Products")]
    public class Product
    {
        public Product()
        {
            Image = "~/ProductImage/Add.png";
        }

        [Key]
        public int ProductID { get; set; }


        [Column("Description")]
        [Required(ErrorMessage = "The {0} is required")]
        [MaxLength(1000, ErrorMessage = "The {0} cannot exceed {1} characters")]
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        [Column("Name")]
        [Required(ErrorMessage = "The {0} is required")]
        [MaxLength(1000, ErrorMessage = "The {0} cannot exceed {1} characters")]
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public bool? IsNewProduct { get; set; } 
        [Column("Date")]
        public DateTime ProductDate { get; set; }
        public string Image { get; set; }
        [Column("Category")]
        public int CategoryID { get; set; }
        [Column("Tag")]
        public int TagID { get; set; }
        public virtual Categories Categories { get; set; }

        public virtual Tags Tags { get; set; }

        [NotMapped]

        public HttpPostedFileBase ImageUpload { get; set; }
    }
}