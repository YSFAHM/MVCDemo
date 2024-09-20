using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(role);
                if(result.Succeeded) return RedirectToAction("Index");
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(role);
        }

        public async Task<IActionResult> Details(string Id)
        {
            if(string.IsNullOrEmpty(Id)) return NotFound();
            var role = await _roleManager.FindByIdAsync(Id);
            if(role != null) return View(role);
            return NotFound();

        }

        public async Task<IActionResult> Update(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return NotFound();
            var role = await _roleManager.FindByIdAsync(Id);
            if (role != null) return View(role);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(string Id,IdentityRole roleInput)
        {
            if (string.IsNullOrEmpty(Id)) return NotFound();
            if(Id != roleInput.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(roleInput.Id);
                role.Name = roleInput.Name;
                role.NormalizedName = roleInput.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded) RedirectToAction("Index");
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(roleInput);
        }
        public async Task<IActionResult> Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return NotFound();
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null) return NotFound();
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}
