namespace Domain.Models;
public class SubQuest
{
    public int Id { get; set; }
    public int QuestId { get; set; }
    public string? Description { get; set; }
    public bool Finished { get; set; } = false;
}
