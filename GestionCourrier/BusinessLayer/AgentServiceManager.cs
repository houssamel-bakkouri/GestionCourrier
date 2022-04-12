using GestionCourrier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionCourrier.DataLayer;
using System.Data.Entity;


namespace GestionCourrier.BusinessLayer
{
    public class AgentServiceManager : InterfaceAgentServiceManager
    {
        MasterDbContext context = new MasterDbContext();
        public void AddAgentService(AgentService agentService)
        {
            context.AgentServices.Add(agentService);
            context.SaveChanges();
        }

        public bool Authenticate(Compte c)
        {
            throw new NotImplementedException();
        }

        public void DeleteAgentService(int id)
        {
            AgentService agentService = context.AgentServices.Find(id);
            context.AgentServices.Remove(agentService);
            context.SaveChanges();
        }

        public DbSet<AgentService> GetAgentServices()
        {
            return context.AgentServices;
        }

        public AgentService SearchAgent(int id)
        {
            List<AgentService> agent = context.AgentServices.Include("Compte").Include("Compte.Role").ToList();
            return agent.FirstOrDefault(item => item.Id == id);
        }
        public DbSet<Role> GetRoles()
        {
            return context.Roles;
        }
    }
}