using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCourrier.Models;
using System.Data.Entity;



namespace GestionCourrier.BusinessLayer
{
    interface InterfaceAgentServiceManager
    {
        void AddAgentService(AgentService agentService);
        DbSet<AgentService> GetAgentServices();
        AgentService SearchAgent(int id);
        void DeleteAgentService(int id);
        bool Authenticate(Compte c);
        DbSet<Role> GetRoles();
    }
}
