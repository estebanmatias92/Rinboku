using Microsoft.AspNetCore.Identity;

namespace Rinboku.Models
{
    public class AppUser : IdentityUser
    {
        public string Occupation { get; set; }
    }
}
