using Microsoft.AspNetCore.Identity;

namespace Nettside.Models
{
    /// <summary>
    /// User model that inherits from IdentityUser and adds first and last name properties.
    /// </summary>
    public class Users : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string LastName { get; set; }
    }
}



