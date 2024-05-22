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

    [HttpGet("List")]
    public IActionResult List()
    {
        var users = _userService.List();
        return Ok(users);
    }

    [HttpPost("Register")]
    public IActionResult Post([FromBody] UserRequest user)
    {
        var newUser = _userService.Create(user);
        return Ok(newUser);
    }


    [HttpPut("Update/{userId}")]
    public IActionResult Put([FromBody] UpdatedUserRequest user, int userId)
    {
        user.Id = userId;
        var updatedUser = _userService.Update(user);
        return Ok(updatedUser);
    }

    [HttpDelete("Deletar/{idUsuario}")]
    public IActionResult Delete(int idUsuario)
    {
        _userService.Delete(idUsuario);
        return NoContent();
    }
}
