using GestionCourrier.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCourrier.BusinessLayer
{
    interface InterfaceRolesManager
    {
        DbSet<Role> GetRoles();
    }
}
