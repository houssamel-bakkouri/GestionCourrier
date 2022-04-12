using GestionCourrier.DataLayer;
using GestionCourrier.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionCourrier.BusinessLayer
{
    public class RolesManager : InterfaceRolesManager
    {
        MasterDbContext context = new MasterDbContext();
        public DbSet<Role> GetRoles()
        {
            return context.Roles;
        }
    }
}