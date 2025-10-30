using Asp.Versioning;
using FullStackCI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullStackCI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HaciendaController (IHaciendaApiClientService haciendaApiClientService) : ControllerBase
    {
        private readonly IHaciendaApiClientService _haciendaApiClientService = haciendaApiClientService;
        [HttpGet]
        public async Task<IActionResult> ObtenerDatosCedula(string cedula)
        {
            try
            {
                var response = await _haciendaApiClientService.GetHaciendaResponse(cedula);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}