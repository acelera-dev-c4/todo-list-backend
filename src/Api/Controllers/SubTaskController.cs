using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
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
        _subTaskService.Create(newSubTask);
        return Ok(newSubTask);
    }

    [HttpPut("{subTaskId}")]
    public IActionResult Put([FromRoute] int subTaskId, [FromBody] SubTaskUpdate updateSubTask)
    {
        try
        {
            var updatedSubTask = _subTaskService.Update(updateSubTask, subTaskId);
            return Ok(updatedSubTask);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpDelete("{subTaskId}")]
    public IActionResult Delete([FromRoute] int subTaskId)
    {
        try
        {
            _subTaskService.Delete(subTaskId);
            return NoContent();
        } 
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
