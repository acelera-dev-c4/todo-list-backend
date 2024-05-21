using Domain.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("Lista")]
    public IActionResult List()
    {
        var users = _usuarioService.List();
        return Ok(users);
    }

    [HttpPost("Cadastro")]
    public IActionResult Post([FromBody] UserRequest usuario)
    {
        var newUser = _usuarioService.Create(usuario);
        return Ok(newUser);
    }

    [HttpPut("Atualizar/{idUsuario}")]
    public IActionResult Put([FromBody] UpdatedUserRequest user, int idUsuario)
    {
        user.Id = idUsuario;
        var updatedUser = _usuarioService.Update(user);
        return Ok(updatedUser);
    }
}
