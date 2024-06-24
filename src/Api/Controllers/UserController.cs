using Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
[EnableCors("AllowAllHeaders")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var users = await _userService.List();
        return Ok(users);
    }

    [HttpPost]
    [AllowAnonymous] //Acesso anônimo
    public async Task<IActionResult> Post([FromBody] UserRequest user)
    {
        var newUser = await _userService.Create(user);
        return Ok(newUser);
    }


    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UserUpdate user)
    {
        var updatedUser = await _userService.Update(user);
        return Ok(updatedUser);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int userId)
    {
        await _userService.Delete(userId);
        return NoContent();
    }
}