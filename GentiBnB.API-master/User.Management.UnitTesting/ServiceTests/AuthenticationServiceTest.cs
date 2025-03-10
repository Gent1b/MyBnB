using Moq;
using Microsoft.AspNetCore.Identity;
using User.Management.API.Services;
using User.Management.API.Models.Authentication.Login;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using User.Management.API.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using User.Management.API.Models.DTO;
using User.Management.Service.Services;

namespace User.Management.UnitTesting.ServiceTests
{
    public class AuthenticationServiceTest
    {
        private readonly Mock<UserManager<ApplicationUser>> userManagerMock;
        private readonly Mock<RoleManager<IdentityRole>> roleManagerMock;
        private readonly Mock<IConfiguration> configurationMock;
        private readonly Mock<IEmailService> emailServiceMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly AuthenticationService authService;

        public AuthenticationServiceTest()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            roleManagerMock = new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);
            configurationMock = new Mock<IConfiguration>();
            emailServiceMock = new Mock<IEmailService>();
            mapperMock = new Mock<IMapper>();

            authService = new AuthenticationService(
                userManagerMock.Object,
                roleManagerMock.Object,
                configurationMock.Object,
                emailServiceMock.Object,
                mapperMock.Object
            );
        }

        [Fact]
        public async Task Login_ReturnsSuccess_WhenCredentialsAreValid()
        {
            var loginModel = new LoginModel { Username = "testUser", Password = "Test@123" };
            var user = new ApplicationUser { UserName = loginModel.Username, Email = "testUser@example.com" };
            var userRoles = new List<string> { "User" };

            userManagerMock.Setup(x => x.FindByNameAsync(loginModel.Username)).ReturnsAsync(user);
            userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginModel.Password)).ReturnsAsync(true);
            userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(userRoles);
            userManagerMock.Setup(x => x.IsEmailConfirmedAsync(user)).ReturnsAsync(true); // Assuming the email is confirmed for the test
            configurationMock.SetupGet(x => x["JWT:Secret"]).Returns("veryLongSecretKeyHereToMakeSureItsOver128Bits");
            configurationMock.SetupGet(x => x["JWT:ValidIssuer"]).Returns("testIssuer");
            configurationMock.SetupGet(x => x["JWT:ValidAudience"]).Returns("testAudience");

            mapperMock.Setup(m => m.Map<UserDTO>(It.IsAny<ApplicationUser>()))
                .Returns(new UserDTO { UserName = user.UserName });

            var result = await authService.Login(loginModel);

            Assert.True(result.Success);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal("Login Successful", result.Message);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(result.Data.Token);

            // Check for expected claims
            Assert.Equal(loginModel.Username, jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value);
            Assert.Equal(userRoles[0], jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value);
        }
        [Fact]
        public async Task CheckExistingUser_ReturnsSuccess_WhenUserDoesNotExist()
        {
            var email = "newuser@example.com";
            var username = "newuser";
            userManagerMock.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync((ApplicationUser)null);
            userManagerMock.Setup(x => x.FindByNameAsync(username)).ReturnsAsync((ApplicationUser)null);

            var result = await authService.CheckExistingUser(email, username);

            Assert.True(result.Success);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal("No conflicts", result.Message);
        }

        [Fact]
        public async Task CheckExistingUser_ReturnsFailure_WhenEmailExists()
        {
            var email = "existinguser@example.com";
            var username = "newuser";
            var existingUser = new ApplicationUser { Email = email };
            userManagerMock.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(existingUser);
            userManagerMock.Setup(x => x.FindByNameAsync(username)).ReturnsAsync((ApplicationUser)null);

            var result = await authService.CheckExistingUser(email, username);

            Assert.False(result.Success);
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
            Assert.Equal("Email already exists", result.Message);
        }

        [Fact]
        public async Task CheckExistingUser_ReturnsFailure_WhenUsernameExists()
        {
            var email = "newuser@example.com";
            var username = "existinguser";
            var existingUser = new ApplicationUser { UserName = username };
            userManagerMock.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync((ApplicationUser)null);
            userManagerMock.Setup(x => x.FindByNameAsync(username)).ReturnsAsync(existingUser);

            var result = await authService.CheckExistingUser(email, username);

            Assert.False(result.Success);
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
            Assert.Equal("Username already exists", result.Message);
        }


    }
}
