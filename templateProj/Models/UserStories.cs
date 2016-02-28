using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

//using System.Data.Linq.Mapping;
using System.Web;

namespace templateProj.Models
{
    [Table("UserStories")]
    public class UserStories : DbContext
    {
       
        [Key, Column(Order = 0) ]
        public int Pid { get; set; }

       
        [Key, Column(Order = 1)]
        public int USid { get; set; }
       
        public string UserStory { get; set; }
        public string ColorCode { get; set; }
        public string Priority { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
    

    }
}