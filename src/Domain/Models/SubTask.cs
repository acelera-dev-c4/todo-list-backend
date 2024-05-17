namespace Domain.Models;
public class SubTask
{
    public int Id { get; set; }
    public int MainTaskId { get; set; }
    public string? Description { get; set; }
    public bool Finished { get; set; } = false;
}
