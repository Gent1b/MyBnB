using Microsoft.AspNetCore.Identity;

namespace User.Management.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Country { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public List<Stay> Stays { get; set; }

    }
}
