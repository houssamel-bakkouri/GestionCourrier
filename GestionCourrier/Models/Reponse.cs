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
        //Nom de la personne qui a effectuée l’opération de réponse
        public AgentService Suivi { get; set; }
        public Courrier courrier { get; set; }
    }
}