using Kartverket16.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nettside.Models;
using UsersApp.ViewModels;

namespace Nettside.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager, ILogger<AccountController> logger)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult EmployeeOrMapuser()
        {
            return View();
        }
        
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if user exists
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    logger.LogWarning("Login failed: User not found for email {Email}", model.Email);
                    return View(model);
                }

                // Attempt to sign in with UserName
                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    logger.LogInformation("User {Email} successfully logged in.", model.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    logger.LogWarning("Login failed for user {Email}. Reason: {Result}", model.Email, result.ToString());
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Ensure UserName is set to Email
                var user = new Users
                {
                    UserName = model.Email, // Use email as username
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    logger.LogInformation("User {Email} successfully registered.", model.Email);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        logger.LogError("Registration error for {Email}: {Error}", model.Email, error.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ProfilePage()
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var profilePageViewModel = new ProfilePageViewModel
                {
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Email = currentUser.Email
                };

                return View(profilePageViewModel);
            }

            return RedirectToAction("Login");
        }
    }
}
