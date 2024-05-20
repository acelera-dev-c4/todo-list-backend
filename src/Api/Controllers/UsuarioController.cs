using Services;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _context;

    public UsuarioController(IUsuarioService context)
    {
        _context = context;
    }

    [HttpGet("Lista")]
    public IActionResult List()
    {
        var users = _context.List();
        return Ok(users);
    }

    [HttpPost("Cadastro")]
    public IActionResult Post([FromBody] UserRequest usuario)
    {
        var newUser = _context.Create(usuario);
        return Ok(newUser);
    }
}
