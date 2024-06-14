namespace Domain.Models;
public class Notifications
{
    public int? Id { get; set; }
    public int SubscriptionId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool Readed { get; set; }
}
