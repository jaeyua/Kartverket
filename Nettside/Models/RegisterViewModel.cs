using System.ComponentModel.DataAnnotations;

namespace Nettside.Models
{
    // ViewModel for registrering av bruker, inkludert felt for personlig informasjon og passord.
    public class RegisterViewModel
    {
        // Brukernavn for kontoen.
        public string Username { get; set; }

        // Fornavn som er påkrevd ved registrering.
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        // Etternavn som er påkrevd ved registrering.
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        // E-postadresse som er påkrevd og må være gyldig.
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        // Passord som er påkrevd, og må være mellom 8 og 40 tegn langt.
        // Passordet må også bekreftes mot bekreftelsespassordet.
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
        public string Password { get; set; }

        // Bekreftelse av passord som er påkrevd.
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
