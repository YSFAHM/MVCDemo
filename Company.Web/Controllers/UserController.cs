using Company.Data.Entities;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager,ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;

        }
        public async Task<IActionResult> Index(string searchInp)
        {
            List<ApplicationUser> users;
            if(string.IsNullOrEmpty(searchInp))
            {
                users = await _userManager.Users.ToListAsync();
            }
            else
            {
                users = await _userManager.Users.Where(u => u.NormalizedEmail.Trim().Contains(searchInp.Trim().ToUpper())).ToListAsync();
            }
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            if(string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                return View(user);
            }
            return NotFound();
        }

        public async Task<IActionResult> Update(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                ApplicationUserUpdateViewModel applicationUserUpdateView = new ApplicationUserUpdateViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
                return View(applicationUserUpdateView);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationUserUpdateViewModel applicationUserUpdateView)
        {
            if(applicationUserUpdateView.Id != id) return NotFound();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound();
                user.UserName = applicationUserUpdateView.UserName;
                user.NormalizedUserName = applicationUserUpdateView.UserName.ToUpper();
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User Updated Successfully");
                    return RedirectToAction("Index");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    _logger.LogError(error.Description);
                }
                
            }
            return View(applicationUserUpdateView);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if(string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var LoggedUserID = _userManager.GetUserId(User);
            if(LoggedUserID == user.Id)
            {
                _logger.LogError("Cannot Remove the Logged User");
                return RedirectToAction("Index");
            }
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}
