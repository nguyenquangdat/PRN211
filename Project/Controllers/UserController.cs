using Project.DBcontext;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class UserController : Controller
    {
        SugasContext sugasContext = new SugasContext();
        // GET: Register User View
        public ActionResult Register()
        {
            return View();
        }

        //Register with POST method
        [HttpPost]

        public ActionResult Register(User user)
        {
            try
            {
                // Thêm người dùng  mới
                sugasContext.Users.Add(user);
                // Lưu lại vào cơ sở dữ liệu
               sugasContext.SaveChanges();
                // Nếu dữ liệu đúng thì trả về trang đăng nhập
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Login");
                }
                return View("Register");

            }
            catch
            {
                return View();
            }
        }

        //Get Login View
        public ActionResult Login()
        {
            return View();

        }
        public ActionResult Orders()
        {
            User u = (User)Session["use"];
            if (u != null)
            {
                
                var list = (from p in sugasContext.Products.ToList()
                           join o in sugasContext.Orders.ToList()
                           on p.ProductID equals o.pid
                           where o.uid == u.UserID
                           select new Order
                           {
                                
                               productname = p.ProductName,
                               price = p.ProductPrice,
                               quantity = o.quantity,
                               total = o.total,
                               status = o.status,
                               time = o.time
                           }).ToList();
                
                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            

        }

        [HttpPost]

       public ActionResult Login(FormCollection formCollection)
        {
            string userMail = formCollection["userMail"].ToString();
            string userPassword = formCollection["userPassword"].ToString();
            var isLogin = sugasContext.Users.SingleOrDefault(x => x.UserEmail.Equals(userMail) && x.UserPassword.Equals(userPassword));

            if(isLogin != null)
            {
                if(userMail == "Admin@gmail.com")
                {
                    Session["use"] = isLogin;
                    return RedirectToAction("Index", "Admin/Product");
                }
                else
                {
                    Session["use"] = isLogin;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Fail = "Login Fail";
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }
    }
}