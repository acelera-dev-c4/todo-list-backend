using Domain.Request;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult List()
    {
        var users = _userService.List();
        return Ok(users);
    }

    [HttpPost]
    public IActionResult Post([FromBody] UserRequest user)
    {
        var newUser = _userService.Create(user);
        return Ok(newUser);
    }

    [HttpPut("Update")]
    public IActionResult Put([FromBody] UpdateUserRequest user)
    {
        var updatedUser = _userService.Update(user);
        return Ok(updatedUser);
    }

    [HttpDelete("{idUsuario}")]
    public IActionResult Delete(int userId)
    {
        _userService.Delete(userId);
        return NoContent();
    }
}
