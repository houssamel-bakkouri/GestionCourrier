﻿using System;
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
            Courrier courrier = db.Courriers.Find(id);
            courrier.Traiter = true;
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

            List<Courrier> Courriers = db.Courriers.Include("Suivi").Where(item => item.Suivi.Nom == agent.Nom).ToList();
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

        // GET: Courriers/Create
        public ActionResult Create()
        {
            ViewBag.Suivi = new SelectList(db.AgentServices, "Id", "Nom");
            ViewBag.UniteAdmin = new SelectList(db.Services, "Id", "Name");
            ViewBag.Responsable = new SelectList(db.AgentServices, "Id", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection, Courrier courrier)
        {
            try
            {
                courrier.Suivi = db.AgentServices.FirstOrDefault(item => item.Id == courrier.Suivi.Id);
                courrier.Responsable = db.AgentServices.FirstOrDefault(item => item.Id == courrier.Responsable.Id);
                courrier.UniteAdmin = db.Services.FirstOrDefault(item => item.Id == courrier.UniteAdmin.Id);
                EmployeBureauOrdre employeBureau = db.EmployeBureaus.Include("Compte").FirstOrDefault(item => item.Compte.Login == User.Identity.Name);
                if (employeBureau != null)
                {
                    courrier.AdminBO = employeBureau;
                }
                db.Courriers.Add(courrier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
      
             catch
            {
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