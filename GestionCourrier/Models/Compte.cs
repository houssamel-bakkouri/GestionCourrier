using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionCourrier.Models
{
    public class Compte
    {
        public int Id { get; set; }

        string login;
        string password;
        Role  role;

        public Compte()
        {
            login = password = "??????";
        }
        [Required]
        [DataType(DataType.Text)]
        public string Login { get => login; set => login = value; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Le mot de passe est faible")]
        [DataType(DataType.Password)]
        public string Password { get => password; set => password = value; }
        public Role Role { get => role; set => role = value; }
        public ICollection<Notification> Notifications { get; set; }

        public override string ToString()
        {
            return "Id " + Id +  "Login :" + login + " Password :" + password;
        }
    }
}