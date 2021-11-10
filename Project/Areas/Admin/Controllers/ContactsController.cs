using Project.DBcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class ContactsController : Controller
    {
        // GET: Admin/Contacts
        public ActionResult Index()
        {
            SugasContext sc = new SugasContext();
            var list = sc.Contacts.OrderByDescending(x=>x.CreateDate).ToList();
            return View(list);
        }

        public ActionResult Status(int status , int id)
        {
            SugasContext sc = new SugasContext();
            var contact = sc.Contacts.Where(x => x.ID == id).FirstOrDefault();
            if(status == 0)
            {
                contact.Status = 1;
            }
            else
            {
                contact.Status = 0;
            }
            sc.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            SugasContext sc = new SugasContext();
            var contact = sc.Contacts.Where(x => x.ID == id).FirstOrDefault();
            return View(contact);
        }
    }
}