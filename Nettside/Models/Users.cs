using Microsoft.AspNetCore.Identity;

namespace Nettside.Models
{
    // Brukermodell som arver fra IdentityUser og legger til fornavn og etternavn.
    public class Users : IdentityUser
    {
        // Brukerens fornavn.
        public string FirstName { get; set; }

        // Brukerens etternavn.
        public string LastName { get; set; }
    }
}
