using Newtonsoft.Json;
using System.Text;

#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8618

namespace mini_chat
{
    public class ClaudeClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string ApiBaseUrl = "https://api.anthropic.com/v1/messages";

        public ClaudeClient(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
        }

        public async Task<string> SendMessageAsync(string prompt, string systemMessage, string model = "claude-3-7-sonnet-20250219", int maxTokens = 1000)
        {
            var requestBody = new
            {
                model = model,
                max_tokens = maxTokens,
                system = systemMessage,
                messages = new[]
                    { new { role = "user", content = prompt } }
            };

            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(ApiBaseUrl, content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

            return responseObj.content[0].text;
        }
    }
}
#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8618
