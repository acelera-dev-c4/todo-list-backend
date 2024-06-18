using Domain.Models;
using Infra.Repositories;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Text.Json;
using Infra;

namespace Api.Controllers;
[ApiController]
//[Authorize]
[Route("/webhooks")]
public class Webhooks : Controller
{
    NotificationHttpClient _notificationHttpClient = new();

    private readonly IMainTaskService _mainTaskService;

    public Webhooks(IMainTaskService mainTaskService)
    {
        _mainTaskService = mainTaskService;
    }

    [HttpPost("deleteSubscription")]
    public async Task<IActionResult> FinishSubscription(int subscriptionId)
    {
        var response = await _notificationHttpClient.DeleteSubscription(subscriptionId);

        return response.IsSuccessStatusCode ? Ok(response) : StatusCode((int)response.StatusCode, $"Failed to delete subscription. Status code: {response.StatusCode}");
    }

    [HttpPost("advise")]
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
}
