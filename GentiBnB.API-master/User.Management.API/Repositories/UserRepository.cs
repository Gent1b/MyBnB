using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using User.Management.API.Models;
using User.Management.API.Models.DTO;

namespace User.Management.API.Repositories
{
    public class UserRepository :IUserRepository 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> DeleteUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUser(string userId, UserDTO updatedUserData)
        {
            var user =  await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.FullName = updatedUserData.FullName;
                user.Country = updatedUserData.Country;
                user.ProfilePictureUrl = updatedUserData.ProfilePictureUrl;
                await _userManager.UpdateAsync(user);

            }
            return _mapper.Map<UserDTO>(user);

        }
    }
}
