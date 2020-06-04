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
    public class NutritionistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult AllNutritionists()
        {
            var nutritionists = db.Nutritionists.Include(c => c.ApplicationUser);
            return View(nutritionists);
        }

        // GET: Nutritionists
        public ActionResult Index()
        {
            var nutri = db.Nutritionists.Include(n => n.ApplicationUser).ToList();
            return View(nutri);
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

        [Authorize(Roles = "Admin")]
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
        public ActionResult Create(NutritionistViewModel nvm, HttpPostedFileBase nimage)
        {
            if (ModelState.IsValid)
            {
                Utilities.CreateUserASP(nvm.Email, nvm.Password, "Nutritionist");
                var nutridb = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var usernutri = nutridb.FindByName(nvm.Email);

                if (nimage != null)
                {
                    var perfil = System.IO.Path.GetFileName(nimage.FileName);
                    var direccion = "~/Images/Nutritionists/" + nvm.Email + "_" + perfil;
                    nimage.SaveAs(Server.MapPath(direccion));
                    nvm.Image = nvm.Email + "_" + perfil;
                }

                var nutri = new Nutritionist
                {
                    FirstName = nvm.FirstName,
                    LastName = nvm.LastName,
                    Age = nvm.Age,
                    Image = nvm.Image,
                    UserId = usernutri.Id
                };

                db.Nutritionists.Add(nutri);
                db.SaveChanges();
                return RedirectToAction("AllNutritionists");
            }

            return View(nvm);
        }

        [Authorize(Roles = "Admin")]
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
        public ActionResult Edit(Nutritionist nutritionist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nutritionist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllNutritionists");
            }
            return View(nutritionist);
        }

        [Authorize(Roles = "Admin")]
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
            return RedirectToAction("AllNutritionists");
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
