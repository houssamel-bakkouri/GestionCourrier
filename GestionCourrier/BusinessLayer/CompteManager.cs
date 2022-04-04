using GestionCourrier.DataLayer;
using GestionCourrier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCourrier.BusinessLayer
{
    public class CompteManager : InterfaceCompteManager
    {
        MasterDbContext context = new MasterDbContext();
        public bool Authenticate(Compte c)
        {
            foreach (var item in context.Comptes)
            {
                if (item.Login == c.Login && item.Password == c.Password)
                    return true;
            }
            return false;
        }
    }
}