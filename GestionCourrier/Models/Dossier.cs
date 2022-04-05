using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exempleWeb2A.myModels
{
    public class Dossier
    {
        private int id;
        private string dossiername;
        private string dossierObjet;
        private Service dossierService;
        public Employe dossierEmploye;
    }
}