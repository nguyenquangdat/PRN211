using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    [Table("Role")]
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<User>();
        }
        [Display(Name = "ID")]
        [Key]
        public int RoleID { get; set; }
        [Display(Name = "Name")]
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}