using CaseItauDigitalAssetsBank.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CaseItauDigitalAssetsBank.API.Auth
{
    public class JwtAuthService : IAuthService
    {
        private readonly byte[] _secretBytes;
        public JwtAuthService(byte[] secretBytes) => _secretBytes = secretBytes;

        public bool ValidateCredentials(string username, string password)
        {

            return username == "admin" && password == "itauAdmin";
        }

        public string GenerateToken(string username, IEnumerable<string>? roles = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(_secretBytes);
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            if (roles != null)
                claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
