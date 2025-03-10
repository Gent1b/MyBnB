using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using User.Management.API.Models;
using User.Management.API.Models.Authentication.Login;
using User.Management.API.Models.Authentication.SignUp;
using User.Management.API.Models.DTO;

namespace User.Management.API.Services
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<string>> RegisterUser(RegisterUser registerUser, string role);
        Task<ApiResponse<LoginResponse>> Login(LoginModel loginModel);
        Task<ApiResponse<string>> ForgotPassword(string email, string forgotPasswordLink);
        Task<ApiResponse<string>> ResetPassword(string email, string token, string newPassword);


    }
}
