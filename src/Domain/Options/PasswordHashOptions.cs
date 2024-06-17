namespace Domain.Options;
public class PasswordHashOptions
{
    public int SaltByteSize { get; set; }
    public int HashByteSize { get; set; }
    public string HashAlgorithm { get; set; } = "";
    public int MinIteration { get; set; }
    public int MaxIteration { get; set; }
}
