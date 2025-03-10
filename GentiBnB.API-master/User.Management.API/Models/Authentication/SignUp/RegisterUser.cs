using System.ComponentModel.DataAnnotations;

namespace User.Management.API.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string? Country { get; set; }
        public string? ProfilePictureUrl { get; set; }


    }
}
