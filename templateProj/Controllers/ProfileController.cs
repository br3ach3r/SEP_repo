using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using templateProj.Models;

namespace templateProj.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Profile()
        {
            UserModel um = db.Umodel.Find(HttpContext.Session["Uname"]);
            return View(um); 
        }

        public ActionResult Profilee(string MemId)
        {
            UserModel um = db.Umodel.Find(MemId);
            Debug.WriteLine("dddddddd"+um.Username);
            return View("~/Views/Profile/Profile.cshtml",um );
        }

        [HttpPost]
        public void SaveUserInfo(string [] infoArr)
        {
            UserModel um = db.Umodel.Find(HttpContext.Session["Uname"]);
            um.FirstName = infoArr[0];
            um.LastName = infoArr[1];
            um.Email = infoArr[2];

            if (ModelState.IsValid)
            {
                db.Entry(um).State = EntityState.Modified;
                db.SaveChanges();
            }

           
        }

        
    }
}