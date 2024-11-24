using Microsoft.Extensions.Options;
using System.Text.Json;
using Nettside.API_Models;

namespace Nettside.Services
{
    public class StedsnavnService : IStedsnavnService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<StedsnavnService> _logger;
        private readonly ApiSettings _apiSettings;
        public StedsnavnService(HttpClient httpClient, ILogger<StedsnavnService> logger, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiSettings = apiSettings.Value;
        }
        public async Task<StedsnavnResponse> GetStedsnavnAsync(string search)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiSettings.StedsnavnApiBaseUrl}/navn?sok={search}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Stedsnavn Response: {json}");

                var stedsnavnResponse = JsonSerializer.Deserialize<StedsnavnResponse>(json);
                return stedsnavnResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching Stedsnavn for '{search}': {ex.Message}");
                return null;
            }
        }
    }
}