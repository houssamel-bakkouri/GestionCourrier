using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionCourrier.Controllers
{
    public class CourrierController : Controller
    {
        // GET: Courrier
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjouterCourrier()
        {
            ViewBag.lService = new List<string>();
            return View();
        }

        [HttpPost]
        public ActionResult AjouterCourrierForm()
        {
            Response.Write("<script>console.log('hhhh');</script>");
            // Response.Write("<script>console.log('hhhhhhh" + res + explication + dateApp + freq + ord + "');</script>");
            //string[] t = res.Split(',');
            /* Ressource r = searchById(int.Parse(t[0]));
             Panne p = new Panne();
             p.Ressource = r;
             p.DateApparition = DateTime.Parse(dateApp);
             if (freq == "frequente")
                 p.Freq = frequence.frequente;
             if (freq == "rare")
                 p.Freq = frequence.rare;
             if (freq == "permanente")
                 p.Freq = frequence.permanente;

             if (ord == "materiel")
                 p.Ordre = ordre.materiel;
             if (ord == "defautSysteme")
                 p.Ordre = ordre.defautSysteme;
             if (ord == "logicielUtilitaire")
                 p.Ordre = ordre.logicielUtilitaire;
             //add to list
             besManager.AjouterPanne(p);*/
            return new EmptyResult();
            // return RedirectToAction("SignalerPanne");
        }

    }
}
