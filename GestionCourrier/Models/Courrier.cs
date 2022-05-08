using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCourrier.Models
{
    public class Courrier
    {
        public int Id { get; set; }
        //public Dossier Dossier { get; set; }
        public string CoordoneeExpediteur { get; set; }
        public string Type { get; set; }
        public string Nature { get; set; }
        public DateTime DateCourrier { get; set; }
        public DateTime DateArrive { get; set; }
        public DateTime DateCreation { get; set; }
        public EmployeBureauOrdre AdminBO { get; set; }
        public AgentService Suivi { get; set; }
        public AgentService Responsable { get; set; }
        public bool Demande { get; set; }
        public string Objet { get; set; }
        public string Commentaire { get; set; }
        public Reaffectation Reaffectation { get; set; }
        public Service UniteAdmin { get; set; }
        public bool Traiter { get; set; } = false;
        public string FileSource { get; set; }
    }
}