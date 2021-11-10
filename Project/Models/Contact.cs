using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int  ID { get; set; }
        public String firtName { get; set; }
        public String LastName { get; set; }

        public String email { get; set; }
        public String Subject { get; set; }
        public String Message { get; set; }

        public int Status { get; set; }

        public DateTime CreateDate { get; set; }

    }
}