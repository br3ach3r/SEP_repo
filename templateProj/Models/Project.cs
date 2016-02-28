using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace templateProj.Models
{
    [Table("Project")]
    public class Project : DbContext
    {
        [Key]
        public int Pid { get; set; }
        
        public string P_name { get; set; }
        public string budget { get; set; }
        public string duration { get; set; }
        public string tech { get; set; }
        public string Requirements { get; set; }
        public string Docs { get; set; }
        public int members { get; set; }
        public string Manager { get; set; }
        public string Leader { get; set; }
        public string stat { get; set; }

    }
}