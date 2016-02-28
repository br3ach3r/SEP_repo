using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace templateProj.Models
{
    [Table("Project_Notific")]
    public class ProjectNotification
    {
        [Key, Column(Order = 0)]
        public int Nid { get; set; }
        [Key, Column(Order = 1)]
        public int Pid { get; set; }
        public string Visibility { get; set; }
        public string MemSug { get; set; } 
    }
}