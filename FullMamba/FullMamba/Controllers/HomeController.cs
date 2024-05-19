
using FullMamba_Business.Services.Abstracts;
using FullMamba_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FullMamba.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public HomeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            List<Employee> employees = _employeeService.GetAllEmployees();
            return View(employees);
        }
    }
}
