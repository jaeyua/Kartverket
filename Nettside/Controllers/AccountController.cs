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
    [Authorize]
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
        public async Task<IActionResult> Register(RegisterViewModel registerViewmodel)
        {
            if (ModelState.IsValid)
            {
                Users IdentityUser = new Users
                {
                    UserName = registerViewmodel.Email,
                    FirstName = registerViewmodel.FirstName,
                    LastName = registerViewmodel.LastName,
                    Email = registerViewmodel.Email,
                };


                // attempt to create the user 
                var IdentityResult = await userManager.CreateAsync(IdentityUser, registerViewmodel.Password);

                if (IdentityResult.Succeeded)
                {
                    // assign this user the "PrivateUser" role
                    var roleIdentityResult = await userManager.AddToRoleAsync(IdentityUser, "PrivateUser");

                    if (roleIdentityResult.Succeeded)
                    {
                        // show success notification
                        return RedirectToAction("Login", "Account");
                    }


                }
                else
                {
                    // show error notification
                    return RedirectToAction("Register");
                }
            }

            return View();
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
            if (ModelState.IsValid)
            {
                // attempt to log in the user
                var signInresult = await signInManager.PasswordSignInAsync(loginViewmodel.Email, loginViewmodel.Password, loginViewmodel.RememberMe, false);

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
            return View(loginViewmodel);
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

            if(result.Succeeded)
            {
                if(userRole == "PrivateUser")
                {
                    return RedirectToAction("ProfilePage", "Home");
                }
            }

            else if (userRole == "Caseworker") 
            {
                
                return RedirectToAction("VisAnsattHjem", "Home");
            }
            else if (userRole == "SystemAdministrator")
            {
                return RedirectToAction("AdminDashBoard", "Home");
            }
            
            return View();
        }


        // Displays the profile page for the logged-in user
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
                return RedirectToAction("VerifyEmail", "Account");
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
