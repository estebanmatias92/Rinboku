using System.ComponentModel.DataAnnotations;

namespace Rinboku.Models
{
    public class User
    {
        public string Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum Length is 2")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
