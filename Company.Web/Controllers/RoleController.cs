using Company.Data.Entities;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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
                if (result.Succeeded) return RedirectToAction("Index");
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

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return NotFound();
            ViewBag.RoleId = roleId;
            var users = await _userManager.Users.ToListAsync();
            var usersInRole = new List<UserInRoleViewModel>();
            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,

                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if(role is null) return NotFound();
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is not null)
                    {
                        if(user.IsSelected && ! await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser,role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                }
                return RedirectToAction("Update",new {id = roleId});
            }
            return View(users);
        }


    }
}
