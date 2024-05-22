using Application.Services;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] AuthRequest request)
    {
        var response = await _authService.SignIn(request);
        if (response != null && response.Token != null)
        {
            return Ok(new { token = response.Token });
        }
        else
        {
            return Unauthorized();
        }
    }

}
