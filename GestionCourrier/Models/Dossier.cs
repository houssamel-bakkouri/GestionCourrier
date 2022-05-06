using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCourrier.Models
{
    public class Dossier
    {
        public int Id { get; set; }
        public string dossiername { get; set; }
        public string dossierObjet { get; set; }
        public AgentService agentService { get; set; }
        public EmployeBureauOrdre employeBureau { get; set; }
    }
}