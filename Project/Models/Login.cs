using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Login
    {
        [Key]
        [Display(Name = "Email")]
        public string UserMail { get; set; }
        [Display(Name = "Password")]
        public string UserPassword { get; set; }
    }
}