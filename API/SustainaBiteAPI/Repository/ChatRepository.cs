using Azure;
using Azure.AI.OpenAI;

namespace SustainaBiteAPI.Repository
{
    public sealed class ChatRepository
    {
        private readonly IConfiguration _configuration;

        public ChatRepository(IConfiguration configuration) => _configuration = configuration;

        internal async Task<string> GetAsync(string[] ingredient)
        {
            return await GetAsync($"Give me a recipie containing given ingredients: {string.Join(", ", ingredient)}");
        }

        internal async Task<string> GetAsync(string question)
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
                        new ChatMessage(ChatRole.System, @"You are an AI assistant that helps people find recipies. You answer only food-related questions."),
                        new ChatMessage(ChatRole.User, question),
                    },
                    Temperature = (float)0.7,
                    MaxTokens = 800,
                    NucleusSamplingFactor = (float)0.95,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                });

            return responseWithoutStream.Value.Choices[0].Message.Content;
        }

        internal async Task<string> Get2Async(string question)
        {
            OpenAIClient client = new OpenAIClient(
            new Uri(_configuration.GetValue<string>("AzureOpenAI:Endpoint")),
                new AzureKeyCredential(_configuration.GetValue<string>("AzureOpenAI:Key")));

            // Azure Cognitive Search setup
            var searchEndpoint = _configuration.GetValue<string>("AzureOpenAI:SearchEndpoint"); // Add your Azure Cognitive Search endpoint here
            var searchKey = _configuration.GetValue<string>("AzureOpenAI:SearchKey"); // Add your Azure Cognitive Search admin key here
            var searchIndexName = "recipiestable"; // Add your Azure Cognitive Search index name here

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Messages =
                {
                    new ChatMessage(ChatRole.System, @"You are an AI assistant that helps people find recipies."),
                    new ChatMessage(ChatRole.User, question),
                },
                // The addition of AzureChatExtensionsOptions enables the use of Azure OpenAI capabilities that add to
                // the behavior of Chat Completions, here the "using your own data" feature to supplement the context
                // with information from an Azure Cognitive Search resource with documents that have been indexed.
                AzureExtensionsOptions = new AzureChatExtensionsOptions()
                {
                    Extensions =
                    {
                        new AzureCognitiveSearchChatExtensionConfiguration()
                        {
                            SearchEndpoint = new Uri(searchEndpoint),
                            IndexName = searchIndexName,
                            SearchKey = new AzureKeyCredential(searchKey!),
                        }
                    }
                }
            };

            // ### If streaming is not selected
            Response<ChatCompletions> responseWithoutStream = await client.GetChatCompletionsAsync(
                "gpt4-test",
                chatCompletionsOptions);

            return responseWithoutStream.Value.Choices[0].Message.Content;
        }

    }
}
