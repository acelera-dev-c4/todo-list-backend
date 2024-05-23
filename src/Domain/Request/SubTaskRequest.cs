namespace Domain.Models;
public class SubTaskRequest
{
    public int MainTaskId { get; set; }
    public string? Description { get; set; }
    public bool Finished { get; set; } = false;
}

public class SubTaskUpdate
{
    public string? Description { get; set; }
    public bool Finished { get; set; }
}