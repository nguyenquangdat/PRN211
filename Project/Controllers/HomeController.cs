using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
<<<<<<< HEAD

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

            String subject = "Have Message's contact";
            String body = $"You can click link to contact manage \n https://localhost:44352/Admin/Contacts" ;
            WebMail.Send("nguyenquangdat199999@gmail.com", subject, body, null, null, null, true, null, null, null, null, null);
            return Json(data:"Send Message Sucessfuly",JsonRequestBehavior.AllowGet);
        }
=======
>>>>>>> 748b74f38e37538eab6b26b071cbcb655728040f
    }
}