using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    [Table("Users")]
    public partial class User
    {
        [Key]
        public int UserID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "The {0} is required")]
        [Display(Name = "Name")]

        public string UserName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "The {0} is required")]
        
        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^(84|0[3|5|7|8|9])+([0-9]{8})\b",
         ErrorMessage = "Characters are not allowed.")]
        [Required(ErrorMessage = "The {0} is required")]
        [Display(Name = "Phone Number")]
        public string UserPhone { get; set; }

        [Display(Name = "Password")]
        public string UserPassword { get; set; }

        public int? RoleID { get; set; }

        [StringLength(100)]
        [Display(Name = "Address")]
        public string UserAddress { get; set; }

        //public virtual ICollection<Order> Order { get; set; }

        public virtual Roles Roles { get; set; }

        [NotMapped]
        public String ComformPassword { get; set; }
    }
}