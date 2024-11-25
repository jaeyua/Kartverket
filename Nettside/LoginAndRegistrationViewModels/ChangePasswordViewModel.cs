using System.ComponentModel.DataAnnotations;

namespace UsersApp.ViewModels
{
    /// <summary>
    /// ViewModel for changing the password, containing fields for email and password.
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Gets or sets the email address. This field is required and must be a valid email address.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the new password. This field is required, must be between 8 and 40 characters long, and must match the confirmation password.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password does not match.")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirmation of the new password. This field is required.
        /// </summary>
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}

