using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionCourrier.DataLayer;
using GestionCourrier.Models;

namespace GestionCourrier.Controllers
{
    public class DossiersController : Controller
    {
        private MasterDbContext db = new MasterDbContext();

        // GET: Dossiers
        public ActionResult Index()
        {
            return View(db.Dossiers.Include("Courriers").Include("Service").Include("Responsable").ToList());
        }

        public ActionResult AddCourrier(int id)
        {
            Session["DossierCourrier"] = id;
            return RedirectToAction("Create", "Courriers");
        }
        public ActionResult Send(int id)
        {
            Dossier dossier = db.Dossiers.Find(id);
            dossier.Sent = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Dossiers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossier dossier = db.Dossiers.Find(id);
            if (dossier == null)
            {
                return HttpNotFound();
            }
            return View(dossier);
        }

        // GET: Dossiers/Create
        public ActionResult Create()
        {
            ViewBag.Responsable = new SelectList(db.AgentServices, "Id", "Nom");
            ViewBag.Service = new SelectList(db.Services, "Id", "Name");
            return View();
        }

        // POST: Dossiers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection, Dossier dossier)
        {
            try
            {
                dossier.Responsable = db.AgentServices.FirstOrDefault(item => item.Id == dossier.Responsable.Id);
                dossier.Service = db.Services.FirstOrDefault(item => item.Id == dossier.Service.Id);
                db.Dossiers.Add(dossier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Dossiers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossier dossier = db.Dossiers.Find(id);
            if (dossier == null)
            {
                return HttpNotFound();
            }
            return View(dossier);
        }

        // POST: Dossiers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,dossiername,dossierObjet")] Dossier dossier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dossier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dossier);
        }

        // GET: Dossiers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossier dossier = db.Dossiers.Find(id);
            if (dossier == null)
            {
                return HttpNotFound();
            }
            return View(dossier);
        }

        // POST: Dossiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dossier dossier = db.Dossiers.Find(id);
            db.Dossiers.Remove(dossier);
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
