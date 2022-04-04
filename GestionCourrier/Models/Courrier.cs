using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exempleWeb2A.myModels
{
    enum typeCourrier {courrier, document};
    enum natureCourrier {facture, borderau};
    public class Courrier
    {
        private int id;
        //private string refCourrier;
        private Dossier dossier;
        private string orgName;
        private string orgAddress;
        private typeCourrier typeCourrier;
        private natureCourrier natureCourrier;
        private DateTime dateCourrier;
        private DateTime dateArrivee;
        private DateTime dateCreation;
        private string uniteAdmin;
        private Service service;
        private Employe employe;
        private Employe traitEmploye;
        private bool demande;
        private string objectCourrier;
        private string commentaire;
                
    }
}