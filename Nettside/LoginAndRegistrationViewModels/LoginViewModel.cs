using System.ComponentModel.DataAnnotations;

namespace UsersApp.ViewModels
{
    // ViewModel for innlogging, som inneholder felt for e-post, passord og husk meg-valg.
    public class LoginViewModel
    {
        // E-postfelt som er påkrevd og må være en gyldig e-postadresse.
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        // Passordfelt som er påkrevd og skal behandles som et passord.
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Boolsk verdi for å angi om brukeren skal huskes ved neste pålogging.
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
