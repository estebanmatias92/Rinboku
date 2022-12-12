using System.ComponentModel.DataAnnotations;

namespace Rinboku.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required, MinLength(2, ErrorMessage = "Minimum Length is 2")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
