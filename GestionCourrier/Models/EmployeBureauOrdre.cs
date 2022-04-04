using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCourrier.Models
{
    public class EmployeBureauOrdre
    {
        public int Id { get; set; }
        public Compte Compte { get => compte; set => compte = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }

        Compte compte;
        string nom;
        string prenom;

        public EmployeBureauOrdre()
        {
        }

        public EmployeBureauOrdre(Compte compte, string nom, string prenom)
        {
            this.compte = compte;
            this.nom = nom;
            this.prenom = prenom;
        }

        public override string ToString()
        {
            return "Nom :" + nom + " Prenom:" + prenom + " Compte :" + compte;
        }
    }
}