using Domain.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class MainTaskController : ControllerBase
{
    private readonly IMainTaskService _mainTaskService;

    public MainTaskController(IMainTaskService mainTaskService)
    {
        _mainTaskService = mainTaskService;
    }

    [HttpGet("{userId}")]
    public IActionResult Get([FromRoute] int userId)
    {
        var mainTasks = _mainTaskService.Get(userId);
        return mainTasks is null ? NotFound() : Ok(mainTasks);
    }

    [HttpPost]
    public IActionResult Post([FromBody] MainTaskRequest mainTaskRequest)
    {
        var newMainTask = _mainTaskService.Create(mainTaskRequest);
        return Ok(newMainTask);
    }

    [HttpPut("{mainTaskId}")]
    public IActionResult Put(int mainTaskId, [FromBody] MainTaskUpdate updateMainTask)
    {
        var mainTask = _mainTaskService.Update(updateMainTask, mainTaskId);
        return Ok(mainTask);
    }

    [HttpDelete("{mainTaskId}")]
    public IActionResult Delete(int mainTaskId)
    {
        _mainTaskService.Delete(mainTaskId);
        return NoContent();
    }
}
