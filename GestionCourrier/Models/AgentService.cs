using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCourrier.Models
{
    public class AgentService
    {
        public int Id { get; set; }

        Compte compte;
        string nom;
        string prenom;
        string service;

        public Compte Compte { get => compte; set => compte = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Service { get => service; set => service = value; }

        public AgentService()
        {
        }

        public AgentService(Compte compte, string nom, string prenom, string service)
        {
            this.compte = compte;
            this.nom = nom;
            this.prenom = prenom;
            this.service = service;
        }

    }
}