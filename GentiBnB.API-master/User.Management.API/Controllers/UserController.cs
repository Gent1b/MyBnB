using Microsoft.AspNetCore.Mvc;
using User.Management.API.Models.DTO;
using User.Management.API.Services;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("by-id/{userId}")]
    public async Task<ActionResult> GetUserById(string userId)
    {
        var response = await _userService.GetUserById(userId);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("by-username/{userName}")]
    public async Task<ActionResult> GetUserByUserName(string userName)
    {
        var response = await _userService.GetUserByUserName(userName);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult> UpdateUser(string userId, UserDTO updatedUserData)
    {
        var response = await _userService.UpdateUser(userId, updatedUserData);
        return StatusCode(response.StatusCode, response);
    }
}
