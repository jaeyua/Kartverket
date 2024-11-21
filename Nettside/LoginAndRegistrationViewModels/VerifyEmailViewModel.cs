using System.ComponentModel.DataAnnotations;

namespace UsersApp.ViewModels
{
    // ViewModel for e-postverifisering, som inneholder et felt for e-postadressen.
    public class VerifyEmailViewModel
    {
        // E-postfelt som er påkrevd og må være en gyldig e-postadresse.
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
