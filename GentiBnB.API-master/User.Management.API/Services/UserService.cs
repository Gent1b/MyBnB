using User.Management.API.Models;
using User.Management.API.Models.DTO;
using User.Management.API.Repositories;

namespace User.Management.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<UserDTO>> DeleteUserById(string userId)
        {
            try
            {
                var user = await _userRepository.DeleteUserById(userId);
                if (user != null)
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = true,
                        StatusCode = StatusCodes.Status200OK,
                        Message = "User deleted successfully",
                        Data = user
                    };
                }
                else
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "User not found",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserDTO>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error deleting user",
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse<UserDTO>> GetUserById(string userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                if (user != null)
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = true,
                        StatusCode = StatusCodes.Status200OK,
                        Message = "User retrieved successfully",
                        Data = user
                    };
                }
                else
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "User not found",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserDTO>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving user",
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse<UserDTO>> GetUserByUserName(string userName)
        {
            try
            {
                var user = await _userRepository.GetUserByUserName(userName);
                if (user != null)
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = true,
                        StatusCode = StatusCodes.Status200OK,
                        Message = "User retrieved successfully",
                        Data = user
                    };
                }
                else
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "User not found",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserDTO>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving user",
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse<UserDTO>> UpdateUser(string userId, UserDTO updatedUserData)
        {
            try
            {
                var user = await _userRepository.UpdateUser(userId, updatedUserData);
                if (user != null)
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = true,
                        StatusCode = StatusCodes.Status200OK,
                        Message = "User updated successfully",
                        Data = user
                    };
                }
                else
                {
                    return new ApiResponse<UserDTO>
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "User not found",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserDTO>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error updating user",
                    Error = ex.Message
                };
            }
        }

        public async Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                return new ApiResponse<IEnumerable<UserDTO>>
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Users retrieved successfully",
                    Data = users
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<UserDTO>>
                {
                    Success = false,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error retrieving users",
                    Error = ex.Message
                };
            }
        }
    }
}
