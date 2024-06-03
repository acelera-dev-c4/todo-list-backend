using Service;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Web.Controllers;

[Route("[controller]")]
[ApiController]
[EnableCors("AllowAllHeaders")]
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
        if (response != null && response.Jwt != null)
        {
            return Ok(new
            {
                token = response.Jwt,
                userData = response.User
            });
        }
        else
        {
            return Unauthorized();
        }
    }

}
