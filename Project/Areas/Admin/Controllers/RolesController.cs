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
    [Authorize(Roles = "ADMIN")]
    public class RolesController : Controller
    {
        private SugasContext sugasContext = new SugasContext();

        // GET: Admin/Roles
        // Reuturn List of Role in View Index
        public ActionResult Index()
        {

            return View(sugasContext.Roles.ToList());
        }

        // GET: Admin/Roles/Details
        // Reuturn Details of Role by ID in Detalis Index
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = sugasContext.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // GET: Admin/Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleID,RoleName")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                sugasContext.Roles.Add(roles);
                sugasContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roles);
        }

        // GET: Admin/Roles/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = sugasContext.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // POST: Admin/Roles/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleID,RoleName")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                sugasContext.Entry(roles).State = EntityState.Modified;
                sugasContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roles);
        }

        // GET: Admin/Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = sugasContext.Roles.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roles roles = sugasContext.Roles.Find(id);
            sugasContext.Roles.Remove(roles);
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
            var roles = sugasContext.Roles.ToList();

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

            ws.Cells["A5"].Value = "RoleID";
            ws.Cells["B5"].Value = "RoleName";




            int rows = 6;
            foreach (var item in roles)
            {
                ws.Cells[String.Format("A{0}", rows)].Value = item.RoleID;
                ws.Cells[String.Format("B{0}", rows)].Value = item.RoleName;

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