using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;



namespace Infra;
    public class NotificationHttpClient
    {
        private readonly HttpClient _httpClient;

        public NotificationHttpClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7056/"); 
        }

        public async Task<bool> CheckSubtaskSubscriptionAsync(string subtaskId)
        {
            var url = $"subscriptions/{subtaskId}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
              
                return content.Contains("assinada", StringComparison.OrdinalIgnoreCase); 
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ArgumentException("Subtask ID inválido.");
            }
            else
            {
                throw new HttpRequestException($"Erro ao acessar a API de Notification. Status code: {response.StatusCode}");
            }
        }
    }

