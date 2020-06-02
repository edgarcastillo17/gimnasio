using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gimnasio.Web.Models;
using Microsoft.AspNet.Identity;

namespace Gimnasio.Web.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AllClients()
        {
            var clients = db.Clients.Include(c => c.Coach).Include(n => n.Nutritionist).ToList();
            return View(clients);
        }

        public ActionResult ClientByCoach()
        {
            var user = User.Identity.GetUserId();
            var coach = db.Coaches.Where(c => c.UserId == user).FirstOrDefault();
            var clients = db.Clients.Include(e => e.Coach).Where(c => c.CoachId == coach.Id).ToList();

            return View(clients);
        }

        public ActionResult ClientByNutritionist()
        {
            var user = User.Identity.GetUserId();
            var nutri = db.Nutritionists.Where(n => n.UserId == user).FirstOrDefault();
            var clients = db.Clients.Include(n => n.Nutritionist).Where(c => c.NutritionistId == nutri.Id).ToList();

            return View(clients);
        }

        // GET: Clients
        public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.Coach).Include(c => c.Nutritionist);
            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.CoachId = new SelectList(db.Coaches, "Id", "Specialty");
            ViewBag.NutritionistId = new SelectList(db.Nutritionists, "Id", "Image");
            return View();
        }

        // POST: Clients/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Age,Type,Admission,CoachId,NutritionistId")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoachId = new SelectList(db.Coaches, "Id", "Specialty", client.CoachId);
            ViewBag.NutritionistId = new SelectList(db.Nutritionists, "Id", "Image", client.NutritionistId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoachId = new SelectList(db.Coaches, "Id", "Specialty", client.CoachId);
            ViewBag.NutritionistId = new SelectList(db.Nutritionists, "Id", "Image", client.NutritionistId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Age,Type,Admission,CoachId,NutritionistId")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoachId = new SelectList(db.Coaches, "Id", "Specialty", client.CoachId);
            ViewBag.NutritionistId = new SelectList(db.Nutritionists, "Id", "Image", client.NutritionistId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
