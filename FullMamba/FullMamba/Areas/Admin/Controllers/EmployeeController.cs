using FullMamba_Business.Exceptions;
using FullMamba_Business.Services.Abstracts;
using FullMamba_Core.Models;
using Microsoft.AspNetCore.Mvc;
using FileNotFoundException = FullMamba_Business.Exceptions.FileNotFoundException;

namespace FullMamba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            List<Employee> employees = _employeeService.GetAllEmployees();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if(!ModelState.IsValid) return View();

            try
            {
                _employeeService.AddEmployee(employee);
            }
            catch(NullReferenceException ex)
            {
                return NotFound();
            }
            catch(FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var existEmployee = _employeeService.GetEmployee(x => x.Id == id);
            if (existEmployee == null) return NotFound();

            try
            {
                _employeeService.DeleteEmployee(id);
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var existEmployee = _employeeService.GetEmployee(x => x.Id == id);
            if (existEmployee == null) return NotFound();

            return View(existEmployee);
        }

        [HttpPost]
        public IActionResult Update(int id, Employee employee)
        {
            if (!ModelState.IsValid) 
                return View();

            try
            {
                _employeeService.UpdateEmployee(id, employee);
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
