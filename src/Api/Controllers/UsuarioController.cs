using AceleraDevTodoListApi.DB;
using Domain.Entitys;
using Domain.Mappers;
using Domain.Request;
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
        return Ok(_context.Usuarios.ToList());
    }

    [HttpPost("Cadastro")]
    public IActionResult Post([FromBody] Usuario usuario)
    {
        var newUser = _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return Ok(newUser);
    }
}
