using Domain.Request;
using Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Text.Json;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
[EnableCors("AllowAllHeaders")]
public class MainTaskController : ControllerBase
{
    private readonly IMainTaskService _mainTaskService;
    NotificationHttpClient _notificationHttpClient = new();

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

    [HttpGet("search")]
    public IActionResult Get([FromQuery] int? MainTaskId,
                             [FromQuery] string? UserName,
                             [FromQuery] string? MainTaskDescription)
    {
        if (MainTaskId == null && string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(MainTaskDescription))
            return BadRequest("At least one parameter is required (MainTaskId, UserName, MainTaskDescription)");

        var mainTasks = _mainTaskService.SearchByParams(MainTaskId, UserName, MainTaskDescription);
        return mainTasks is null ? NotFound() : Ok(mainTasks);
    }

    [HttpPost]
    public IActionResult Post([FromBody] MainTaskRequest mainTaskRequest)
    {
        var newMainTask = _mainTaskService.Create(mainTaskRequest);
        return Ok(newMainTask);
    }

    [HttpPost("SetUrlWebhook")]
    public async Task<IActionResult> AdviseSubscriptionToMainTask([FromBody] JsonElement content)
    {
        string url = content.GetProperty("url").GetString()!;
        int mainTaskId = content.GetProperty("mainTaskId").GetInt32();

        try
        {
            await _mainTaskService.NotifyWithUrl(mainTaskId, url);
            return Ok("Url set for future notification");
        }
        catch (Exception e)
        {
            return BadRequest($"Cannot update url on main task id:{mainTaskId}. Exception message: {e.Message}");
        }
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

    [HttpPost("deleteSubscription")]
    public async Task<IActionResult> FinishSubscription(int subscriptionId)
    {
        var response = await _notificationHttpClient.DeleteSubscription(subscriptionId);

        return response.IsSuccessStatusCode ? Ok(response) : StatusCode((int)response.StatusCode, $"Failed to delete subscription. Status code: {response.StatusCode}");
    }
}
