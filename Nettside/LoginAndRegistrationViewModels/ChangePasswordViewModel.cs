using System.ComponentModel.DataAnnotations;

namespace UsersApp.ViewModels
{
    // ViewModel for endring av passord, som inneholder felt for e-post og passord.
    public class ChangePasswordViewModel
    {
        // E-postfelt som er påkrevd og må være en gyldig e-postadresse.
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        // Nytt passord som er påkrevd, med minimum 8 tegn og maksimum 40 tegn, og det må bekrefte passordet.
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password does not match.")]
        public string NewPassword { get; set; }

        // Bekreftelse på det nye passordet som er påkrevd.
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
