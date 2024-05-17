using AceleraDevTodoListApi.DB;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly MyDBContext _context;

    public UserController(MyDBContext context)
    {
        _context = context;
    }

    [HttpGet("List")]
    public IActionResult List()
    {
        return Ok(_context.Users.ToList());
    }

    [HttpPost("Register")]
    public IActionResult Post([FromBody]User user)
    {
        try
        {
            var newUser = _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(newUser);
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha no cadastro. {ex.Message}");
        }
    }
}
