using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Service.Dtos;
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
            var departmentsDtos = _departmentService.GetAll();
            return View(departmentsDtos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentDto departmentDto)
        {
            if(ModelState.IsValid)
            {
                _departmentService.Add(departmentDto);
                return RedirectToAction(nameof(Index));
            }
            return View(departmentDto);
        }

        public IActionResult Details(int? id,string viewName ="Details")
        {

            if (id == null)
            {
                return View("NotFound");
            }

            var departmentDto = _departmentService.GetById(id.Value);
            if (departmentDto == null)
            {
                return View("NotFound");
            }

            return View(viewName,departmentDto);

        }

        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }

        [HttpPost]
        public IActionResult Update(int? id, DepartmentDto departmentDto)
        {
            if(departmentDto.Id != id.Value)
            {
                return View("NotFound");
            }
            _departmentService.Update(departmentDto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            _departmentService.Delete(id.Value);
            return RedirectToAction(nameof(Index));
        }

    }
}
