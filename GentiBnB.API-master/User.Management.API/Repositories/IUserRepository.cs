using User.Management.API.Models.DTO;

namespace User.Management.API.Repositories
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUserById(string userId);
        Task<UserDTO> UpdateUser(string userId, UserDTO updatedUserData);
        Task<UserDTO> DeleteUserById(string userId);
        Task<UserDTO> GetUserByUserName(string userName);

        Task<IEnumerable<UserDTO>> GetAllUsers();

    }
}
