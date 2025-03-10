using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Management.API.Models;
using User.Management.API.Models.Authentication.Login;
using User.Management.API.Models.Authentication.SignUp;
using User.Management.API.Models.DTO;
using User.Management.Service.Models;
using User.Management.Service.Services;

namespace User.Management.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper; // Add IMapper

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
            _mapper = mapper; // Initialize IMapper
        }

        public async Task<ApiResponse<LoginResponse>> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    //add user claims to my token
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id) 


                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                //adding the email verified to the token aswell
                bool isEmailVerified = await _userManager.IsEmailConfirmedAsync(user);
                authClaims.Add(new Claim("EmailVerified", isEmailVerified.ToString()));
                // Add a custom claim to the JWT token to include the user role
                authClaims.Add(new Claim("UserRole", string.Join(",", userRoles)));


                var jwtToken = GetToken(authClaims);

                var userDto = _mapper.Map<UserDTO>(user);

                var loginResponse = new LoginResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    Expiration = jwtToken.ValidTo,
                    EmailVerified = isEmailVerified,
                    User = userDto
                };
                // Map ApplicationUser to UserDTO

                return new ApiResponse<LoginResponse>
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Login Successful",
                    Data = loginResponse
                };
            }
            else
            {
                // Return an unauthorized response if login failed
                return new ApiResponse<LoginResponse>
                {
                    Success = false,
                    StatusCode = 401,
                    Message = "Unauthorized",
                    Error = "Invalid username or password"
                };
            }
        }

        public async Task<ApiResponse<string>> RegisterUser(RegisterUser registerUser, string role)
        {
            var existingUserCheckResult = await CheckExistingUser(registerUser.Email, registerUser.UserName);
            if (!existingUserCheckResult.Success)
            {
                return existingUserCheckResult;
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    StatusCode = 400,
                    Message = "Role doesn't exist",
                    Error = "Specified role does not exist."
                };
            }

            // Map RegisterUser to ApplicationUser
            var user = _mapper.Map<ApplicationUser>(registerUser);
            // Additional property assignment, if needed
            user.EmailConfirmed = false;

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);

                var emailConfirmationResult = await SendEmailConfirmation(user);
                if (emailConfirmationResult.Success)
                {
                    return new ApiResponse<string>
                    {
                        Success = true,
                        StatusCode = 201,
                        Message = "User Created Successfully",
                        Data = "User successfully registered."
                    };
                }
            }

            return new ApiResponse<string>
            {
                Success = false,
                StatusCode = 500,
                Message = "Failed to Create User!",
                Error = string.Join(", ", result.Errors.Select(x => x.Description))
            };
        }


        public async Task<ApiResponse<string>> CheckExistingUser(string email, string userName)
        {
            try
            {
                var existingUserByEmail = await _userManager.FindByEmailAsync(email);
                var existingUserByUserName = await _userManager.FindByNameAsync(userName);

                if (existingUserByEmail != null || existingUserByUserName != null)
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        StatusCode = 409,
                        Message = existingUserByEmail != null ? "Email already exists" : "Username already exists",
                        Error = "Try Using a different email or username"
                    };
                }

                return new ApiResponse<string>
                {
                    Success = true, // No conflicts
                    StatusCode = 200,
                    Message = "No conflicts",
                    Data = "Email and username are available."
                };
            }
            catch (Exception ex)
            {
                // Handle the exception, log it, and return an appropriate response.
                return new ApiResponse<string>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = "An error occurred while checking for existing users",
                    Error = ex.Message // You can customize this error message.
                };
            }
        }

        public async Task<ApiResponse<string>> ForgotPassword(string email, string forgotPasswordLink)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var message = new Message(new string[] { user.Email }, "Forgot Password Link", forgotPasswordLink);
                _emailService.SendEmail(message);
                return new ApiResponse<string>
                {
                    Success = true,
                    StatusCode = 200,
                    Message = $"Password Change request has been sent to {user.Email}. Please check your email.",
                    Data = "Password reset link sent."
                };
            }
            return new ApiResponse<string>
            {
                Success = false,
                StatusCode = 400,
                Message = "Couldn't send a link to the email. Please try again.",
                Error = "Couldn't send a link to the email. Please try again."
            };
        }


        public async Task<ApiResponse<string>> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, token, newPassword);
                if (resetPassResult.Succeeded)
                {
                    return new ApiResponse<string>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "Password changed successfully",
                        Data = "Password updated successfully."
                    };
                }
                else
                {
                    var errors = resetPassResult.Errors.Select(error => error.Description).ToList();
                    return new ApiResponse<string>
                    {
                        Success = false,
                        StatusCode = 400,
                        Message = "Password change failed",
                        Error = string.Join(", ", errors) // Collate errors into a single string.
                    };
                }
            }
            return new ApiResponse<string>
            {
                Success = false,
                StatusCode = 400,
                Message = "Couldn't change the password. Please try again.",
                Error = "No user associated with the provided email."
            };
        }




        private async Task<ApiResponse<string>> SendEmailConfirmation(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var email = Uri.EscapeDataString(user.Email);
            var tokenEncoded = Uri.EscapeDataString(token);

            var confirmationLink = $"https://localhost:7266/api/Authentication/ConfirmEmail?token={tokenEncoded}&email={email}";
            var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);

            _emailService.SendEmail(message);

            return new ApiResponse<string>
            {
                Success = true,
                StatusCode = 200,
                Message = "Email confirmation sent",
                Data = "Email confirmation link sent successfully."
            };
        }




        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        public Task ResetPassword(ResetPassword resetPassword)
        {
            throw new NotImplementedException();
        }
    }
}
