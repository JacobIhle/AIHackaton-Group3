using DallENet;

namespace SustainaBiteAPI.Repository
{
    public class ImageGeneratorRepository
    {
        private readonly IDallEClient _client;
        private readonly IHttpClientFactory _httpClientFactory;

        public ImageGeneratorRepository(IDallEClient client, IHttpClientFactory httpClientFactory)
        {
            _client = client;
            _httpClientFactory = httpClientFactory;
        }

        internal async Task<byte[]> GenerateImageAsync(string query)
        {
            var response = await _client.GenerateImagesAsync(query);

            using HttpClient httpClient = _httpClientFactory.CreateClient();
            return await httpClient.GetByteArrayAsync(response.Result.Images.FirstOrDefault().Url);
        }
    }
}
