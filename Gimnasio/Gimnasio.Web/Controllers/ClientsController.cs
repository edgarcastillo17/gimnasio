using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gimnasio.Web.Class;
using Gimnasio.Web.Models;
using Gimnasio.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gimnasio.Web.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult AllClients()
        {
            var clients = db.Clients.Include(c => c.Coach).Include(c => c.Nutritionist).Include(c => c.ApplicationUser);
            return View(clients);
        }

        [Authorize(Roles = "Coach")]
        public ActionResult ClientByCoach()
        {
            var user = User.Identity.GetUserId();
            var coach = db.Coaches.Where(c => c.UserId == user).FirstOrDefault();
            var clients = db.Clients.Include(e => e.Coach).Where(c => c.CoachId == coach.Id).ToList();

            return View(clients);
        }

        [Authorize(Roles = "Nutritionist")]
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
            var clients = db.Clients.Include(c => c.Coach).Include(c => c.Nutritionist).Include(c => c.ApplicationUser);
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

        [Authorize(Roles = "Admin")]
        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.Coaches = (from c in db.Coaches
                                select c).ToList();
            ViewBag.Nutritionists = (from c in db.Nutritionists
                                select c).ToList();
            ViewBag.CoachId = new SelectList(db.Coaches, "Id", "FirstName");
            ViewBag.NutritionistId = new SelectList(db.Nutritionists, "Id", "FirstName");
            return View();
        }

        // POST: Clients/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                Utilities.CreateUserASP(cvm.Email, cvm.Password, "Client");
                var clientdb = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var userclient = clientdb.FindByName(cvm.Email);

                var client = new Client
                {
                    FirstName = cvm.FirstName,
                    LastName = cvm.LastName,
                    Age = cvm.Age,
                    Type = cvm.Type,
                    Admission = cvm.Admission,
                    UserId = userclient.Id
                };

                ViewBag.CoachId = new SelectList(db.Coaches, "Id", "FirstName", cvm.CoachId);
                ViewBag.NutritionistId = new SelectList(db.Nutritionists, "Id", "FirstName", cvm.NutritionistId);

                client.CoachId = cvm.CoachId;
                client.NutritionistId = cvm.NutritionistId;

                db.Clients.Add(client);
                db.SaveChanges();
                
                return RedirectToAction("AllClients");
            }

            return View(cvm);
        }

        [Authorize(Roles = "Admin")]
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

            ViewBag.Coaches = (from c in db.Coaches
                               select c).ToList();
            ViewBag.Nutritionists = (from c in db.Nutritionists
                                     select c).ToList();
            ViewBag.CoachId = new SelectList(db.Coaches, "Id", "FirstName");
            ViewBag.NutritionistId = new SelectList(db.Nutritionists, "Id", "FirstName");
            return View(client);
        }

        // POST: Clients/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllClients");
            }
            ViewBag.CoachId = new SelectList(db.Coaches, "Id", "FirstName", client.CoachId);
            ViewBag.NutritionistId = new SelectList(db.Nutritionists, "Id", "FirstName", client.NutritionistId);
            return View(client);
        }

        [Authorize(Roles = "Admin")]
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
            return RedirectToAction("AllClients");
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
