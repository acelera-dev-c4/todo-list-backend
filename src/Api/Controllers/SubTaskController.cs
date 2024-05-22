using Domain.Models;
using Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubTaskController : Controller
{
    private readonly ISubTaskService _mainTaskService;

    public SubTaskController(ISubTaskService mainTaskService)
    {
        _mainTaskService = mainTaskService;
    }

    [HttpGet("{mainTaskId}")]
    public IActionResult Get([FromRoute] int mainTaskId)
    {
        var mainTasks = _mainTaskService.List(mainTaskId);
        return mainTasks is null ? NotFound() : Ok(mainTasks);
    }

    [HttpPost]
    public IActionResult Post([FromBody] SubTask novaSubTask)
    {
        _mainTaskService.Create(novaSubTask);
        return Ok(novaSubTask);
    }

    [HttpPut("{subTaskId}")]
    public IActionResult Put([FromRoute] int subTaskId, [FromBody] SubTask updatedSubTask)
        {
        _mainTaskService.Update(updatedSubTask, subTaskId);

        return Ok(updatedSubTask);
    }

    [HttpDelete("{subTaskId}")]
    public IActionResult Delete([FromRoute] int subTaskId)
    {
        _mainTaskService.Delete(subTaskId);

        return NoContent();
    }
}
