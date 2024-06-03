using Domain.Models;

namespace Domain.Responses;

public class JwtResponse
{
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
}

public class AuthResponse
{
    public JwtResponse? Jwt { get; set; }
    public UserResponse? User { get; set; }
}
