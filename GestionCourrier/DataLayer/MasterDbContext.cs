using GestionCourrier.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionCourrier.DataLayer
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext() : base("Masterdb")
        {
        }

        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Role> Roles { get; set; } 
        public DbSet<AgentService> AgentServices { get; set; }
        public DbSet<EmployeBureauOrdre> EmployeBureaus { get; set; }
    }
}