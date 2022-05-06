using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        Service service;

        public Compte Compte { get => compte; set => compte = value; }
        [Required]
        [DataType(DataType.Text)]
        public string Nom { get => nom; set => nom = value; }
        [Required]
        [DataType(DataType.Text)]
        public string Prenom { get => prenom; set => prenom = value; }
        [Required]
        [DataType(DataType.Text)]
        public Service Service { get => service; set => service = value; }

        public AgentService()
        {
        }
    }
}