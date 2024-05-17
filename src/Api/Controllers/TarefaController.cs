using AceleraDevTodoListApi.DB;
using Domain.Entitys;
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
        try
        {
            return Ok(_context.Tarefas);
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha ao mostrar as Tarefas. {ex.Message}");
        }
    }

    [HttpGet("Id")]
    public IActionResult Get(int idUsuario)
    {
        try
        {
            var tarefa = _context.Tarefas.Where(x => x.IdUsuario == idUsuario).ToList();
            return tarefa is null ? NotFound() : Ok(tarefa);
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha ao mostrar a Tarefa. {ex.Message}");
        }

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

    [HttpPut("Update/{Id}")]
    public IActionResult Put(int Id, [FromBody]string updateDescription)
    {
        try
        {
            var tarefa = _context.Tarefas.Find(Id);
            if (tarefa is null)
                return NotFound($"Tarefa não encontrada.");

            tarefa.Descricao = updateDescription;

            _context.Update(tarefa);
            _context.SaveChanges();

            return Ok(tarefa);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(int Id)
    {
        try
        {
            var tarefa = _context.Tarefas.Find(Id);
            if (tarefa is null)
                return NotFound($"Tarefa não encontrada.");

            _context.Remove(tarefa);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha ao deletar tarefa. {ex.Message}");
        }
    }
}
