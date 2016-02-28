using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Services.Description;
using templateProj.Models;

namespace templateProj.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {

        DataContext db = new DataContext();
     
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> sendEmail(string uname)
        {
            UserModel um = db.Umodel.Single(u => u.Username == uname);
            SendMailModel sm = new SendMailModel();

            if (um != null)
            {
                string pw = passGen();
                var message = await EMailTemplate("RecoverPW");
                message = message.Replace("@ViewBag.Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(um.Username));
                message = message.Replace("@ViewBag.Pass", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(pw));

                await MessageServices.SendEmail(um.Email, "Project base", message);
                @ViewBag.Error = "Nerror";
                @ViewBag.Uname = uname;

                sm.RecoverCode = pw;
            }
            else
            {
                @ViewBag.Error = "error";
            }

            return View("~/Views/Login/ForgotPassword.cshtml", sm);
        }
      

        public string passGen()
        {
            string L = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string N = "0123456789";
            string pw = "";
            Random r = new Random();

            for (int i = 1; i <= 4 ; i++)
            {
                pw += L[r.Next(0, 25)];
                pw += N[r.Next(0, 9)];
            }
            return pw;
        }


        // GET: Email
       public static async Task<string> EMailTemplate(string template)
        {
            var templateFilePath = HostingEnvironment.MapPath("~/Views/EmailTemplates/") + template + ".cshtml";
            StreamReader objstreamreaderfile = new StreamReader(templateFilePath);
            var body = await objstreamreaderfile.ReadToEndAsync();
            objstreamreaderfile.Close();
            return body;
       }
    }
}