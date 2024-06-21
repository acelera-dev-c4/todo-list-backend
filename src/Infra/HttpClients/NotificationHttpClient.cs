using Domain.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Infra;
public class NotificationHttpClient
{
    private readonly HttpClient _httpClient;
    public NotificationHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("notificationClient");
    }

    private void SetAuthenticationHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<HttpResponseMessage> DeleteSubscription(int subscriptionId)
    {
        var conString = $"{_httpClient.BaseAddress}Subscription?subscriptionId={subscriptionId}";
        var response = await _httpClient.DeleteAsync(conString);
        return response;
    }


    public async Task<HttpResponseMessage> CreateNotification(string token, int subscriptionId, string message, bool readed, int userId)
    {
        var payload = new
        {
            subscriptionId,
            message,
            readed,
            userId
        };

        SetAuthenticationHeader(token);

        string jsonPayload = System.Text.Json.JsonSerializer.Serialize(payload);
        HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync($"{_httpClient.BaseAddress}Notification", content);
        result.EnsureSuccessStatusCode();
        return result;
    }

    public async Task<HttpResponseMessage> GetSubscriptionsBySubTaskId(int subTaskId, string token)
    {
        SetAuthenticationHeader(token);

        return await _httpClient.GetAsync($"{_httpClient.BaseAddress}Subscription/SubTaskId?subtaskId={subTaskId}");
    }

    public async Task<List<Subscription>> GetSubscriptionsByMainTaskId(int mainTaskId, string token)
    {
        SetAuthenticationHeader(token);

        var response = _httpClient.GetAsync($"{_httpClient.BaseAddress}Subscription/MainTaskId?maintaskId={mainTaskId}");

        response.Result.EnsureSuccessStatusCode();
        var jsonString = await response.Result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Subscription>>(jsonString)!;
    }
}
