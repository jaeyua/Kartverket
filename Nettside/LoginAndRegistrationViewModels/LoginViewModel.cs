using System.ComponentModel.DataAnnotations;

namespace UsersApp.ViewModels
{
    /// <summary>
    /// ViewModel for user login, containing fields for email, password, and remember me option.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the email address. This field is required and must be a valid email address.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password. This field is required and should be treated as a password.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user should be remembered on the next login.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

