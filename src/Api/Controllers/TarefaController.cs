using Infra.DB;
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

    [HttpGet("Lista")]
    public IActionResult Get()
    {
        return Ok(_context.Tarefas);
    }

    [HttpGet("IdUsuario")]
    public IActionResult Get(int idUsuario)
    {
        var tarefa = _context.Tarefas.Where(x => x.IdUsuario == idUsuario).ToList();
        return tarefa is null ? NotFound() : Ok(tarefa);
    }

    [HttpPost("Criacao")]
    public IActionResult Post(RequisicaoTarefa requisicaoTarefa)
    {
        var novaTarefa = MapeadorTarefa.ParaClasse(requisicaoTarefa);
        _context.Tarefas.Add(novaTarefa);
        _context.SaveChanges();
        return Ok(novaTarefa);
    }

    [HttpPut("Update/{Id}")]
    public IActionResult Put(int Id, [FromBody] TarefaUpdateRequest updateDescription)
    {
        var tarefa = _context.Tarefas.Find(Id);
        if (tarefa is null)
            return NotFound($"Tarefa não encontrada.");

        tarefa.Descricao = updateDescription.Description;

        _context.Update(tarefa);
        _context.SaveChanges();

        return Ok(tarefa);
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(int Id)
    {
        var tarefa = _context.Tarefas.Find(Id);
        if (tarefa is null)
            return NotFound($"Tarefa não encontrada.");

        _context.Remove(tarefa);
        _context.SaveChanges();

        return NoContent();
    }
}
