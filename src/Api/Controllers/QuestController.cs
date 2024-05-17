using AceleraDevTodoListApi.DB;
using Domain.Mappers;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestController : ControllerBase
{
    private readonly MyDBContext _context;

    public QuestController(MyDBContext context)
    {
        _context = context;
    }

    [HttpGet("Lista")]
    public IActionResult Get()
    {
        try
        {
            return Ok(_context.Quests);
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
            var quest = _context.Quests.Where(x => x.UserId == userId).FirstOrDefault();
            return quest is null ? NotFound() : Ok(quest);
        }
        catch (Exception ex)
        {
            return BadRequest($"Falha ao mostrar a Tarefa. {ex.Message}");
        }

    }

    [HttpPost("Criacao")]
    public IActionResult Post(QuestRequest taskRequest)
    {
        try
        {
            var newTask = QuestMapper.ToClass(taskRequest);
            _context.Quests.Add(newTask);
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
            var tarefa = _context.Quests.Find(Id);
            if (tarefa is null)
                return NotFound($"Tarefa não encontrada.");

            tarefa.Description = updateDescription;

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
            var tarefa = _context.Quests.Find(Id);
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
