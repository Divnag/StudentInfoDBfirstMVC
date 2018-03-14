using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentInfoApp.Models;

namespace StudentInfoApp.Controllers
{
    public class ChildDetailsController : Controller
    {
        private ChildEntities db = new ChildEntities();

        // GET: ChildDetails
        public ActionResult Index()
        {
            var childDetails = db.ChildDetails.Include(c => c.Bus).Include(c => c.School);
            return View(childDetails.ToList());
        }

        // GET: ChildDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildDetail childDetail = db.ChildDetails.Find(id);
            if (childDetail == null)
            {
                return HttpNotFound();
            }
            return View(childDetail);
        }

        // GET: ChildDetails/Create
        public ActionResult Create()
        {
            ViewBag.BusID = new SelectList(db.Buses, "BusID", "BusNumber");
            ViewBag.SchoolID = new SelectList(db.Schools, "SchoolID", "SchoolName");
            return View();
        }

        // POST: ChildDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChildID,FirstName,LastName,Grade,SchoolID,BusID")] ChildDetail childDetail)
        {
            if (ModelState.IsValid)
            {
                db.ChildDetails.Add(childDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusID = new SelectList(db.Buses, "BusID", "BusNumber", childDetail.BusID);
            ViewBag.SchoolID = new SelectList(db.Schools, "SchoolID", "SchoolName", childDetail.SchoolID);
            return View(childDetail);
        }

        // GET: ChildDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildDetail childDetail = db.ChildDetails.Find(id);
            if (childDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusID = new SelectList(db.Buses, "BusID", "BusNumber", childDetail.BusID);
            ViewBag.SchoolID = new SelectList(db.Schools, "SchoolID", "SchoolName", childDetail.SchoolID);
            return View(childDetail);
        }

        // POST: ChildDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChildID,FirstName,LastName,Grade,SchoolID,BusID")] ChildDetail childDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(childDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusID = new SelectList(db.Buses, "BusID", "BusNumber", childDetail.BusID);
            ViewBag.SchoolID = new SelectList(db.Schools, "SchoolID", "SchoolName", childDetail.SchoolID);
            return View(childDetail);
        }

        // GET: ChildDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildDetail childDetail = db.ChildDetails.Find(id);
            if (childDetail == null)
            {
                return HttpNotFound();
            }
            return View(childDetail);
        }

        // POST: ChildDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChildDetail childDetail = db.ChildDetails.Find(id);
            db.ChildDetails.Remove(childDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
