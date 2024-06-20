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
    public async Task<IActionResult> Get([FromRoute] int mainTaskId)
    {
        var mainTasks = await _subTaskService.List(mainTaskId);
        return mainTasks is null ? NotFound() : Ok(mainTasks);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SubTaskRequest newSubTask)
    {
        await _subTaskService.Create(newSubTask);
        return Ok(newSubTask);
    }

    [HttpPut("{subTaskId}")]
    public async Task<IActionResult> Put([FromRoute] int subTaskId, [FromBody] SubTaskUpdate updateSubTask)
    {
        var updatedSubTask = await _subTaskService.Update(updateSubTask, subTaskId);
        return Ok(updatedSubTask);
    }

    [HttpDelete("{subTaskId}")]
    public async Task<IActionResult> Delete([FromRoute] int subTaskId)
    {
        await _subTaskService.Delete(subTaskId);
        return NoContent();
    }
}
