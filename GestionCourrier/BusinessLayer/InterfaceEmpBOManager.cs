using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCourrier.Models;

namespace GestionCourrier.BusinessLayer
{
    interface InterfaceEmpBOManager
    {
        void AddEmployeBO(EmployeBureauOrdre employe);
        DbSet<EmployeBureauOrdre> GetEmployes();
        EmployeBureauOrdre SearchEmploye(int id);
        void DeleteEmploye(int id);
        bool Authenticate(Compte c);
        DbSet<Role> GetRoles();
    }
}
