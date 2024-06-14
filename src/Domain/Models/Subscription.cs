namespace Domain.Models;
public class Subscription
{
    public int? Id { get; set; }
    public int SubTaskIdSubscriber { get; set; }
    public int MainTaskIdTopic { get; set; }
}
