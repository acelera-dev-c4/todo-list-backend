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

    [HttpPut("{userId}")]
    public IActionResult Put([FromRoute]int userId, [FromBody] UserUpdate userUpdate)
    {
        var updatedUser = _userService.Update(userUpdate, userId);
        return Ok(updatedUser);
    }

    [HttpDelete("{userId}")]
    public IActionResult Delete(int userId)
    {
        _userService.Delete(userId);
        return NoContent();
    }
}
