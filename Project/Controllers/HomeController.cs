using Project.DBcontext;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
       public ActionResult AddContact(Contact contact)
        {
            SugasContext sc = new SugasContext();
            Contact c = new Contact();
            c.firtName = contact.firtName;
            c.LastName = contact.LastName;
            c.Message = contact.Message;
            c.Subject = contact.Subject;
            c.email = contact.email;
            c.Status = 0;
            c.CreateDate = DateTime.Now;
            sc.Contacts.Add(c);
            sc.SaveChanges();
            return Json(data:"Send Message Sucessfuly",JsonRequestBehavior.AllowGet);
        }
    }
}