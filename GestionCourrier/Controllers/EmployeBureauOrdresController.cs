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
    public class EmployeBureauOrdresController : Controller
    {
        private MasterDbContext db = new MasterDbContext();

        //private RolesManager rolesManager = new RolesManager();
        private EmpBOManager EmpBOManager = new EmpBOManager();
        //private CompteManager CompteManager = new CompteManager();

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Auth");
        }
        [AllowAnonymous]
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

                if (EmpBOManager.Authenticate(model))
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
        //[Authorize(Roles = "userBureauOrdre,adminBureauOrdre")]
        public ActionResult Index()
        {
            return View(EmpBOManager.GetEmployes().Include("Compte.Role"));
        }

        [Authorize(Roles = "adminBureauOrdre")]
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
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.roles = new SelectList(EmpBOManager.GetRoles(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(FormCollection collection, EmployeBureauOrdre employeBureauOrdre)
        {
            try
            {
                employeBureauOrdre.Compte.Role = EmpBOManager.GetRoles().FirstOrDefault(item => item.Id == employeBureauOrdre.Compte.Role.Id);
                EmpBOManager.AddEmployeBO(employeBureauOrdre);
                return RedirectToAction("Auth");
            }
            catch 
            {
                return View();
            }
        }

        [Authorize(Roles = "adminBureauOrdre")]
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

     [Authorize(Roles = "adminBureauOrdre")]
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
        [Authorize(Roles = "adminBureauOrdre")]
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
       [Authorize(Roles = "adminBureauOrdre")]
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
