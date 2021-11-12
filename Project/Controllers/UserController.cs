using Project.DBcontext;
using Project.Models;
using Project.Util;
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
                    var users = sugasContext.Users.ToList();
                    foreach(User user1 in users)
                    {
                        if(user1.UserEmail == user.UserEmail)
                        {
                            TempData["Error"] = "account already exists ";
                            return View();
                        }
                    }
                    user.RoleID = 2;
                    user.UserPassword = Hasing.HashPassword(user.UserPassword);
                    // Thêm người dùng  mới
                    sugasContext.Users.Add(user);
                    // Lưu lại vào cơ sở dữ liệu
                    sugasContext.SaveChanges();
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
            var isLogin = sugasContext.Users.SingleOrDefault(x => x.UserEmail.Equals(userMail));
            if(isLogin != null)
            {
                if (Hasing.validatePassword(userPassword, isLogin.UserPassword))
                {

                    if (userMail == "Admin@gmail.com")
                    {
                       // FormsAuthentication.SetAuthCookie(userMail, false);
                        Session["use"] = isLogin;
                        return RedirectToAction("Index", "Admin/Product");
                    }
                    else
                    {
                       // FormsAuthentication.SetAuthCookie(userMail, false);
                        Session["use"] = isLogin;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["error"] = "User or Password incorrect";
                    return View("Login");
                }
            }
            else
            {
                TempData["error"] = "User or Password incorrect";
                return View("Login");
            }
          }
        
        public ActionResult Logout()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Information(int id)
        {
            var user = sugasContext.Users.Where(x => x.UserID == id).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Information(User user)
        {
            var current_user = sugasContext.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
            if (user.UserPassword == null)
            {
                current_user.UserName = user.UserName;
                current_user.UserPhone = user.UserPhone;
                current_user.UserAddress = user.UserAddress;
                sugasContext.SaveChanges();
                return Json(data: "Update Sucessfully",JsonRequestBehavior.AllowGet);
            }
            else if (user.UserPassword != null)
            {
                if(user.UserPassword != user.ComformPassword)
                {
                    return Json(data: "Password are not the same ", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    current_user.UserName = user.UserName;
                    current_user.UserPhone = user.UserPhone;
                    current_user.UserAddress = user.UserAddress;
                    current_user.UserPassword = Hasing.HashPassword(user.UserPassword);
                    sugasContext.SaveChanges();
                    return Json(data: "Update Sucessfully", JsonRequestBehavior.AllowGet);
                }
            }
            return Json(data: "Update Sucessfully", JsonRequestBehavior.AllowGet);

        }

    }
}