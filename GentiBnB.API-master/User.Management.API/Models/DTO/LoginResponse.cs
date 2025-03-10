namespace User.Management.API.Models.DTO
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool EmailVerified { get; set; }
        public UserDTO User { get; set; }
    }

}
