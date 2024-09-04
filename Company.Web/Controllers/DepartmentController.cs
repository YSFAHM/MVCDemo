using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Company.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var departments = _departmentService.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid)
            {
                _departmentService.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Details(int? id,string viewName ="Details")
        {

            if (id == null)
            {
                return View("NotFound");
            }

            var department = _departmentService.GetById(id.Value);
            if (department == null)
            {
                return View("NotFound");
            }

            return View(viewName,department);

        }

        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }

        [HttpPost]
        public IActionResult Update(int? id, Department department)
        {
            if(department.Id != id.Value)
            {
                return View("NotFound");
            }
            _departmentService.Update(department);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var department = _departmentService.GetById(id.Value);
            if (department == null)
            {
                return View("NotFound");
            }
            _departmentService.Delete(department);
            return RedirectToAction(nameof(Index));
        }

    }
}
