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
    public class TagsController : Controller
    {
        private SugasContext sugasContext = new SugasContext();
        // GET: Admin/Tag
        public ActionResult Index()
        {
            return View(sugasContext.Tags.ToList());
        }

        // GET: Admin/Hedieuhanhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tags tags = sugasContext.Tags.Find(id);
            if (tags == null)
            {
                return HttpNotFound();
            }
            return View(tags);
        }

        // GET: Admin/Hedieuhanhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Hedieuhanhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TagID,TagName")] Tags tags)
        {
            if (ModelState.IsValid)
            {
                sugasContext.Tags.Add(tags);
                sugasContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tags);
        }

        // GET: Admin/Hedieuhanhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tags tags = sugasContext.Tags.Find(id);
            if (tags == null)
            {
                return HttpNotFound();
            }
            return View(tags);
        }


        // POST: Admin/Hedieuhanhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TagID,TagName")] Tags tags)
        {
            if (ModelState.IsValid)
            {
                sugasContext.Entry(tags).State = EntityState.Modified;
                sugasContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tags);
        }

        // GET: Admin/Hedieuhanhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tags tags = sugasContext.Tags.Find(id);
            if (tags == null)
            {
                return HttpNotFound();
            }
            return View(tags);
        }

        // POST: Admin/Hedieuhanhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tags tags = sugasContext.Tags.Find(id);
            sugasContext.Tags.Remove(tags);
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