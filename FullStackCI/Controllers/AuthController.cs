using Asp.Versioning;
using FullStackCI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FullStackCI.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController (IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO req)
        {
            if (req.Username.Equals("usuario") && req.Password.Equals("password123"))
            {
                var _claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, req.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("rol", "Usuario"),
                };

                var _tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(_claims),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"] ?? string.Empty)),
                        SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"]
                };

                var _tokenHandler = new JwtSecurityTokenHandler();
                var _token = _tokenHandler.CreateToken(_tokenDescriptor);
                var _tokenString = _tokenHandler.WriteToken(_token);

                return Ok(new { token = _tokenString });
            }

            return Unauthorized();
        }

        [HttpGet("token-info")]
        public IActionResult GetTokenInfo([FromHeader] string authorization)
        {
            if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("Bearer "))
                return Unauthorized();

            var _token = authorization.Substring("Bearer ".Length).Trim();

            var _handler = new JwtSecurityTokenHandler();

            if (!_handler.CanReadToken(_token))
                return BadRequest("Invalid token format");

            var _jwtToken = _handler.ReadJwtToken(_token);

            return Ok(new
            {
                _jwtToken.Issuer,
                Audiences = string.Join(", ", _jwtToken.Audiences),
                Expiration = _jwtToken.ValidTo,
                Claim = _jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value)
            });
        }

        [HttpGet("validate-api-key")]
        public IActionResult ValidateApiKey([FromHeader] string apiKey)
        {
            if (!apiKey.Equals("mi-api-key"))
                return Unauthorized();

            return Ok(new { message = "ApiKey válida" });
        }
    }
}