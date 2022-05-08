using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCourrier.Models
{
    public class Dossier
    {
        public int Id { get; set; }
        public string Dossiername { get; set; }
        public string DossierObjet { get; set; }
        public Service Service { get; set; }
        public AgentService Responsable { get; set; }
        public ICollection<Courrier> Courriers { get; set; }
        public bool Sent { get; set; } = false;
    }
}