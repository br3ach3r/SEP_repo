using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using templateProj.Filters;
using templateProj.Models;

namespace templateProj.Controllers
{
    [SessionTimeOutFilter]
    [AdminFilter]
   // [ManagerFilter]
    [Authorize]
    public class UStoriesController : Controller
    {
        
        // GET: UserStories
       
        private string [] stories;

        DataContext db = new DataContext();

       
        public ActionResult CreateUs()
        {
            string userID = HttpContext.Session["Uname"].ToString();
            Project p = db.Proj.Single(id => id.Manager == userID);
            //Debug.WriteLine("ddddddd"+userID);
            List<UserStories> list = db.Ustory.Where(id => id.Pid 
                == p.Pid).ToList();

            Manager_UserStories mu = new Manager_UserStories();
            mu.Ustory = list;
            mu.Umodel = db.Umodel.Find(userID);
            
            return View(mu);
        }

        // Generating user story string for the insertion
        [HttpPost]
        public void SaveUs(string[] USarr) 
        {
            stories = USarr;
            //Debug.WriteLine("ssssssssss" + USarr[0]);
            UsToDb();
        }


        private void UsToDb() // Updating usertempdata table
        {
            string id = HttpContext.Session["Uname"].ToString();
           
            UserStories us = new UserStories();
            Project p = db.Proj.Single(mid => mid.Manager == id);
            try
            {
                UserStories us2 = db.Ustory.First();
                us2 = db.Ustory.OrderByDescending(usid => usid.USid).First(); // getting the last row of UserStory Table
                us.USid = us2.USid + 1; // auto incrementing the User story ID
              //  Debug.WriteLine("rrrrrrrrrrrrrrr");
            }
            catch (System.InvalidOperationException e)
            {
               //  Debug.WriteLine("eeeeeeeeeeeeeee");
                us.USid = 1;
            }

            int x = 1;

            foreach (string s in stories)
            {
                 //Debug.WriteLine("ddddddddddddddddddddddddd"+ s);
                if (s != "")
                {
                    string[] res = s.Split(';');

                    us.Pid = p.Pid;
                    us.UserStory = res[x++];
                    us.ColorCode = res[x++];
                    us.Priority = res[x++];
                    us.Description = res[x++];
                    us.Duration = res[x++];

                    x = 1;

                    if (Convert.ToInt32(res[0]) < 0)
                    {
                       // Debug.WriteLine("iiiiiiiiiiiiiiiii" + s);

                        if (ModelState.IsValid)
                        {
                            db.Ustory.Add(us);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        int storyId = Convert.ToInt32(res[res.Length - 1]);
                        UserStories story = db.Ustory.Where(usid => usid.Pid == p.Pid && usid.USid == storyId).First();

                        story.UserStory = res[x++];
                        story.ColorCode = res[x++];
                        story.Priority = res[x++];
                        story.Description = res[x++];
                        story.Duration = res[x++];

//Debug.WriteLine("ffffffffffffffff" + s);
                        if (ModelState.IsValid)
                        {
                            db.Entry(story).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
                x = 1;
            }

            // Debug.WriteLine(stories);

           
        }
    }
}