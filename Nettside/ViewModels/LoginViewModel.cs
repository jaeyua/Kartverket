using System.ComponentModel.DataAnnotations;

namespace Nettside.ViewModels
{
    public class LoginViewModel
    {
        

        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
