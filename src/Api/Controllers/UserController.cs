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

    [HttpGet("Lista")]
    public IActionResult List()
    {
        var users = _userService.List();
        return Ok(users);
    }

    [HttpPost("Cadastro")]
    public IActionResult Post([FromBody] UserRequest user)
    {
        var newUser = _userService.Create(user);
        return Ok(newUser);
    }
}
