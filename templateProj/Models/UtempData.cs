using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace templateProj.Models
{
    [Table("usertempdata")]
    public class UtempData
    {
        [Key]
        public string Uid { get; set; }
        public string Ustories { get; set; } 
    }
}