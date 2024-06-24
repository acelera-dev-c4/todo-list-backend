namespace Domain.Models;

public class MainTask
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public string? Description { get; set; }
    public bool Completed { get; set; }
    public string UrlNotificationWebhook { get; set; } = "";
}
