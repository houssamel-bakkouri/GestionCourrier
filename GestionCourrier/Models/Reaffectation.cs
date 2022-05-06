using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCourrier.Models
{
    public class Reaffectation
    {
        public int Id { get; set; }
        public Service Service { get; set; }
        public DateTime Date { get; set; }
        public string Motif { get; set; }
        public EmployeBureauOrdre Employe { get; set; }
        public AgentService AgentService { get; set; }
    }
}