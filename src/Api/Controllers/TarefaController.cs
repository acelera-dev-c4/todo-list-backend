using AceleraDevTodoListApi.DB;
using Domain.Mappers;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefaController : ControllerBase
{
    private readonly MyDBContext _context;

    public TarefaController(MyDBContext context)
    {
        _context = context;
    }

    [HttpPost("Criacao")]
    public IActionResult Post(RequisicaoTarefa requisicaoTarefa)
    {
        try
        {
            var novaTarefa = MapeadorTarefa.ParaClasse(requisicaoTarefa);
            _context.Tarefas.Add(novaTarefa);
            _context.SaveChanges();
            return Ok(novaTarefa);
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha na criação da tarefa. {ex.Message}");
        }
    }
    [HttpGet("Lista")]
    public IActionResult Get()
    {
        try
        {
            return Ok(_context.Tarefas);
        }
        catch (Exception ex)
        {
            return NotFound("Nenhuma tarefa encontrada");
        }
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var tarefa = _context.Tarefas.Where(x => x.Id == id);
        return tarefa is null ? NotFound() : Ok(tarefa);
    }
}
