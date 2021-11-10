using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    [Table("Test")]
    public class Test
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public String name { get; set; }

    }
}