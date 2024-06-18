
namespace Infra;
public class NotificationHttpClient
{
    private readonly HttpClient httpClient;

    public NotificationHttpClient()
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7056/");
    }

    public async Task<HttpResponseMessage> DeleteSubscription(int subscriptionId)
    {
        var conString = $"{httpClient.BaseAddress}Subscription?subscriptionId={subscriptionId}";
        var response = await httpClient.DeleteAsync(conString);
        return response;
    }
}
