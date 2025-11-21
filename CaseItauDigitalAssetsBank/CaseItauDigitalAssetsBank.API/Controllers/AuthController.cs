using CaseItauDigitalAssetsBank.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CaseItauDigitalAssetsBank.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        public record LoginRequest([Required] string Username, [Required] string Password);

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            if (!_auth.ValidateCredentials(req.Username, req.Password))
                return Unauthorized();

            var token = _auth.GenerateToken(req.Username, new[] { "User" });
            return Ok(new { token });
        }
    }
}