using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
[EnableCors("AllowAllHeaders")]
public class SubTaskController : Controller
{
    private readonly ISubTaskService _subTaskService;

    public SubTaskController(ISubTaskService subTaskService)
    {
        _subTaskService = subTaskService;
    }

    [HttpGet("{mainTaskId}")]
    public IActionResult Get([FromRoute] int mainTaskId)
    {
        var mainTasks = _subTaskService.List(mainTaskId);
        return mainTasks is null ? NotFound() : Ok(mainTasks);
    }

    [HttpPost]
    public IActionResult Post([FromBody] SubTaskRequest newSubTask)
    {
        var createdSubTask = _subTaskService.Create(newSubTask);
        return Ok(createdSubTask);
    }

    [HttpPut("{subTaskId}")]
    public async Task<IActionResult> Put([FromRoute] int subTaskId, [FromBody] SubTaskUpdate updateSubTask)
    {
        var updatedSubTask = await _subTaskService.Update(updateSubTask, subTaskId);
        return Ok(updatedSubTask);
    }

    [HttpPut("finished/{subTaskId}")]
    public async Task<IActionResult> Put([FromRoute] int subTaskId, [FromBody] UpdateSubtaskFinished finished)
    {
        await _subTaskService.UpdateSubtaskFinished(subTaskId, finished.Finished);
        return Ok(finished);
    }

    [HttpDelete("{subTaskId}")]
    public IActionResult Delete([FromRoute] int subTaskId)
    {
        _subTaskService.Delete(subTaskId);
        return NoContent();
    }
}
