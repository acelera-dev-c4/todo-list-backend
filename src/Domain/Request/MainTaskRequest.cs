namespace Domain.Request;

public class MainTaskRequest
{
    public int UserId { get; set; }
    public string? Description { get; set; }
}

public class MainTaskUpdate
{
    public string? Description { get; set; }
}