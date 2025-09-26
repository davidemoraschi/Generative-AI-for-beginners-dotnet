using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;

var githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

if (string.IsNullOrEmpty(githubToken))
{
    throw new InvalidOperationException("GitHub token is not set. Please set the GITHUB_TOKEN environment variable or user secret.");
}

IChatClient client = new ChatCompletionsClient(
        endpoint: new Uri("https://models.inference.ai.azure.com"),
        new AzureKeyCredential(Environment.GetEnvironmentVariable("GITHUB_TOKEN") ?? throw new InvalidOperationException("Missing GITHUB_TOKEN environment variable. Ensure you followed the instructions to setup a GitHub Token to use GitHub Models.")))
        .AsChatClient("Phi-3.5-MoE-instruct");

var response = await client.GetResponseAsync("What is Quantum Computing?");

Console.WriteLine(response.Text);