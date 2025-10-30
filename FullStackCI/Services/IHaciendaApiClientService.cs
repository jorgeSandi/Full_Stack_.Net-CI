using FullStackCI.Dtos;

namespace FullStackCI.Services
{
    public interface IHaciendaApiClientService
    {
        Task<RespuestaHaciendaDTO> GetHaciendaResponse(string cedula);
    }
}