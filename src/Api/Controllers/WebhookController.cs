using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers;
[ApiController]
//[Authorize]
[Route("/webhooks")]
public class Webhooks : Controller
{
    WebhooksService _webhookService = new();

    [HttpPost("deleteSubscription")] 
    public async Task<IActionResult> FinishSubscription(int subscriptionId)
    {
        var response = await _webhookService.DeleteSubscription(subscriptionId);
        
        return response.IsSuccessStatusCode ? Ok(response) : StatusCode((int)response.StatusCode, $"Failed to delete subscription. Status code: {response.StatusCode}");
    }
}
