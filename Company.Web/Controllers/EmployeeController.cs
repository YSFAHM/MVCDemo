using Company.Data.Entities;
using Company.Service.Dtos;
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
        [HttpGet]
        public IActionResult Index(string? searchInp)
        {
            IEnumerable<EmployeeDto> employeesDtos = new List<EmployeeDto>();
            if(string.IsNullOrEmpty(searchInp))
            {
                employeesDtos = _employeeService.GetAll();
            }
            else
            {
                employeesDtos = _employeeService.GetEmployeesByName(searchInp);
                
            }
            return View(employeesDtos);

        }

        public IActionResult Details(int? id)
        {
            if (id is null) return View("NotFound");
            var employeeDto = _employeeService.GetById(id.Value);
            if (employeeDto is null) return View("NotFound");
            return View(employeeDto);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDto employeeDto)
        {
            
            if (ModelState.IsValid)
            {
                _employeeService.Add(employeeDto);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = _departmentService.GetAll();
            return View(employeeDto);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            _employeeService.Delete(id.Value);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int?id)
        {
            if (id is null) return View("NotFound");
            var employee = _employeeService.GetById(id.Value);
            if (employee is null) return View("NotFound");

            ViewBag.Departments = _departmentService.GetAll();
            return View(employee);
        }
        [HttpPost]
        public IActionResult Update(int? id ,EmployeeDto employeeDto)
        {
            if (employeeDto.Id != id.Value)
            {
                return View("NotFound");
            }
            _employeeService.Update(employeeDto);
            return RedirectToAction(nameof(Index));
        }
    }
}
