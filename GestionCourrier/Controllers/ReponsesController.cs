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
    public class ReponsesController : Controller
    {
        private MasterDbContext db = new MasterDbContext();

        // GET: Reponses
        public ActionResult Index()
        {
            return View(db.Reponses.Include("Suivi").ToList());
        }

        // GET: Reponses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reponse reponse = db.Reponses.Find(id);
            if (reponse == null)
            {
                return HttpNotFound();
            }
            return View(reponse);
        }

        // GET: Reponses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reponse reponse = db.Reponses.Find(id);
            if (reponse == null)
            {
                return HttpNotFound();
            }
            return View(reponse);
        }

        public ActionResult Courrier(int id)
        {
            try
            {
                Courrier courrier = db.Courriers.Include("Reponse").FirstOrDefault(item => item.Reponse.Id == id);
                return View(courrier);
            }
            catch (Exception)
            {
                return View("Index");
            }
        }

        // POST: Reponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reponse reponse = db.Reponses.Find(id);
            db.Reponses.Remove(reponse);
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
