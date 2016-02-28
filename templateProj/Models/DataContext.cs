using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace templateProj.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DBcon")         
        {
        } 

        public DbSet<Project> Proj { get; set; }
        public DbSet<UserStories> Ustory { get; set; }

        public DbSet<ProjectNotification> ProjNot { get; set; } 
        public DbSet<UserModel> Umodel { get; set; }

        public DbSet<ProjectMembers> Pmembers { get; set; }
    }
} 