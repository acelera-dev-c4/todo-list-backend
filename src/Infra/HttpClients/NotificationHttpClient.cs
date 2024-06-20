using Microsoft.Extensions.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Net.Http;

namespace Infra;
public class NotificationHttpClient
{
    private readonly HttpClient _httpClient;


    public NotificationHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("notificationClient");
    }

    public async Task<HttpResponseMessage> DeleteSubscription(int subscriptionId)
    {
        var conString = $"{_httpClient.BaseAddress}Subscription?subscriptionId={subscriptionId}";
        var response = await _httpClient.DeleteAsync(conString);
        return response;
    }

   
    public async Task<HttpResponseMessage> CreateNotification(int subscriptionId, string message, bool readed)
    {
        try
            {

            var payload = new
            {
                subscriptionId,
                message,
                readed
            };

            string jsonPayload = JsonSerializer.Serialize(payload);
            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync($"{_httpClient.BaseAddress}Notification", content);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null;

    }
}
