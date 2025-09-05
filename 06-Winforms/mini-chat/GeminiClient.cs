using System.Text;
using System.Text.Json;

#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8618

namespace mini_chat
{
    public class GeminiClient
    {
        private readonly string _apiKey;
        private readonly string _baseUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-pro:generateContent";
        private readonly HttpClient _httpClient;

        public GeminiClient(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey), "API Key cannot be null or empty.");
            }

            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        // Helper class to deserialize the Gemini API response
        private class GeminiResponse
        {
            public Candidate[] candidates { get; set; }
        }

        private class Candidate
        {
            public Content content { get; set; }
        }

        private class Content
        {
            public Part[] parts { get; set; }
        }

        private class Part
        {
            public string text { get; set; }
        }

        public async Task<string> SendMessageAsync(string prompt, string systemMessage)
        {
            try
            {
                // Prepare the request body
                var requestBody = new
                {
                    system_instruction = new
                    // System prompt object
                    { parts = new object[] { new { text = systemMessage } } },
                    // User prompt object
                    contents = new object[] { new { role = "user", parts = new object[] { new { text = prompt } } } }
                };

                string requestUri = $"{_baseUrl}?key={_apiKey}";
                string jsonBody = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                request.Content = content;
                var response = await _httpClient.SendAsync(request);

                // Check for errors
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error: {response.StatusCode} - {errorContent}");
                    return null;  // Or throw an exception, depending on your error handling strategy
                }

                // Read and return the response
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response
                //var geminiResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                GeminiResponse geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseContent);

                if (geminiResponse?.candidates != null
                    && geminiResponse.candidates.Length > 0
                    && geminiResponse.candidates[0]?.content?.parts != null
                    && geminiResponse.candidates[0].content.parts.Length > 0)
                {
                    return geminiResponse.candidates[0].content.parts[0].text;
                }
                else
                {
                    MessageBox.Show($"Unexpected response structure: {responseContent}"); // Log the unexpected response.
                    return null; // Or throw an exception
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"HTTP Request Exception: {ex.Message}");
                return null; // Or throw the exception
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"JSON Exception: {ex.Message}");
                return null; // Or throw the exception
            }
            catch (Exception ex)
            {
                MessageBox.Show($"General Exception: {ex.Message}");
                return null; // Or throw the exception
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8618
