using AceleraDevTodoListApi.DB;
using Domain.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly MyDBContext _context;

    public UsuarioController(MyDBContext context)
    {
        _context = context;
    }

    [HttpGet("Lista")]
    public IActionResult List()
    {
        var users = _context.Usuarios
            .Select(u => new { u.Id, u.Nome, u.Email })
            .ToList();
        return Ok(users);
    }

    [HttpPost("Cadastro")]
    public IActionResult Post([FromBody]Usuario usuario)
    {
        try
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            var userResponse = new
            {
                Id = usuario.Id,
                Name = usuario.Nome,
                Email = usuario.Email,
            };
            return Ok(userResponse);
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha no cadastro. {ex.Message}");
        }
    }
}
