using System.ComponentModel.DataAnnotations;

namespace Nettside.Models
{
    /// <summary>
    /// ViewModel for user registration, including fields for personal information and password.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the username for the account.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the first name. This field is required.
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name. This field is required.
        /// </summary>
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address. This field is required and must be a valid email address.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password. This field is required, must be between 8 and 40 characters long, and must match the confirmation password.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirmation of the password. This field is required.
        /// </summary>
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}



