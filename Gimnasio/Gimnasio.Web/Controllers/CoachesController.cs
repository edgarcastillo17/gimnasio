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
    public class CoachesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Coaches
        public ActionResult Index()
        {
            var coach = db.Coaches.Include(n => n.ApplicationUser).ToList();
            return View(coach);
        }

        // GET: Coaches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // GET: Coaches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coaches/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CoachViewModel cvm, HttpPostedFileBase cimage)
        {
            if (ModelState.IsValid)
            {
                Utilities.CreateUserASP(cvm.Email, cvm.Password, "Coach");
                var coachdb = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var usercoach = coachdb.FindByName(cvm.Email);

                if (cimage != null)
                {
                    var perfil = System.IO.Path.GetFileName(cimage.FileName);
                    var direccion = "~/Images/Coaches/" + cvm.Email + "_" + perfil;
                    cimage.SaveAs(Server.MapPath(direccion));
                    cvm.Image = cvm.Email + "_" + perfil;
                }

                var coach = new Coach
                {
                    FirstName = cvm.FirstName,
                    LastName = cvm.LastName,
                    Age = cvm.Age,
                    Specialty = cvm.Specialty,
                    Image = cvm.Image,
                    UserId = usercoach.Id
                };

                db.Coaches.Add(coach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cvm);
        }

        // GET: Coaches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coach);
        }

        // GET: Coaches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coaches.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coach coach = db.Coaches.Find(id);
            db.Coaches.Remove(coach);
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
