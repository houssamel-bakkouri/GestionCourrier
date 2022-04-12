using GestionCourrier.DataLayer;
using GestionCourrier.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionCourrier.BusinessLayer
{
    public class EmpBOManager : InterfaceEmpBOManager
    {
        MasterDbContext context = new MasterDbContext();
        public void AddEmployeBO(EmployeBureauOrdre employe)
        {
            context.EmployeBureaus.Add(employe);
            context.SaveChanges();
        }

        public void DeleteEmploye(int id)
        {
            EmployeBureauOrdre employe = context.EmployeBureaus.Find(id);
            context.EmployeBureaus.Remove(employe);
            context.SaveChanges();
        }

        public DbSet<EmployeBureauOrdre> GetEmployes()
        {
            return context.EmployeBureaus;
        }
        public DbSet<Compte> GetComptes()
        {
            return context.Comptes;
        }
        public DbSet<Role> GetRoles()
        {
            return context.Roles;
        }

        public EmployeBureauOrdre SearchEmploye(int id)
        {
            List<EmployeBureauOrdre> employes = context.EmployeBureaus.Include("Compte").Include("Compte.Role").ToList();
            return employes.FirstOrDefault(item => item.Id == id);
        }

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