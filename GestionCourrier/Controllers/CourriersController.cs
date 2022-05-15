using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionCourrier.DataLayer;
using GestionCourrier.Models;

namespace GestionCourrier.Controllers
{
    public class CourriersController : Controller
    {
        private MasterDbContext db = new MasterDbContext();

        // GET: Courriers
        public ActionResult ListTraiter()
        {
            if(User.Identity.Name == "")
            {
                return View(new List<Courrier>());
            }
            AgentService agent = db.AgentServices.Include("Compte").Include("Service").FirstOrDefault(item => item.Compte.Login == User.Identity.Name);

            List<Courrier> Courriers = db.Courriers.Where(item => item.UniteAdmin.Name == agent.Service.Name && !item.Traiter).ToList();
            return View(Courriers);
        }

        public ActionResult Traiter(int id)
        {
            Courrier courrier = db.Courriers.Include("Suivi.Compte.Notifications").Include("AdminBO.Compte.Notifications").FirstOrDefault(item => item.Id == id);
            courrier.Traiter = true;

            Notification notificationAdminBo = new Notification
            {
                Title = "Une Courrier a été bien traiter",
                Message = $"La courrier avec le réference {courrier.Id} " +
                $"a été bien traiter par {courrier.Responsable.Nom} {courrier.Responsable.Prenom}"
            };
            courrier.AdminBO.Compte.Notifications.Add(notificationAdminBo);

            Notification notificationSuivi = new Notification
            {
                Title = "Votre courrier a été bien taiter",
                Message = "La courrier que vous avez recu a été bien traiter"
            };
            courrier.Suivi.Compte.Notifications.Add(notificationSuivi);

            db.SaveChanges();
            return RedirectToAction("ListTraiter");
        }

        public ActionResult ListMessageAgent()
        {
            if (User.Identity.Name == "")
            {
                return View(new List<Courrier>());
            }
            AgentService agent = db.AgentServices.Include("Compte").Include("Service").FirstOrDefault(item => item.Compte.Login == User.Identity.Name);

            List<Courrier> Courriers = db.Courriers.Include("Suivi").Include("Reponse").Where(item => item.Suivi.Nom == agent.Nom && item.Traiter).ToList();
            return View(Courriers);
        }



        public ActionResult Index()
        {

            return View(db.Courriers.Include("Reaffectation").ToList());
        }

        // GET: Courriers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courrier courrier = db.Courriers.Find(id);
            if (courrier == null)
            {
                return HttpNotFound();
            }
            return View(courrier);
        }

        public ActionResult Repondre(int id)
        {
            Session["CourrierReponseId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Repondre(FormCollection collection, Reponse reponse)
        {
            try
            {
                Courrier courrier = db.Courriers.Find((int)Session["CourrierReponseId"]);
                Session["CourrierReponseId"] = null;
                AgentService suivi = db.AgentServices.Include("Compte").FirstOrDefault(item => item.Compte.Login == User.Identity.Name);
                reponse.Suivi = suivi;
                courrier.Reponse = reponse;
                db.SaveChanges();
                Session["ReponseId"] = reponse.Id;
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }

        }

        // GET: Courriers/Create
        public ActionResult Create()
        {
            ViewBag.Suivi = new SelectList(db.AgentServices.Include("Compte"), "Id", "Nom");
            ViewBag.UniteAdmin = new SelectList(db.Services, "Id", "Name");
            ViewBag.Responsable = new SelectList(db.AgentServices.Include("Compte"), "Id", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection, Courrier courrier)
        {
            try
            {
                courrier.DateCreation = DateTime.Now;
                courrier.Suivi = db.AgentServices.FirstOrDefault(item => item.Id == courrier.Suivi.Id);
                courrier.Responsable = db.AgentServices.FirstOrDefault(item => item.Id == courrier.Responsable.Id);
                courrier.UniteAdmin = db.Services.FirstOrDefault(item => item.Id == courrier.UniteAdmin.Id);
                EmployeBureauOrdre employeBureau = db.EmployeBureaus.Include("Compte").FirstOrDefault(item => item.Compte.Login == User.Identity.Name);
               
                if (employeBureau != null)
                {
                    courrier.AdminBO = employeBureau;
                    Notification notificationAdminService = new Notification { Title = "Nouvelle courrier a traiter", Message = $"L'employe {courrier.Suivi.Nom} {courrier.Suivi.Prenom} a recu une courrier ", };
                    AgentService responsable = db.AgentServices.Include("Compte").Include("Compte.Notifications").First(item => item.Id == courrier.Responsable.Id);
                    responsable.Compte.Notifications.Add(notificationAdminService);

                    Notification notificationAgent = new Notification { Title = "Vous avez recu une nouvelle courrier", Message = $"Vous avez recu une nouvelle courrier de type : {courrier.Type} entrein de traitement par votre admin de service" };
                    AgentService suivi = db.AgentServices.Include("Compte").Include("Compte").Include("Compte.Notifications").First(item => item.Id == courrier.Suivi.Id);
                    suivi.Compte.Notifications.Add(notificationAdminService);
                }
                db.Courriers.Add(courrier);
                db.SaveChanges();
                Session["AddedCourrierId"] = courrier.Id;
                return RedirectToAction("UploadFile");
            }
      
             catch
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                    Courrier courrier = db.Courriers.Find((int)Session["AddedCourrierId"]);
                    if(courrier != null)
                    {
                        courrier.FileSource = _path;
                        db.SaveChanges();
                        // If this courrier is a reponse to another courrier
                        if (Session["ReponseId"] != null)
                        {
                            Reponse reponse = db.Reponses.Include("courrier").FirstOrDefault(item => item.Id == (int)Session["Reponse"]);
                            reponse.courrier = courrier;
                            db.SaveChanges();
                            return RedirectToAction("ListMessageAgent");
                        }

                        // If this courrier is a part of a dossier
                        if (Session["DossierCourrier"] != null)
                        {
                            int id = (int)Session["DossierCourrier"];
                            Dossier dossier = db.Dossiers.Include("Courriers").FirstOrDefault(item => item.Id == id);
                            dossier.Courriers.Add(courrier);
                            db.SaveChanges();
                            return RedirectToAction("Index", "Dossiers");
                        }
                    }
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

        public ActionResult Reaffecter(int id)
        {
            Session["CourrierId"] = id;
            ViewBag.Service = new SelectList(db.Services, "Id", "Name");
            ViewBag.Employe = new SelectList(db.EmployeBureaus, "Id", "Nom");
            ViewBag.AgentService = new SelectList(db.AgentServices, "Id", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reaffecter(FormCollection collection, Reaffectation model)
        {
            try
            {
                model.Service = db.Services.FirstOrDefault(item => item.Id == model.Service.Id);
                model.Employe = db.EmployeBureaus.FirstOrDefault(item => item.Id == model.Employe.Id);
                model.AgentService = db.AgentServices.FirstOrDefault(item => item.Id == model.AgentService.Id);
                Courrier courrier = db.Courriers.Find((int)Session["CourrierId"]);
                courrier.Reaffectation = model;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            catch
            {
                return View();
            }

        }

        // GET: Courriers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courrier courrier = db.Courriers.Find(id);
            if (courrier == null)
            {
                return HttpNotFound();
            }
            return View(courrier);
        }

        // POST: Courriers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CoordoneeExpediteur,Type,Nature,DateCourrier,DateArrive,DateCreation,Demande,Objet,Commentaire")] Courrier courrier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courrier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courrier);
        }

        // GET: Courriers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courrier courrier = db.Courriers.Find(id);
            if (courrier == null)
            {
                return HttpNotFound();
            }
            return View(courrier);
        }

        // POST: Courriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Courrier courrier = db.Courriers.Find(id);
            db.Courriers.Remove(courrier);
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
