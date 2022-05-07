using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RopeyDVD.Areas.Identity.Data;
using RopeyDVD.Data;
using RopeyDVD.Models;

namespace RopeyDVD.Controllers
{
    
    public class UserController : Controller
    {
        private readonly RopeyDVDContext _context;
        private UserManager<RopeyDVDUser> _userManager;

        public UserController(RopeyDVDContext context,
                              UserManager<RopeyDVDUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Edit(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if(user == null)
            {
                return View("Error");
            }

            var editUser = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Password = string.Empty
            };

            return View(editUser);
        }

        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if(user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> OnPostAsync(EditUserViewModel user)
        {

            RopeyDVDUser ropeyUser = await _userManager.FindByIdAsync(user.Id);

            if(ropeyUser == null)
            {
                return NotFound();
            }

            ropeyUser.PasswordHash = _userManager.PasswordHasher.HashPassword(ropeyUser, user.Password);

            var result = await _userManager.UpdateAsync(ropeyUser);

            return RedirectToAction("Index");
        }
    }
}
