using User.Management.API.Models;
using User.Management.API.Models.DTO;
using System.Collections.Generic;

namespace User.Management.API.Services
{
    public interface IUserService
    {
        Task<ApiResponse<UserDTO>> GetUserById(string userId);
        Task<ApiResponse<UserDTO>> GetUserByUserName(string userName);

        Task<ApiResponse<UserDTO>> UpdateUser(string userId, UserDTO updatedUserData);
        Task<ApiResponse<UserDTO>> DeleteUserById(string userId);
        Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsers();
    }
}
