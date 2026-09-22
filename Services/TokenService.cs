using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using daza_store_be.Entities;
using daza_store_be.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace daza_store_be.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;

            // pull secret key from .env or fallback to appsettings.json
            var signingKey = Environment.GetEnvironmentVariable("JwtSettings__SigningKey") ?? _config["JWT:SigningKey"] ?? _config["JwtSettings:SigningKey"];

            if (string.IsNullOrWhiteSpace(signingKey))
            {
                throw new InvalidOperationException("JWT Signing Key is missing from configuration/environment variables");
            }

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        }

        public string CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? ""),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,
                Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? _config["JWT:Issuer"],
                Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}