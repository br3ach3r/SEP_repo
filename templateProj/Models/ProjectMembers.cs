using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace templateProj.Models
{
    [Table("proj_members")]
    public class ProjectMembers 
    {
        [Key, Column(Order = 0)]
        public int Pid { get; set; }
        [Key, Column(Order = 1)]
        public string Uname { get; set; }
    }
}