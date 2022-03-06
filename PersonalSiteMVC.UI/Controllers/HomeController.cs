using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalSiteMVC.UI.Models;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace PersonalSiteMVC.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {

            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            string message = $"You have recieved an email from {cvm.Name}.<br />" +
                $"Subject: {cvm.Subject}<br />" +
                $"Message: {cvm.Message}<br />" +
                $"Please respond to {cvm.Email} with your reply.";

            MailMessage mm = new MailMessage(
                
                //FROM 
                ConfigurationManager.AppSettings["EmailUser"].ToString(),

                //TO
                ConfigurationManager.AppSettings["EmailTo"].ToString(),

                //Subject
                cvm.Subject,

                //BODY
                message
                
                );

            mm.IsBodyHtml = true;

            mm.Priority = MailPriority.High;

            mm.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            client.Credentials = new NetworkCredential(
                
                //User
                ConfigurationManager.AppSettings["EmailUser"].ToString(),

                //Password
                ConfigurationManager.AppSettings["EmailPass"].ToString()
                );

            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry, but your request could not be completed at this time. Please try again later.<br />Error Message: {ex.StackTrace}";

                return View(cvm);

            }

            return View("EmailConfirmation", cvm);
        }
    }
}