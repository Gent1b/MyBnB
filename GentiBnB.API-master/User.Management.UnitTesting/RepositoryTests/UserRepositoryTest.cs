using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading.Tasks;
using User.Management.API.Models;
using User.Management.API.Models.DTO;
using User.Management.API.Repositories;
using Xunit;

namespace User.Management.UnitTesting.RepositoryTests
{
    [Collection("Non-Parallel Collection")]

    public class UserRepositoryTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            _mapperMock = new Mock<IMapper>();

            _userRepository = new UserRepository(_userManagerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task UpdateUser_ReturnsUpdatedUserDTO_WhenUserExists()
        {
            // Arrange
            var userId = "testUserId";
            //data to be updated
            var updatedUserData = new UserDTO
            {
                FullName = "Updated Name",
                Country = "Updated Country",
                ProfilePictureUrl = "Updated URL"
            };
            // data to be returned from the database
            var user = new ApplicationUser
            {
                Id = userId,
                FullName = "Original Name",
                Country = "Original Country",
                ProfilePictureUrl = "Original URL"
            };

            _userManagerMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            _mapperMock.Setup(m => m.Map<UserDTO>(It.IsAny<ApplicationUser>())).Returns(updatedUserData);

            var result = await _userRepository.UpdateUser(userId, updatedUserData);

            _userManagerMock.Verify(x => x.UpdateAsync(It.Is<ApplicationUser>(u => u.FullName == updatedUserData.FullName && u.Country == updatedUserData.Country && u.ProfilePictureUrl == updatedUserData.ProfilePictureUrl)), Times.Once);
            _mapperMock.Verify(m => m.Map<UserDTO>(It.IsAny<ApplicationUser>()), Times.Once);
            Assert.Equal(updatedUserData.FullName, result.FullName);
            Assert.Equal(updatedUserData.Country, result.Country);
            Assert.Equal(updatedUserData.ProfilePictureUrl, result.ProfilePictureUrl);
        }

    }
}
