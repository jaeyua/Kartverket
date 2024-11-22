using Kartverket16.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nettside.Models;
using System.Net.WebSockets;
using UsersApp.ViewModels;

namespace Nettside.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;    

        // Constructor to initialize SignInManager and UserManager
        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        

        // Displays the registration page
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        // Handles registration form submission
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var createUser = new Users
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Username
            };

            // Attempt to create the user
            var applicationResult = await userManager.CreateAsync(createUser, registerViewModel.Password);

            if (applicationResult.Succeeded)
            {
                // Add the user to the "PrivateUser" role
                var applicationIdentityResult = await userManager.AddToRoleAsync(createUser, "PrivateUser");

                if (applicationIdentityResult.Succeeded)
                {
                    // Redirect to Login if successful
                    return RedirectToAction("Login");
                }
            }

            // If any of the operations fail, return the Register view with errors
            foreach (var error in applicationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(registerViewModel);
        }




        [HttpGet]
        public async Task<ActionResult> RegisterCaseWorker()
        {

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RegisterCaseworker(RegisterViewModel registerViewModel)
        {
            var createUser = new Users
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Username
            };

            // Attempt to create the user
            var applicationResult = await userManager.CreateAsync(createUser, registerViewModel.Password);

            if (applicationResult.Succeeded)
            {
                // Add the user to the "PrivateUser" role
                var applicationIdentityResult = await userManager.AddToRoleAsync(createUser, "Caseworker");

                if (applicationIdentityResult.Succeeded)
                {
                    // Redirect to Login if successful
                    return RedirectToAction("Login");
                }
            }

            // If any of the operations fail, return the Register view with errors
            foreach (var error in applicationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(registerViewModel);
        }









        // Displays the login page
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

         // Handles login form submission
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewmodel)
        {
            if (!ModelState.IsValid)
            {

             return View();
            }


                var signInresult = await signInManager.PasswordSignInAsync(loginViewmodel.UserName, loginViewmodel.Password, false, false);
           
                // attempt to log in the user
              

                if (signInresult != null && signInresult.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");

                    return View(loginViewmodel);
                }
            
            
        }


     
        [HttpGet]
        [AllowAnonymous]
        public IActionResult EmployeeOrMapUser()
        {
            return View();
        }


        // Displays the page for selecting employee or map user
        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> EmployeeOrMapuser(string userRole)
        {
           // checks if the role is valid
           if(string.IsNullOrEmpty(userRole))
            {
                // handle invalid input
                return RedirectToAction("EmployeeOrMapUser");
            }

            // get the current logged-in user
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // add role to the user
            var result = await userManager.AddToRoleAsync(user, userRole);

          
            
            return View();
        }


        // Displays the profile page for the logged-in user
        [HttpGet]
        public async Task<IActionResult> ProfilePage(RegisterViewModel registerViewModel)
        {

            return View();
        }


       
           





        // Displays the email verification page
        public IActionResult VerifyEmail()
        {
            return View();
        }

        // Handles email verification form submission
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "No user found with the specified email address.");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
                }
            }
            return View(model);
        }

        // Displays the change password page
    
        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("ChangePassword", "Account");
            }
            return View(new ChangePasswordViewModel { Email = username });
        }

        // Handles change password form submission
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(user, model.NewPassword);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. Please try again.");
                return View(model);
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


        // Logs the user out and redirects to the home page
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
