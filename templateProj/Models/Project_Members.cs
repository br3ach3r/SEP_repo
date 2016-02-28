using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace templateProj.Models
{
    public class Project_Members
    {
        public Project proj { get; set; }
        public List<UserModel> UmodelList { get; set; }

        public List<UserModel> CandidList { get; set; }
        public UserModel Umodel { get; set; } 
    }
}