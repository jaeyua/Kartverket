using Microsoft.AspNetCore.Identity;

namespace Nettside.Models
{
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }  
        public string LastName { get; set; }


       
    }
}
