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
        public MasterDbContext() : base("CourriersDB")
        {
        }

        public DbSet<Compte> Comptes { get; set; }
        public DbSet<Role> Roles { get; set; } 
        public DbSet<AgentService> AgentServices { get; set; }
        public DbSet<EmployeBureauOrdre> EmployeBureaus { get; set; }
        public DbSet<Courrier> Courriers { get; set; }
        public DbSet<Reaffectation> Reaffectations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Dossier> Dossiers { get; set; }
        public DbSet<Reponse> Reponses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}