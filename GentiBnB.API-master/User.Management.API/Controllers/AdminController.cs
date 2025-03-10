using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Management.API.Models;
using User.Management.API.Services;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserService _userService;

    public AdminController(UserManager<ApplicationUser> userManager,IUserService userService)
    {
        _userManager = userManager;
        _userService = userService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> ListUsers()
    {
        var response = await _userService.GetAllUsers();
        return StatusCode(response.StatusCode, response);
    }


    [HttpDelete("delete-user/{userId}")]
    public async Task<ActionResult> DeleteUser(string userId)
    {
        var response = await _userService.DeleteUserById(userId);
        return StatusCode(response.StatusCode, response);
    }
}
