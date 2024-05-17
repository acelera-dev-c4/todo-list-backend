using AceleraDevTodoListApi.DB;
using Domain.Mappers;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly MyDBContext _context;

    public TaskController(MyDBContext context)
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

    [HttpGet("{id}")]
    public IActionResult Get(int userId)
    {
        try
        {
            var task = _context.Tarefas.Where(x => x.IdUsuario == userId).FirstOrDefault();
            return task is null ? NotFound() : Ok(task);
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha ao mostrar a Tarefa. {ex.Message}");
        }

    }

    [HttpPost("Criacao")]
    public IActionResult Post(TaskRequest taskRequest)
    {
        try
        {
            var newTask = TaskMapper.ToClass(taskRequest);
            _context.Tarefas.Add(newTask);
            _context.SaveChanges();
            return Ok(newTask);
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
            var task = _context.Tarefas.Find(Id);
            if (task is null)
                return NotFound($"Tarefa não encontrada.");

            task.Descricao = updateDescription;

            _context.Update(task);
            _context.SaveChanges();

            return Ok(task);
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
            var task = _context.Tarefas.Find(Id);
            if (task is null)
                return NotFound($"Tarefa não encontrada.");

            _context.Remove(task);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha ao deletar tarefa. {ex.Message}");
        }
    }
}
