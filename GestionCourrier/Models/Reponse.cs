using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCourrier.Models
{
    public class Reponse
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public AgentService Suivi { get; set; }
    }
}