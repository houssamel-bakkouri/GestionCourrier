using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        /*[Required]
        public Courrier courrier { get; set; }*/
    }
}