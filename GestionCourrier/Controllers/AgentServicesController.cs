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
using GestionCourrier.BusinessLayer;
using System.Web.Security;

namespace GestionCourrier.Controllers
{
    public class AgentServicesController : Controller
    {
        private MasterDbContext db = new MasterDbContext();

        private RolesManager rolesManager = new RolesManager();
        private AgentServiceManager AgentServiceManager = new AgentServiceManager();
        private CompteManager CompteManager = new CompteManager();

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

                if (AgentServiceManager.Authenticate(model))
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

        // GET: AgentServices
      [Authorize(Roles = "userService,adminService")]
        public ActionResult Index()
        {
            return View(AgentServiceManager.GetAgentServices());
        }

        // GET: AgentServices/Details/5
[Authorize(Roles = "adminService")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentService agentService = AgentServiceManager.SearchAgent((int)id);
            if (agentService == null)
            {
                return HttpNotFound();
            }
            return View(agentService);
        }

        [AllowAnonymous]
        // GET: AgentServices/Create
        public ActionResult Create()
        {
            ViewBag.roles = new SelectList(AgentServiceManager.GetRoles(), "Id", "Name");
            ViewBag.Service = new SelectList(db.Services, "Id", "Name");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(FormCollection collection, AgentService agentService)
        {
            try
            {
                agentService.Compte.Role = db.Roles.FirstOrDefault(item => item.Id == agentService.Compte.Role.Id);
                agentService.Service = db.Services.FirstOrDefault(item => item.Id == agentService.Service.Id);
                //AgentServiceManager.AddAgentService(agentService);
                db.AgentServices.Add(agentService);
                db.SaveChanges();
                return RedirectToAction("Auth");
            }
            catch
            {
                return View();
            }
        }

       [Authorize(Roles = "adminService")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentService agentService = db.AgentServices.Find(id);
            if (agentService == null)
            {
                return HttpNotFound();
            }
            return View(agentService);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "adminService")]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,Service")] AgentService agentService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agentService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentService);
        }

        // GET: AgentServices/Delete/5
        [Authorize(Roles = "adminService")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgentService agentService = AgentServiceManager.SearchAgent((int)id);
            if (agentService == null)
            {
                return HttpNotFound();
            }
            return View(agentService);
        }

        // POST: AgentServices/Delete/5
        [Authorize(Roles = "adminService")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /*AgentService agentService = db.AgentServices.Find(id);
            db.AgentServices.Remove(agentService);
            db.SaveChanges();*/
            AgentServiceManager.DeleteAgentService(id);
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
