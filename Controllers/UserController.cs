using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RopeyDVD.Areas.Identity.Data;
using RopeyDVD.Data;
using RopeyDVD.Models;
using System.ComponentModel.DataAnnotations;

namespace RopeyDVD.Controllers
{

    public class UserController : Controller
    {
        private readonly RopeyDVDContext _context;
        private UserManager<RopeyDVDUser> _userManager;
        private readonly IUserEmailStore<RopeyDVDUser> _emailStore;
        private readonly IUserStore<RopeyDVDUser> _userStore;
        private readonly ILogger<UserController> _logger;

        public UserController(RopeyDVDContext context,
                              UserManager<RopeyDVDUser> userManager,
                              IUserStore<RopeyDVDUser> userStore,
                              ILogger<UserController> logger)
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _logger = logger;
        }




        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Create()
        {

            return View();
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserInputModel model)
        {

            if (ModelState.IsValid)
            {
                var checkUser = await _userManager.FindByNameAsync(model.Email);

                if (checkUser != null)
                {
                    ModelState.AddModelError("CustomError", "User already exists with that email.");
                    return View(model);
                }

                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    _logger.LogInformation("UserCreationFailed");
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("CustomError", error.Description);
                    }
                    return View(model);
                }
                else { return RedirectToAction("Index"); }
            }

            return View();
        }

        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Edit(string id)
        {

            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
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

            if (user == null)
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
            if (ModelState.IsValid)
            {
                RopeyDVDUser ropeyUser = await _userManager.FindByIdAsync(user.Id);


                RopeyDVDUser otherUser = await _userManager.FindByEmailAsync(user.Email);

                // If the other user has a different ID then the email is already taken.
                if (otherUser != null && otherUser.Id != ropeyUser.Id)
                {
                    ModelState.AddModelError("CustomError", "This Email already taken");
                    return View("Edit", user);
                }

                ropeyUser.Email = user.Email;
                ropeyUser.PasswordHash = _userManager.PasswordHasher.HashPassword(ropeyUser, user.Password);

                var result = await _userManager.UpdateAsync(ropeyUser);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", new { id = user.Id });
        }

        private RopeyDVDUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<RopeyDVDUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(RopeyDVDUser)}'. " +
                    $"Ensure that '{nameof(RopeyDVDUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<RopeyDVDUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<RopeyDVDUser>)_userStore;
        }
    }
}
