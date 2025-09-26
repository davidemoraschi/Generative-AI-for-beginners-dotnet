using System.Text;
using System.Text.Json;

#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8618

namespace mini_chat
{
    public class PerplexityClient
    {
        private readonly string _apiKey;
        private readonly string _baseUrl = "https://api.perplexity.ai/chat/completions";
        private readonly HttpClient _httpClient;

        public PerplexityClient(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey), "API Key cannot be null or empty.");
            }

            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        // Helper classes to deserialize the Perplexity API response
        private class PerplexityResponse
        {
            public Choice[] choices { get; set; }
        }

        private class Choice
        {
            public Message message { get; set; }
        }

        private class Message
        {
            public string content { get; set; }
        }

        public async Task<string> SendMessageAsync(string prompt)
        {
            try
            {
                // Prepare the request body (no system message, only user message)
                var requestBody = new
                {
                    model = "sonar-pro",
                    messages = new object[]
                    {
                        new { role = "user", content = prompt }
                    }
                };

                string requestUri = _baseUrl;
                string jsonBody = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                request.Content = content;
                request.Headers.Add("Authorization", $"Bearer {_apiKey}");

                var response = await _httpClient.SendAsync(request);

                // Check for errors
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error: {response.StatusCode} - {errorContent}");
                    return null;
                }

                // Read and return the response
                string responseContent = await response.Content.ReadAsStringAsync();

                PerplexityResponse perplexityResponse = JsonSerializer.Deserialize<PerplexityResponse>(responseContent);

                if (perplexityResponse?.choices != null
                    && perplexityResponse.choices.Length > 0
                    && perplexityResponse.choices[0]?.message?.content != null)
                {
                    return perplexityResponse.choices[0].message.content;
                }
                else
                {
                    MessageBox.Show($"Unexpected response structure: {responseContent}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"HTTP Request Exception: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"JSON Exception: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"General Exception: {ex.Message}");
                return null;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8618
