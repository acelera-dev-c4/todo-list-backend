namespace Domain.Request;
public class UserRequest
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
}
public class UpdatedUserRequest : UserRequest
{
    public int? Id { get; set; }
}
