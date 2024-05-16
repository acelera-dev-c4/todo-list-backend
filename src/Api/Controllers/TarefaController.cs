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

    [HttpDelete("Deletar")]
    public IActionResult Delete(int Id)
    {
        try
        {
            var tarefa = _context.Tarefas.Find(Id);
            if (tarefa is null)
                //return NotFound($"Tarefa com Id {Id} não encontrada.");
                return BadRequest();

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
