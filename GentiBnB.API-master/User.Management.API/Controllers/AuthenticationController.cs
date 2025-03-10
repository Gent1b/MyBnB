using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using User.Management.API.Models;
using User.Management.API.Models.Authentication.Login;
using User.Management.API.Models.Authentication.SignUp;
using User.Management.API.Services;
using User.Management.Service.Models;
using User.Management.Service.Services;

namespace User.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(UserManager<ApplicationUser> userManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
        {
            var result = await _authenticationService.RegisterUser(registerUser, role);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "This User Doesn't exist!",
                    Data = "This User Doesn't exist!"
                });
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Message = "Email Verified Successfully",
                    Data = "Email Verified Successfully"
                });
            }
            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                Message = "Error verifying email.",
                Data = "Error occurred during email verification."
            });
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var loginResult = await _authenticationService.Login(loginModel);
            return StatusCode(loginResult.StatusCode, loginResult);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = WebUtility.UrlEncode(token);

                var reactAppBaseUrl = "http://localhost:3000";

                // Route to the React page that handles reset ppassword
                var resetPasswordRoute = "/reset-password";

                // Constructing the full URL
                var forgotPasswordLink = $"{reactAppBaseUrl}{resetPasswordRoute}?token={encodedToken}&email={user.Email}";

                var result = await _authenticationService.ForgotPassword(email, forgotPasswordLink);
                return StatusCode(result.StatusCode, result);
            }

            return BadRequest(new ApiResponse<string>
            {
                Success = false,
                StatusCode = 400,
                Message = "User not found.",
                Data = "User not found."
            });
        }





        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return Ok(new ApiResponse<ResetPassword>
            {
                Success = true,
                StatusCode = 200,
                Message = "Reset Password",
                Data = model
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var result = await _authenticationService.ResetPassword(resetPassword.Email, resetPassword.Token, resetPassword.Password);
            return StatusCode(result.StatusCode, result);
        }


    }
}
