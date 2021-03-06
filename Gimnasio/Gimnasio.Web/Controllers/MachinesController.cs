﻿using System;
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
    public class MachinesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult AllMachines()
        {
            return View(db.Machines.ToList());
        }
        // GET: Machines
        public ActionResult Index()
        {
            return View(db.Machines.ToList());
        }

        // GET: Machines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        [Authorize(Roles = "Admin")]

        // GET: Machines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Machines/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Machine machine, HttpPostedFileBase eimage)
        {
            try
            {
                var perfil = System.IO.Path.GetFileName(eimage.FileName);
                var direccion = "~/Images/Machines/" + machine.Id + "_" + perfil;
                eimage.SaveAs(Server.MapPath(direccion));
                machine.Image = machine.Id + "_" + perfil;

                db.Machines.Add(machine);
                db.SaveChanges();
                return RedirectToAction("AllMachines");
            }
            catch (Exception)
            {
                return View(machine);
            }
        }

        [Authorize(Roles = "Admin")]

        // GET: Machines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Machine machine, HttpPostedFileBase eimage)
        {
            try
            {
                var perfil = System.IO.Path.GetFileName(eimage.FileName);
                var direccion = "~/Images/Machines/" + machine.Id + "_" + perfil;
                eimage.SaveAs(Server.MapPath(direccion));
                machine.Image = machine.Id + "_" + perfil;

                db.Entry(machine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllMachines");
            }
            catch (Exception)
            {
                return View(machine);
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: Machines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.Find(id);
            if (machine == null)
            {
                return HttpNotFound();
            }
            return View(machine);
        }

        // POST: Machines/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Machine machine = db.Machines.Find(id);
            db.Machines.Remove(machine);
            db.SaveChanges();
            return RedirectToAction("AllMachines");
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
