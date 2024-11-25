using System.ComponentModel.DataAnnotations;

namespace UsersApp.ViewModels
{
    /// <summary>
    /// ViewModel for email verification, containing a field for the email address.
    /// </summary>
    public class VerifyEmailViewModel
    {
        /// <summary>
        /// Gets or sets the email address. This field is required and must be a valid email address.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}


