using FullMamba_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullMamba_Business.Services.Abstracts
{
    public interface IEmployeeService
    {
        void AddEmployee(Employee employee);
        void DeleteEmployee(int id);
        void UpdateEmployee(int id, Employee employee);
        Employee GetEmployee(Func<Employee, bool>? func = null);
        List<Employee> GetAllEmployees(Func<Employee, bool>? func = null);
    }
}
