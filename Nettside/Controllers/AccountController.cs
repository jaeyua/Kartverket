using Kartverket16.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nettside.Models;
using UsersApp.ViewModels;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nettside.Controllers
{
    /// <summary>
    /// Controller for handling account-related actions such as login, registration, and profile management
    

    [Authorize] // Requires authentication for all actions by default unless otherwise specified
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager; // service provided by asp.net core identity
        private readonly UserManager<Users> userManager; // service provided by asp.net core identity


        /// <summary>
        /// Constructor for injecting SignInManager and UserManager services
        /// </summary>
        /// <param name="signInManager">a service to manage user sign-in operations</param>
        /// <param name="userManager">a service to manage user interactions</param>
        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager) // constructor to inject UserManager & SignInManager services.
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }


        // displays the login page
        [HttpGet]
        [AllowAnonymous]  // allows access without authentication
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpGet]
        [AllowAnonymous]  // allows access without authentication
        public IActionResult EmployeeOrMapuser()
        {
            return View();
        }
        
        /// <summary>
        /// Handles user login requests
        /// </summary>
        /// <param name="model">the login form data</param>
        /// <returns>redirects to the home page if successful or redisplays the login form with errors</returns>
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               // attempt to sign in the user with the provided credentials
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return View(model);
                }
            }
            return View(model);
        }



        /// <summary>
        /// displays the profile page for the logged-in user
        /// </summary>
        /// <returns>the profile page view or redirects to login if the user is not found</returns>
        [HttpGet]
        public async Task<IActionResult> ProfilePage()
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser != null)
            {
               // create a viewmodel with the current user's details
                var profilePageViewModel = new ProfilePageViewModel
                {
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Email = currentUser.Email

                };

                return View(profilePageViewModel);

            }
           // redirect to login if the user is not authenticated
            return RedirectToAction("Login");
        }



        // displays the registration page
        [AllowAnonymous] 
        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// Handles user registration requests
        /// </summary>
        /// <param name="model">the registration form data</param>
        /// <returns>redirects to login on success or redisplays the form with errors</returns>
        [ValidateAntiForgeryToken]
        [AllowAnonymous] 
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users users = new Users
                {
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,

                };

                var result = await userManager.CreateAsync(users, model.Password);

                if (result.Succeeded)
                {
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
            return View(model);
        }


        // displays the email verification
        public IActionResult VerifyEmail()
        {
            return View();
        }

        /// <summary>
        /// handles email verification submissions
        /// </summary>
        /// <param name="model">the email verification form data</param>
        /// <returns>redirects to password change on success or redisplays the form with errors</returns>
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


        /// <summary>
        /// displays the passwordchange page for a specified user
        /// </summary>
        /// <param name="username">the username of the user who is changing their passsword</param>
        /// <returns>the password change form or redirects to verifyemail if no username is provided</returns>
        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordViewModel { Email = username });
        }


        /// <summary>
        /// handles password change submissions
        /// </summary>
        /// <param name="model">the password change form data</param>
        /// <returns>redirects to login on success or redisplays the form with errors</returns>
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


        // logs out the user 
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
