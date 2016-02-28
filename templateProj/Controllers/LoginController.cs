using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using templateProj.Models;
using templateProj.Property;

namespace templateProj.Controllers
{
    //[AllowAnonymous]
    public class LoginController : Controller
    {
        
        DataContext db = new DataContext();
        Paths path = new Paths();
        Encrypt hash = new Encrypt();

        // Login action
        public ActionResult Index()
        {

            return View("login");
        }

        // Recover Password page
        public ActionResult RecoverPass()
        {

            return View("ForgotPassword", null);
        }

        // Login method
        [HttpPost]
        public ActionResult login(string uname, string pass)
        {

            if (ModelState.IsValid)
            {
                SessionController s = new SessionController();
                //  try
                // {
                UserModel um = db.Umodel.Find(uname);

                string hashpw = hash.HashPassword(pass, um.salt);

                // Debug.WriteLine("ddddddddddddddddddd  " + "ss " + um.salt 
                // + " pass " + hashpw);

                UserModel User = db.Umodel.Single(usr => usr.Username == uname
                    && usr.password == hashpw);

                UserRole role = s.GetUserValidity(User);

                string Urole = "";

                if (role == UserRole.Admin)
                {
                    Urole = "Admin";
                }
                else if (role == UserRole.PManager)
                {
                    Urole = "PManager";
                }
                else if (role == UserRole.Developer)
                {
                    Urole = "Developer";
                }
                else
                {
                    ViewBag.errorMsg = "error";
                    return View();
                }

                FormsAuthentication.SetAuthCookie(User.Username, true);

                Session["Role"] = Urole;
                Session["Uname"] = User.Username;
                Session["ProPic"] = User.ProfilePic;

                //UserModel us = db.Umodel.Find(uname);
                //string sal = hash.CreateSalt();
                //string p = hash.HashPassword(pass, sal);
                //us.password = p;
                //us.salt = sal;
                //if (ModelState.IsValid)
                //{
                //    db.Entry(us).State = EntityState.Modified;
                //    db.SaveChanges();
                //}
                //Debug.WriteLine("ddddddddddddddddddd  " + "ss " + sal + "
                //pass " + p);
                return RedirectToAction("Home", "Home");
                //  }
                //  catch (System.InvalidOperationException e)
                //  {
                //      ViewBag.errorMsg = "error";
                //     return View();
                //  }


            }
            else
            {
                ViewBag.errorMsg = "error";
                return View();
            }
        }

        // Logout method
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        // New password entry
        public ActionResult NewPw(string pw, string username)
        {

            UserModel user = db.Umodel.Find(username);

            string salt = hash.CreateSalt();
            string hashpw = hash.HashPassword(pw,salt);

            user.password = hashpw;
            user.salt = salt;
            //Debug.WriteLine("USERNAME : " + username);
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return View(path.dict["loginURL"]);
            }
            return View(path.dict["loginURL"]);
        }
    }
}