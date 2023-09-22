using Azure;
using Azure.AI.OpenAI;

namespace SustainaBiteAPI.Repository
{
    public sealed class ChatRepository
    {
        private readonly IConfiguration _configuration;

        public ChatRepository(IConfiguration configuration) => _configuration = configuration;

        public async Task<string> GetAsync(string[] ingredient)
        {
            OpenAIClient client = new OpenAIClient(
            new Uri(_configuration["AzureOpenAI:Endpoint"]),
                new AzureKeyCredential(_configuration["AzureOpenAI:Key"]));

            // ### If streaming is not selected
            Response<ChatCompletions> responseWithoutStream = await client.GetChatCompletionsAsync(
                "test",
                new ChatCompletionsOptions()
                {
                    Messages =
                    {
                        new ChatMessage(ChatRole.System, @"You are an AI assistant that helps people find information only about food recipies. You answer only food-related questions. You answer funny food-related jokes."),
                        new ChatMessage(ChatRole.User, $"Give me a recipie containing given ingredients: {string.Join(", ", ingredient)}"),
                    },
                    Temperature = (float)0.7,
                    MaxTokens = 800,
                    NucleusSamplingFactor = (float)0.95,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                });

            return responseWithoutStream.Value.Choices[0].Message.Content;
        }

    }
}
