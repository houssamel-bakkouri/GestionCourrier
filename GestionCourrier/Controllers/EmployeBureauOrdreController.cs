using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GestionCourrier.BusinessLayer;
using GestionCourrier.DataLayer;
using GestionCourrier.Models;

namespace GestionCourrier.Controllers
{
    public class EmployeBureauOrdreController : Controller
    {
        private MasterDbContext db = new MasterDbContext();

        private RolesManager rolesManager = new RolesManager();
        private EmpBOManager EmpBOManager = new EmpBOManager();
        private CompteManager CompteManager = new CompteManager();


        public ActionResult Auth()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Auth(FormCollection collection, Compte model)
        {
            try
            {
                // TODO: Add insert logic here

                if (CompteManager.Authenticate(model))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                    return RedirectToAction("Index");
                }
                ViewBag.msgError = "Echec de l'authentification";
                return View();
            }
            catch
            {
                return View();
            }
        }
        // GET: EmployeBureauOrdres
        [Authorize]
        public ActionResult Index()
        {
            return View(EmpBOManager.GetEmployes());
        }

        // GET: EmployeBureauOrdres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //EmployeBureauOrdre employeBureauOrdre = db.EmployeBureaus.Find(id);
            EmployeBureauOrdre employeBureauOrdre = EmpBOManager.SearchEmploye((int)id);
            if (employeBureauOrdre == null)
            {
                return HttpNotFound();
            }
            return View(employeBureauOrdre);
        }

        // GET: EmployeBureauOrdres/Create
        public ActionResult Create()
        {
            ViewBag.roles = new SelectList(rolesManager.GetRoles(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, EmployeBureauOrdre employeBureauOrdre)
        {
            try
            {
                //employeBureauOrdre.Compte.Role = rolesManager.GetRoles().FirstOrDefault(item => item.Id == employeBureauOrdre.Compte.Role.Id);
                // add roles later
                EmpBOManager.AddEmployeBO(employeBureauOrdre);
                return RedirectToAction("Auth");
            }
            catch 
            {
                return View();
            }
        }

        // GET: EmployeBureauOrdres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeBureauOrdre employeBureauOrdre = db.EmployeBureaus.Find(id);
            if (employeBureauOrdre == null)
            {
                return HttpNotFound();
            }
            return View(employeBureauOrdre);
        }

        // POST: EmployeBureauOrdres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom")] EmployeBureauOrdre employeBureauOrdre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeBureauOrdre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeBureauOrdre);
        }

        // GET: EmployeBureauOrdres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeBureauOrdre employeBureauOrdre = EmpBOManager.SearchEmploye((int)id);
            if (employeBureauOrdre == null)
            {
                return HttpNotFound();
            }
            return View(employeBureauOrdre);
        }

        // POST: EmployeBureauOrdres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /*EmployeBureauOrdre employeBureauOrdre = db.EmployeBureaus.Find(id);
            db.EmployeBureaus.Remove(employeBureauOrdre);
            db.SaveChanges();*/
            EmpBOManager.DeleteEmploye(id);
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
