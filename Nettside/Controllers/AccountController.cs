
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nettside.Models;
using Nettside.ViewModels;
using System.Net.WebSockets;

namespace Nettside.Controllers
{
    /// <summary>
    /// Controller to handle account-related actions like registration, login and profile management. 
    /// </summary>

    [Authorize] // requires authentication for all actions by default unless otherwise specified 
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager; // service provided by asp.net core identity
        private readonly UserManager<Users> userManager;    // service provided by asp.net core identity



        /// <summary>
        /// Constructor to initialize SignInManager and UserManager
        /// </summary>
        /// <param name="signInManager">a service to manage user sign-in operations</param>
        /// <param name="userManager">a service to manage user interactions</param>
        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager) // constructor to inject signInManager and UserManager
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
        
       

        /// <summary> /
        /// Handles user registration form submission and creates a new user account
        /// </summary>
        /// <param name="registerViewModel">the user registration details</param>
        /// <returns>redirects to login on success or reloads the registration page on failure</returns>
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



        // displays the registration page for a caseworker.
        [HttpGet]
        public async Task<ActionResult> RegisterCaseWorker()
        {

            return View();
        }


        /// <summary>
        /// handles caseworker registration form submission. 
        /// </summary>
        /// <param name="registerViewModel">the caseworker registration details.</param>
        /// <returns>redirects to login on success or reloads the registration page on failure </returns>
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
                // Add the user to the "Caseworker" role
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

         
        /// <summary>
        /// handles login form submission and authenticates the user
        /// </summary>
        /// <param name="loginViewmodel">the login details</param>
        /// <returns>redirects to home on success or reloads the login page on failure.</returns>
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


     
        // displays the employee or map user selection page
        [HttpGet]
        [AllowAnonymous]
        public IActionResult EmployeeOrMapUser()
        {
            return View();
        }



       /// <summary>
       /// assigns a role to the logged-in user based on the selected option.
       /// </summary>
       /// <param name="userRole">the selected role.</param>
       /// <returns>redirects to the role selection page if the role is invalid or assigns the role successfully.</returns>
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


       
           





        // displays the email verification page
        public IActionResult VerifyEmail()
        {
            return View();
        }


        
        /// <summary>
        /// handles email verification form submission
        /// </summary>
        /// <param name="model">the email verification details</param>
        /// <returns>redirects to changepassword if the email is valid or reloads the verification page on failure.</returns>
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





        /// <summary>
        /// displays the changepassword page
        /// </summary>
        /// <param name="model">the username associated with the account.</param>
        /// <returns></returns>
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



       // displays the access denied page for unauthorized users.
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
