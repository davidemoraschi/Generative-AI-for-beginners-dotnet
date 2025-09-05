using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8618

namespace mini_chat
{
    public class GrokClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl = "https://api.groq.com/openai/v1/chat/completions";

        public GrokClient(string apiKey)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> SendMessageAsync(string systemMessage, string userMessage)
        {
            try
            {
                var request = new
                {
                    model = "grok-1",
                    messages = new[]
                    {
                        new { role = "system", content = systemMessage },
                        new { role = "user", content = userMessage }
                    }
                };

                var content = new StringContent(
                    JsonSerializer.Serialize(request),
                    Encoding.UTF8,
                    "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(_apiUrl, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                using JsonDocument document = JsonDocument.Parse(responseBody);

                // Extract the assistant's response message
                JsonElement choices = document.RootElement.GetProperty("choices");
                if (choices.GetArrayLength() > 0)
                {
                    JsonElement firstChoice = choices[0];
                    JsonElement message = firstChoice.GetProperty("message");
                    string assistantResponse = message.GetProperty("content").GetString();
                    return assistantResponse;
                }

                return "No response from Grok API";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8618
