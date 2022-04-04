using GestionCourrier.DataLayer;
using GestionCourrier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionCourrier.BusinessLayer
{
    public class RolesManager : InterfaceRolesManager
    {
        MasterDbContext context = new MasterDbContext();
        public List<Role> GetRoles()
        {
            return context.Roles.ToList();
        }
    }
}