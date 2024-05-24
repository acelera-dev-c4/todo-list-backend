namespace Domain.Options;

public class TokenOptions
{
    public static string Section = "Token";

    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public int ValidForMinutes { get; set; }
    public string? Key { get; set; }

    public DateTime AccessTokenExpiration { get => IssuedAt.AddMinutes(ValidForMinutes); }
    public TimeSpan ValidFor { get => AccessTokenExpiration - DateTime.UtcNow; }
    public DateTime IssuedAt => DateTime.UtcNow;
    public DateTime NotBefore => IssuedAt;
}
