using Project.DBcontext;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private SugasContext sugasContext = new SugasContext();

        // Xem quản lý tất cả người dùng
        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = sugasContext.Users.Include(n => n.Roles);
            return View(users.ToList());
        }

        //Xem chi tiết người dùng theo UserID
        // GET: Admin/Users/Details
        public ActionResult Details(int? id)
        {
            // Nếu không có người dùng có mã được truyền vào thì trả về trang báo lỗi
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Tìm kiếm user theo ID
            User user = sugasContext.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            // trả về View UserDetails
            return View(user);
        }

        // Edit User by ID
        // GET: Admin/Users/Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = sugasContext.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(sugasContext.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        //Return user and save Data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Name,Email,Phone,Password,RoleID")] User user)
        {
            if (ModelState.IsValid)
            {
                sugasContext.Entry(user).State = EntityState.Modified;
                sugasContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(sugasContext.Roles, "RoleID", "RoleName", user.RoleID);
            return View(user);
        }

        // Delete User by ID
        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = sugasContext.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //Return user and save Data
        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = sugasContext.Users.Find(id);
            sugasContext.Users.Remove(user);
            sugasContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sugasContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}