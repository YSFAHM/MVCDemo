using Company.Data.Entities;
using Company.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService,IDepartmentService departmentService) 
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var employees = _employeeService.GetAll();
            return View(employees);
        }

        public IActionResult Details(int? id)
        {
            if (id is null) return View("NotFound");
            var employee = _employeeService.GetById(id.Value);
            if (employee is null) return View("NotFound");
            return View(employee);
        }

        public IActionResult Create()
        {
            TempData["Departments"] = _departmentService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            
            if (ModelState.IsValid)
            {
                _employeeService.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var employee = _employeeService.GetById(id.Value);
            if (employee == null)
            {
                return View("NotFound");
            }
            _employeeService.Delete(employee);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int?id)
        {
            if (id is null) return View("NotFound");
            var employee = _employeeService.GetById(id.Value);
            if (employee is null) return View("NotFound");

            TempData["Departments"] = _departmentService.GetAll();
            return View(employee);
        }
        [HttpPost]
        public IActionResult Update(int? id ,Employee employee)
        {
            if (employee.Id != id.Value)
            {
                return View("NotFound");
            }
            _employeeService.Update(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}
