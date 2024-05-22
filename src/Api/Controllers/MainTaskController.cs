using Domain.Mappers;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MainTaskController : ControllerBase
{
    private readonly IMainTaskService _mainTaskService;

    public MainTaskController(IMainTaskService mainTaskService)
    {
        _mainTaskService = mainTaskService;
    }   

    [HttpGet("{userId}")]
    public IActionResult Get([FromRoute]int userId)
    {
        var mainTasks = _mainTaskService.Get(userId);
        return mainTasks is null ? NotFound() : Ok(mainTasks);
    }

    [HttpPost]
    public IActionResult Post([FromBody] RequisicaoMainTask requisicaoMainTask)
    {
        var novaMainTask = MapeadorMainTask.ParaClasse(requisicaoMainTask);
        _mainTaskService.Create(novaMainTask);
        return Created();
    }

    [HttpPut("{mainTaskId}")]
    public IActionResult Put(int mainTaskId, [FromBody] MainTaskUpdateRequest updatedDescription)
    {
        var mainTask = _mainTaskService.Find(mainTaskId);

        if (mainTask is null)
            return NotFound($"MainTask não encontrada.");

        mainTask.Descricao = updatedDescription.Description;

        if (mainTask.Id is not null)
            _mainTaskService.Update(mainTask, (int)mainTask.Id);

        return Ok(mainTask);
    }

    [HttpDelete("{mainTaskId}")]
    public IActionResult Delete(int mainTaskId)
    {
        var mainTask = _mainTaskService.Find(mainTaskId);
        if (mainTask is null)
            return NotFound($"MainTask não encontrada.");

        _mainTaskService.Delete(mainTaskId);

        return NoContent();
    }
}
