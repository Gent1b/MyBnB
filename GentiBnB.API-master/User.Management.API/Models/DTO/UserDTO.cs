namespace User.Management.API.Models.DTO
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string Country { get; set; }
        public string? ProfilePictureUrl { get; set; }
        // Add other properties as needed
    }

}
