using PlatformService.DTOS;
using System.Text.Json;
using System.Text;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platformReadDto), Encoding.UTF8,mediaType: "application/json"
                );
            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Sync POST to CommandService");
            }
            else
            {
                Console.WriteLine("Desync POST to CommandService");
            }
        }
    }
}
