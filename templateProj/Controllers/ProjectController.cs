using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using templateProj.Filters;
using templateProj.Models;

namespace templateProj.Controllers
{
    [AdminFilter]
    [SessionTimeOutFilter]
    [Authorize]
    public class ProjectController : Controller
    {
        DataContext db = new DataContext();
        
        // GET: Project
        public ActionResult ProjectDetails()
        {
            string Username = HttpContext.Session["Uname"].ToString();

            UserModel um = db.Umodel.Where(id => id.Username
                == Username).First();


            Project p = db.Proj.Where(Pid => Pid.Pid == um.CurrentProj).First();

            Project_Members pm = new Project_Members();

            List<ProjectMembers> Pmem = db.Pmembers.Where(id => id.Pid 
                == p.Pid).ToList();

            List<UserModel> users = new List<UserModel>();

            List<ProjectNotification> SuggestedUserList = db.ProjNot.Where(ID => ID.Pid == p.Pid).ToList();

          //  Debug.WriteLine("ddddddddddddddddddddd" + SuggestedUserList[0].MemSug);
            List<UserModel> list = db.Umodel.Where(ID => ID.Status == "Free" && ID.Skills.Contains(p.tech)).ToList();

            List<UserModel> Candidlist = new List<UserModel>();

                foreach (var UserList in list)
                {
                    bool check = true;
                    foreach (var SugList in SuggestedUserList )
                    {
                        
                        if ( UserList.Username == SugList.MemSug)
                        {
                           
                            check = false;
                            break;
                        }
                    }

                    if (check)
                    {
                      //  Debug.WriteLine("UserList.Username " + UserList.Username );
                        Candidlist.Add(UserList);
                    }
                }
           // Debug.WriteLine("ddddddddddddddddddddd"+Candidlist[0].FirstName);
            

           
            foreach (var item in Pmem)
            {
                users.Add(db.Umodel.Single(id => id.Username == item.Uname));

            }

            pm.CandidList = Candidlist;
            pm.proj = p;
            pm.UmodelList = users;
            pm.Umodel = um;
            return View(pm);
        }

        [HttpPost]
        public void SuggestMems(string UserID)
        {
            int Id = Convert.ToInt32(UserID);

            string UserOnline = HttpContext.Session["Uname"].ToString();

            UserModel user = db.Umodel.Single(id1 => id1.id == Id );
            UserModel member =  db.Umodel.Single(id1 => id1.Username == UserOnline);

            ProjectNotification pn = new ProjectNotification();

            try
            {
                ProjectNotification pn2 = db.ProjNot.First();
                pn2 = db.ProjNot.OrderByDescending(nid => nid.Nid).First(); // getting the last row of Project Notification Table
                pn.Nid = pn2.Nid + 1; // auto incrementing the Notification ID
                //  Debug.WriteLine("rrrrrrrrrrrrrrr");
            }
            catch (System.InvalidOperationException e)
            {
                //  Debug.WriteLine("eeeeeeeeeeeeeee");
                pn.Nid = 1;
            }



            pn.Pid = member.CurrentProj;
            pn.Visibility = "PLeader";
            pn.MemSug = user.Username;

            if (ModelState.IsValid)
            {
                db.ProjNot.Add(pn);
                db.SaveChanges();
            }

        }
    }
}