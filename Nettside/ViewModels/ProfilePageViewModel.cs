namespace Kartverket16.Models.ViewModels
{
    // ViewModel som representerer informasjonen som vises på brukerens profilside.
    public class ProfilePageViewModel
    {
        // Brukerens fornavn.
        public string FirstName { get; set; }

        // Brukerens etternavn.
        public string LastName { get; set; }

        // Brukerens e-postadresse.
        public string Email { get; set; }
    }
}
