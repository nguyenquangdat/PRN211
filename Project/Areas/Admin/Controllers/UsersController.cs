using OfficeOpenXml;
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

        public void ExportToExcel()
        {
            var users = sugasContext.Users.ToList();

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excelPackage = new ExcelPackage();
            //add new ExcelSheet
            ExcelWorksheet ws = excelPackage.Workbook.Worksheets.Add("Report");
            // process our database dât in excelSheet
            ws.Cells["A1"].Value = "Report";
            ws.Cells["B1"].Value = "Report1";

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = string.Format("{0:dd MMM yyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A5"].Value = "UserID";
            ws.Cells["B5"].Value = "UserName";
            ws.Cells["C5"].Value = "UserEmail";
            ws.Cells["D5"].Value = "UserPhone";
            ws.Cells["E5"].Value = "RoleID";
            ws.Cells["F5"].Value = "UserAddress";


            int rows = 6;
            foreach (var item in users)
            {
                ws.Cells[String.Format("A{0}", rows)].Value = item.UserID;
                ws.Cells[String.Format("B{0}", rows)].Value = item.UserName;
                ws.Cells[String.Format("C{0}", rows)].Value = item.UserEmail;
                ws.Cells[String.Format("D{0}", rows)].Value = item.UserPhone;
                ws.Cells[String.Format("E{0}", rows)].Value = item.RoleID;
                ws.Cells[String.Format("F{0}", rows)].Value = item.UserAddress;

                rows++;

            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposittion", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(excelPackage.GetAsByteArray());
            Response.End();
        }
    }
}