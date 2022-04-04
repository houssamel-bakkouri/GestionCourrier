using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionCourrier.Models;

namespace GestionCourrier.BusinessLayer
{
    interface InterfaceEmpBOManager
    {
        void AddEmployeBO(EmployeBureauOrdre employe);
        List<EmployeBureauOrdre> GetEmployes();
        EmployeBureauOrdre SearchEmploye(int id);
        void DeleteEmploye(int id);
    }
}
