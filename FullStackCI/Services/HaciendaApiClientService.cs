
using FullStackCI.Dtos;
using System.Text.Json;

namespace FullStackCI.Services
{
    public class HaciendaApiClientService (IHttpClientFactory httpClientFactory) : IHaciendaApiClientService
    {
        private readonly IHttpClientFactory _httpClient = httpClientFactory;
        private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public async Task<RespuestaHaciendaDTO> GetHaciendaResponse(string cedula)
        {
            try
            {
                var _client = _httpClient.CreateClient();

                var _response = await _client.GetAsync($"https://api.hacienda.go.cr/fe/ae?identificacion={cedula}");

                _response.EnsureSuccessStatusCode();

                var json = await _response.Content.ReadAsStringAsync();                
                var data = JsonSerializer.Deserialize<RespuestaHaciendaDTO>(json, options);
                if (data != null)
                    return data;
                else
                    throw new Exception();
            }
            catch
            {
                throw;
            }
        }
    }
}