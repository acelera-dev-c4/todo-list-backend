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

    [HttpPost("Cadastro")]
    public IActionResult Post([FromBody] Usuario usuario)
    {
        try
        {
            var newUser = _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Ok(newUser);
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha no cadastro. {ex.Message}");
        }
        
    }
}
