using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gimnasio.Web.Models;

namespace Gimnasio.Web.Controllers
{
    public class NutritionistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Nutritionists
        public ActionResult Index()
        {
            return View(db.Nutritionists.ToList());
        }

        // GET: Nutritionists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutritionist nutritionist = db.Nutritionists.Find(id);
            if (nutritionist == null)
            {
                return HttpNotFound();
            }
            return View(nutritionist);
        }

        // GET: Nutritionists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nutritionists/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Age,Image")] Nutritionist nutritionist)
        {
            if (ModelState.IsValid)
            {
                db.Nutritionists.Add(nutritionist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nutritionist);
        }

        // GET: Nutritionists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutritionist nutritionist = db.Nutritionists.Find(id);
            if (nutritionist == null)
            {
                return HttpNotFound();
            }
            return View(nutritionist);
        }

        // POST: Nutritionists/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Age,Image")] Nutritionist nutritionist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nutritionist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nutritionist);
        }

        // GET: Nutritionists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutritionist nutritionist = db.Nutritionists.Find(id);
            if (nutritionist == null)
            {
                return HttpNotFound();
            }
            return View(nutritionist);
        }

        // POST: Nutritionists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nutritionist nutritionist = db.Nutritionists.Find(id);
            db.Nutritionists.Remove(nutritionist);
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
