using Domain.Request;
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
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Infra.NotificationHttpClient _notificationHttpClient; // nao funcionou com DI

    public MainTaskController(IMainTaskService mainTaskService, IHttpClientFactory httpClientFactory)
    {
        _mainTaskService = mainTaskService;
        _httpClientFactory = httpClientFactory;
        _notificationHttpClient = new(_httpClientFactory);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get([FromRoute] int userId)
    {
        var mainTasks = await _mainTaskService.Get(userId);
        return mainTasks is null ? NotFound() : Ok(mainTasks);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Get([FromQuery] int? MainTaskId,
                             [FromQuery] string? UserName,
                             [FromQuery] string? MainTaskDescription)
    {
        if (MainTaskId == null && string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(MainTaskDescription))
            return BadRequest("At least one parameter is required (MainTaskId, UserName, MainTaskDescription)");

        var mainTasks = await _mainTaskService.SearchByParams(MainTaskId, UserName, MainTaskDescription);
        return mainTasks is null ? NotFound() : Ok(mainTasks);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MainTaskRequest mainTaskRequest)
    {
        var newMainTask = await _mainTaskService.Create(mainTaskRequest);
        return Ok(newMainTask);
    }

    [HttpPut("SetUrlWebhook")]
    public async Task<IActionResult> SetUrlWebhook([FromBody] JsonElement content)
    {
        int mainTaskId = content.GetProperty("mainTaskId").GetInt32();
        string url = content.GetProperty("url").GetString()!;

        try
        {
            await _mainTaskService.SetUrlWebhook(mainTaskId, url);
            return Ok("Url set for future notification");
        }
        catch (Exception e)
        {
            return BadRequest($"Cannot update url on main task id:{mainTaskId}. Exception message: {e.Message}");
        }
    }

    [HttpPut("{mainTaskId}")]
    public async Task<IActionResult> Put(int mainTaskId, [FromBody] MainTaskUpdate updateMainTask)
    {
        var mainTask = await _mainTaskService.Update(updateMainTask, mainTaskId);
        return Ok(mainTask);
    }

    [HttpDelete("{mainTaskId}")]
    public async Task<IActionResult> Delete(int mainTaskId)
    {
        await _mainTaskService.Delete(mainTaskId);
        return NoContent();
    }

    [HttpPost("deleteSubscription")]
    public async Task<IActionResult> FinishSubscription(int subscriptionId)
    {
        var response = await _notificationHttpClient.DeleteSubscription(subscriptionId);

        return response.IsSuccessStatusCode ? Ok(response) : StatusCode((int)response.StatusCode, $"Failed to delete subscription. Status code: {response.StatusCode}");
    }

    /// <summary>
    /// Updates field: UrlNotificationWebhook on all subscribed MainTasks in the database
    /// </summary>
    /// <param name="newUrl"></param>
    /// <returns></returns>
    [HttpPut("updateUrl")] // acesso apenas ao system user
    public async Task<IActionResult> UpdateUrl(string newUrl)
    {
        var result = await _mainTaskService.UpdateUrl(newUrl);
        return Ok(result);
    }
}

